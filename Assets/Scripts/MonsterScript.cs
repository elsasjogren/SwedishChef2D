using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterScript : CharacterInheritance
{
    // animation vars
    [SerializeField] Sprite[] movingSprite = new Sprite[5]; // moving flipbook
    [SerializeField] Sprite[] destroySprite = new Sprite[7]; // dying flipbook
    [SerializeField] float timeToDie; // length dying animation takes
    [SerializeField] AudioClip squished; // sound when stepped on

    // motion vars
    [SerializeField] float monsterSpeed;
    [SerializeField] float range; // length monster can move
    private float maxPos; // right most possible position
    private float minPos; // left most possible position
    private Vector3 startPos; // pos on game start
    private int directionMonster = 1; // positive if moving right, negative if left

    // dropping items
    [SerializeField] bool holdingPastry; // true if will drop pastry on death
    [SerializeField] GameObject pastry; // prefab to instatiate on death

    private SpriteRenderer mySpriteRenderer;
    private AudioSource myaudio;

    void Start()
    {
        // init vars
        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myaudio = GetComponent<AudioSource>();
        startPos = transform.position;
        maxPos = startPos.x + range / 2;
        minPos = startPos.x - range / 2;

        StartCoroutine(walkingAnimation(0.3f));  // animate
    }

    void Update()
    {
        // move back and forth
        Vector2 vel = rb2d.velocity;
        vel.x = monsterSpeed * directionMonster;
        rb2d.velocity = vel;
        monsterDirection(); // update direction
    }

    // determine direction of the monster motion/sprite
    void monsterDirection()
    {
        //define a space within the monster can run and if he hits on edge turn and go the other way
        if (transform.position.x > maxPos)
        {
            //turn left
            directionMonster = -1;
            mySpriteRenderer.flipX = true;
        }
        if (transform.position.x < minPos)
        {
            //turn right
            directionMonster = 1;
            mySpriteRenderer.flipX = false;
        }
    }

    // animate the walking with timePerFrame seconds between each frame
    protected override IEnumerator walkingAnimation(float timePerFrame)
    {
        // go through sprites
        for (int i = 0; i < movingSprite.Length; i++)
        {
            if(isHurting) yield break; // stop walking if dying
            yield return new WaitForSeconds(timePerFrame);
            mySpriteRenderer.sprite = movingSprite[i];
        }

        if(!isHurting) // if not dying, keep walking
        {
            StartCoroutine(walkingAnimation(timePerFrame));
        }
        
    }

    // animate the death for timeToDie seconds
    private IEnumerator dyingAnimation(float timeToDie)
    {
        // play sound when stepped on
        myaudio.PlayOneShot(squished);
        
        // go through sprites
        for (int i = 0; i < destroySprite.Length; i++)
        {
            yield return new WaitForSeconds(timeToDie / destroySprite.Length);
            mySpriteRenderer.sprite = destroySprite[i];
        }

        // drop the pastry, if it was holding one
        if (holdingPastry)
        {
            // make invisible and delay destruction to allow spawning
            mySpriteRenderer.sprite = null;
            Instantiate(pastry, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }

        // kill it
        Destroy(gameObject);
    }

    // 
    protected override void Hurt(Vector3 impactDirection, bool wasAirborne)
    {
        //if the player collides with the enemy from the side
        if (Mathf.Abs(impactDirection.x) > Mathf.Abs(impactDirection.y)) 
        {
            directionMonster = (int)Mathf.Sign(-impactDirection.x); //change the monsters direction so he will contantly attack the player 
        } else {
            // if the collision comes from the top of the monster(i.e. monster is jumped on)
            if (wasAirborne) 
            {
                TakeDamage();
            }
        }
    }
    // kill monster if damage is taken
    protected override void TakeDamage() {
        isHurting = true;
        monsterSpeed = 0; // stop moving
        rb2d.simulated = false; // prevent collision with dying monster
        StartCoroutine(dyingAnimation(timeToDie)); // begin death
    }
    
    // show the line that monster will walk along
    private void OnDrawGizmosSelected()
    {
        //called if you have selected the game object in editor
        maxPos = transform.position.x + range / 2;
        minPos = transform.position.x - range / 2;

        Vector3 minPosTransform = new Vector3(minPos, transform.position.y, -1);
        Vector3 maxPosTransform = new Vector3(maxPos, transform.position.y, -1);
        Debug.DrawLine(minPosTransform, maxPosTransform, Color.cyan);
    }


}