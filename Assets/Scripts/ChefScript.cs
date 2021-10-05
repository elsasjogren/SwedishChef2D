using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefScript : CharacterInheritance
{
    public float speed;

    private Rigidbody2D rb2d;
    private SpriteRenderer mySpriteRenderer;

    public Sprite[] walking = new Sprite[4];
    public Sprite[] jumpin = new Sprite[2];

    // true if currently walking
    private bool isWalking;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

        float movementHorizontal = 0;

        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Sprite leftSprite = walking[Random.Range(0, walking.Length)];
            //mySpriteRenderer.sprite = leftSprite;
            mySpriteRenderer.flipX = true;
 
            // x component is - * speed
            movementHorizontal = -speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //Sprite rightSprite = walking[Random.Range(0, walking.Length)];
            //mySpriteRenderer.sprite = rightSprite;
            mySpriteRenderer.flipX = false;

            movementHorizontal = speed;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            float movementVertical = 5f;

            rb2d.velocity = new Vector2(rb2d.velocity.x, movementVertical);
        }

        // animate if moving, stop if not
        if (movementHorizontal == 0)
        {
            StopAllCoroutines();
            isWalking = false;
        } else { 
            if (!isWalking) 
            {
                StartCoroutine(walkingAnimation(0.1f));
                isWalking = true;
            }
        }
        

        rb2d.velocity = new Vector2(movementHorizontal, rb2d.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            // collect the croissant
            UIScript.IncreaseScore();  // doesnt work rn :((((
            Destroy(collision.gameObject);
        }
    }

    // animate the walking with timePerFrame seconds between each frame
    IEnumerator walkingAnimation(float timePerFrame)
    {
        for(int i = 0; i < walking.Length; i++)
        {
            yield return new WaitForSeconds(timePerFrame);
            mySpriteRenderer.sprite = walking[i];
        }

        // loop
        StartCoroutine(walkingAnimation(timePerFrame));
    }
};
