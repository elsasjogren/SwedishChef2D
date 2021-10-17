using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefScript : CharacterInheritance
{
    public float speed;

    private SpriteRenderer mySpriteRenderer;

    public float jumpForce;
    public float airSpeedReduction = 0.8f;
    public Sprite[] walking = new Sprite[4];
    public Sprite[] jumpin = new Sprite[2];


    // true if walked last update
    private bool inMotion = false;
    private Sprite idle;
    public bool isHurting = false;
    public float hurtTime = 1.5f;

    public bool isCollecting = false; // true if currently picking up an item
    public bool movement = false;

    void Start()
    {
        // initialize vars
        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        idle = mySpriteRenderer.sprite;
    }


    void Update()
    {
        // dont move until door is done speaking
        if(movement)
        {
            Move();
        }
    }

    private void Move()
    {
        // get horizontal input to determine motion
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

        // jump (only while on the ground)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        // reduce airspeed
        if (!isGrounded)
        {
            movementHorizontal = movementHorizontal * airSpeedReduction;
        }

        // animate if moving, stop if not
        if (movementHorizontal == 0)
        {
            mySpriteRenderer.sprite = idle;
            inMotion = false;

        } else { // is in motion
            if (!inMotion) // start animation again, if was still
            {
                inMotion = true;
                StartCoroutine(walkingAnimation(0.1f));
            }
        }

        // set new velocity
        rb2d.velocity = new Vector2(movementHorizontal, rb2d.velocity.y);
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

    protected override void TakeDamage()
    {
        if(!isHurting)
        {
            UIScript.Damaged();
            StartCoroutine(Hurting(hurtTime));
        }
    }


    // animate the walking with timePerFrame seconds between each frame
    protected override IEnumerator walkingAnimation(float timePerFrame)
    {
        for (int i = 0; i < walking.Length; i++)
        {
            if(!inMotion) // end animation if still
            {
                mySpriteRenderer.sprite = idle;
                yield break;
            }
            yield return new WaitForSeconds(timePerFrame);
            mySpriteRenderer.sprite = walking[i];
        }

        // loop, or stop moving depending on if the last frame he walked or not
        if(inMotion)
        {
            StartCoroutine(walkingAnimation(timePerFrame));
        }
        
    }

    IEnumerator Hurting(float timetToHurt)
    {
        yield return new WaitForSeconds(0.1f);
    }

};
