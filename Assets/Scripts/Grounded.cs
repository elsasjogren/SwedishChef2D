using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    GameObject character;
    CharacterInheritance groundedChar;
    
    void Start()
    {
        // init vars
        character = gameObject.transform.parent.gameObject;
        groundedChar = character.GetComponent<CharacterInheritance>();
    }

    // check if the grounded object has passed through/out of ground
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            groundedChar.isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            groundedChar.isGrounded = false;
        }
    }
}
