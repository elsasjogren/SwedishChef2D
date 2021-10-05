using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterScript : CharacterInheritance
{
    private Rigidbody2D rb2d;
    private SpriteRenderer mySpriteRenderer;

    public Sprite[] jumpSprite = new Sprite[5];
    public Sprite[] attackSprite = new Sprite[6];
    public Sprite[] destroySprite = new Sprite[7];

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
        startPos = transform.position;
        maxPos = startPos.x + range / 2;
        minPos = startPos.x - range / 2;
    }

    private void Update()
    {
        int count = 0;
        Vector2 vel = rb2d.velocity;
        vel.x = monsterSpeed * directionMonster;
        rb2d.velocity = vel;
        mySpriteRenderer.sprite = jumpSprite[count];
        count++;
        monsterDirection();
        StartCoroutine(walkingAnimation(0.5f));  // animate
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

    private void OnTriggerEnter2D(Collider2D collision)
    //on collision with player should take away one of the players three/five lives 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Lives.hearts > 0)
            {
                Lives.hearts--;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        maxPos = startPos.x + range / 2;
        minPos = startPos.x - range / 2;
        //called if you have selected the game object in editor
        Vector3 minPosTransform = new Vector3(minPos, transform.position.y, -1);
        Vector3 maxPosTransform = new Vector3(maxPos, transform.position.y, -1);
        Debug.DrawLine(minPosTransform, maxPosTransform, Color.cyan);
    }

    // animate the walking with timePerFrame seconds between each frame
    IEnumerator walkingAnimation(float timePerFrame)
    {
        for (int i = 0; i < jumpSprite.Length; i++)
        {
            yield return new WaitForSeconds(timePerFrame);
            mySpriteRenderer.sprite = jumpSprite[i];
            yield return new WaitForSeconds(timePerFrame);
        }

        // loop
        StartCoroutine(walkingAnimation(timePerFrame));
    }
}