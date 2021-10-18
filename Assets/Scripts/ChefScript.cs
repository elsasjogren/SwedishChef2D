using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefScript : CharacterInheritance
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float bounceForce;
    [SerializeField] float airSpeedReduction = 0.8f;
    [SerializeField] Sprite[] walking = new Sprite[4];

    // true if walked last update
    public bool inMotion = false;
    [SerializeField] Sprite idle;
    [SerializeField] float hurtTime = 1.5f;

    // to enable player movement
    public bool movement = false;

    private SpriteRenderer mySpriteRenderer;

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

    // take keyboard input to move the player
    private void Move()
    {
        // get horizontal input to determine direction of motion
        float movementHorizontal = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mySpriteRenderer.flipX = true; // flip sprite to face in direction of motion
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

        // if the pushed direction vector is greater than zero, move the player
        if(pushed.magnitude > 0)
        {
            rb2d.velocity = -pushed.normalized*bounceForce;
            pushed = Vector3.zero;
        }

        // reduce airspeed
        if (!isGrounded)
        {
            movementHorizontal *= airSpeedReduction;
        }

        // animate if moving, stop if not
        if (movementHorizontal == 0)
        {
            mySpriteRenderer.sprite = idle;
            inMotion = false;

        } else {
            if (!inMotion) // start animation again, if was still
            {
                inMotion = true;
                StartCoroutine(walkingAnimation(0.1f));
            }
        }

        // set new velocity
        rb2d.velocity = new Vector2(movementHorizontal, rb2d.velocity.y);
    }

    // hurt character depending on the direction of the damage
    protected override void Hurt(Vector3 impactDirection, bool wasAirborne)
    {
        if (Mathf.Abs(impactDirection.x) > Mathf.Abs(impactDirection.y))
        {
            TakeDamage();

        } else {
            if (impactDirection.y > 0.0f)
            {
                TakeDamage();
            }
        }
        
    }

    // damage character
    protected override void TakeDamage()
    {
        // only hurt if currently not already hurt
        if(!isHurting)
        {
            UIScript.Damaged();
            isHurting = true;
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
    private IEnumerator Hurting(float timetToHurt)
    {
        float time = 0;
        while (time < timetToHurt)
        {
            if(time < timetToHurt/4 || (time > timetToHurt/2 && time < 3*timetToHurt/4))
            {
                mySpriteRenderer.color = new Color(255, 0, 0);
            } else {
                mySpriteRenderer.color = new Color(255, 255, 255);
            }
            time += Time.deltaTime; // normalize the time
            yield return null;
        }

        // ensure his color goes back to normal after
        mySpriteRenderer.color = new Color(255, 255, 255);
        isHurting = false;
    }

};
