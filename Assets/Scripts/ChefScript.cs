using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefScript : MonoBehaviour
{
    public float speed;

    public bool isGrounded = false;

    private Rigidbody2D rb2d;
    private SpriteRenderer mySpriteRenderer;

    public Sprite[] walking = new Sprite[4];
    public Sprite[] jumpin = new Sprite[2];



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        // Using RigidBody2D physics (by setting its velocity) for movement with Up-, Down-, Left-, Right-Arrow to move
        Jump();

        float movementHorizontal = 0;
        float movementVertical = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Sprite leftSprite = walking[Random.Range(0, walking.Length)];
            mySpriteRenderer.sprite = leftSprite;
            mySpriteRenderer.flipX = true;
            // x component is - * speed
            movementHorizontal = -speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Sprite rightSprite = walking[Random.Range(0, walking.Length)];
            mySpriteRenderer.sprite = rightSprite;
            mySpriteRenderer.flipX = false;
            movementHorizontal = speed;
        }

        rb2d.velocity = new Vector2(movementHorizontal, movementVertical);
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
    }

};
