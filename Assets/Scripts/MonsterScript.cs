using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterScript : CharacterInheritance
{
    public bool FindPlayer;

    private SpriteRenderer mySpriteRenderer;
    private AudioSource myaudio;

    public Sprite[] movingSprite = new Sprite[5];
    public Sprite[] destroySprite = new Sprite[7];
    public bool dying;
    public float timeToDie;
    public AudioClip squished;

    // motion vars
    public float monsterSpeed;
    float maxPos;
    float minPos;
    public float range;
    private Vector3 startPos;
    private int directionMonster = 1;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myaudio = GetComponent<AudioSource>();
        startPos = transform.position;
        maxPos = startPos.x + range / 2;
        minPos = startPos.x - range / 2;

        StartCoroutine(walkingAnimation(0.3f));  // animate
    }

    private void Update()
    {
        Vector2 vel = rb2d.velocity;
        vel.x = monsterSpeed * directionMonster;
        rb2d.velocity = vel;
        monsterDirection();
    }

    void monsterDirection()
    {

        //define a space within the monster can run and if he hits on edge turn and go the other way
        if (transform.position.x > maxPos)
        {
            directionMonster = -1;
            //turn left
            mySpriteRenderer.flipX = true;
        }
        if (transform.position.x < minPos)
        {
            directionMonster = 1;
            //turn right
            mySpriteRenderer.flipX = false;
        }
    }

    // animate the walking with timePerFrame seconds between each frame
    protected override IEnumerator walkingAnimation(float timePerFrame)
    {
        // go through sprites
        for (int i = 0; i < movingSprite.Length; i++)
        {
            if(dying) yield break;
            yield return new WaitForSeconds(timePerFrame);
            mySpriteRenderer.sprite = movingSprite[i];
        }

        if(!dying)
        {
            StartCoroutine(walkingAnimation(timePerFrame));
        }
        
    }

    private IEnumerator dyingAnimation(float timeToDie)
    {

        myaudio.PlayOneShot(squished);
        // go through sprites
        for (int i = 0; i < destroySprite.Length; i++)
        {
            yield return new WaitForSeconds(timeToDie / destroySprite.Length);
            mySpriteRenderer.sprite = destroySprite[i];
        }

        Destroy(gameObject);
    }



    protected override void Hurt(Vector3 impactDirection)
    {
        if (Mathf.Abs(impactDirection.x) > Mathf.Abs(impactDirection.y)) //if the player collides with the enemy from the side
        {
            directionMonster = (int)Mathf.Sign(-impactDirection.x); //change the monsters direction so he will contantly attack the player 
        }
        else
        {
            if (impactDirection.y > 0.0f) // if the collision comes from the top of the monster(i.e. monster is jumped on)
            {
                TakeDamage();
            }
        }

    }
    // kill monster if damage is taken
    protected override void TakeDamage() {
        dying = true;
        monsterSpeed = 0;
        Vector3 currPos = transform.position;
        rb2d.simulated = false;
        StartCoroutine(dyingAnimation(timeToDie));
    }

    //called if you have selected the game object in editor
    private void OnDrawGizmosSelected()
    {
        maxPos = startPos.x + range / 2;
        minPos = startPos.x - range / 2;

        Vector3 minPosTransform = new Vector3(minPos, transform.position.y, -1);
        Vector3 maxPosTransform = new Vector3(maxPos, transform.position.y, -1);
        Debug.DrawLine(minPosTransform, maxPosTransform, Color.cyan);
    }


}