using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    GameObject character;
    CharacterInheritance groundedChar;
    
    void Start()
    {
        character = gameObject.transform.parent.gameObject;
        groundedChar = character.GetComponent<CharacterInheritance>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            groundedChar.isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            groundedChar.isGrounded = false ;
        }
    }
}
