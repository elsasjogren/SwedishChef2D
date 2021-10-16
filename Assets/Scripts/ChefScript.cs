using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefScript : CharacterInheritance
{
    public float speed;

    private Rigidbody2D rb2d;
    private SpriteRenderer mySpriteRenderer;

    public float jumpForce;
    public Sprite[] walking = new Sprite[4];
    public Sprite[] jumpin = new Sprite[2];


    // true if walked last update
    private bool wasWalking;
    private Sprite idle;
    public bool isHurting = false;
    public float hurtTime = 1.5f;

    public bool isCollecting = false; // true if currently picking up an item

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        idle = mySpriteRenderer.sprite;
    }


    void Update()
    {

        float movementHorizontal = 0;

        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mySpriteRenderer.flipX = true;
            movementHorizontal = -speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            mySpriteRenderer.flipX = false;
            movementHorizontal = speed;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        // animate if moving, stop if not
        if (movementHorizontal == 0)
        {
            mySpriteRenderer.sprite = idle;
            wasWalking = false;

        } else {
            if (!wasWalking) 
            {
                StartCoroutine(walkingAnimation(0.1f));
                wasWalking = true;
            }
        }
       
        rb2d.velocity = new Vector2(movementHorizontal, rb2d.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item") && !isCollecting)
        {

            StartCoroutine(delay(isCollecting));
            // collect the croissant
            Destroy(collision.gameObject);
            UIScript.IncreaseScore();  // doesnt work rn :((((
        }
    }

    protected override void Hurt(Vector3 impactDirection)
    {
        if (Mathf.Abs(impactDirection.x) > Mathf.Abs(impactDirection.y))
        {
            TakeDamage();
        } else {
            if (impactDirection.y > 0.0f)
            {
                TakeDamage();
            }
            Vector2 vel = rb2d.velocity;
            //vel.y = jumpforce;
            rb2d.velocity = vel*-impactDirection.normalized;
        }
        
    }

    public void TakeDamage()
    {
        if(!isHurting)
        {
            UIScript.Damaged();
            StartCoroutine(delay(isHurting));
        }
    }


    // animate the walking with timePerFrame seconds between each frame
    IEnumerator walkingAnimation(float timePerFrame)
    {
        for (int i = 0; i < walking.Length; i++)
        {
            yield return new WaitForSeconds(timePerFrame);
            mySpriteRenderer.sprite = walking[i];
        }

        // loop, or stop moving depending on if the last frame he walked or not
        if(wasWalking)
        {
            StartCoroutine(walkingAnimation(timePerFrame));
        } else
        {
            mySpriteRenderer.sprite = idle;
            yield break;
        }
        
    }

    IEnumerator delay(bool boolVal)
    {
        boolVal = true;
        yield return new WaitForSeconds(0.1f);
        boolVal = false;
    }

};
