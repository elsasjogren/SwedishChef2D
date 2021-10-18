using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefScript : CharacterInheritance
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float airSpeedReduction = 0.8f;
    [SerializeField] Sprite[] walking = new Sprite[4];

    // true if walked last update
    [SerializeField] bool inMotion = false;
    [SerializeField] Sprite idle;
    [SerializeField] float hurtTime = 1.5f;
    [SerializeField] AnimationCurve blink;

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
        }
        
    }

    protected override void TakeDamage()
    {
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
        while (time < 1)
        {
            mySpriteRenderer.color = new Color(255, 255*blink.Evaluate(time), 255*blink.Evaluate(time));
            time += Time.deltaTime; // normalize the time
            yield return null;
        }

        // ensure his color goes back to normal after
        mySpriteRenderer.color = new Color(255, 255, 255);
        isHurting = false;
    }

};
