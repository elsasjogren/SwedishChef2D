using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterInheritance : MonoBehaviour
{
    // attributes and functions that are shared between player and enemies
    public bool isGrounded = false; // true if on the ground
    protected bool isHurting = false; // true if being damaged
    protected Vector2 pushed;
    public Rigidbody2D rb2d;

    // hurt character depending on the direction of the damage
    protected abstract void Hurt(Vector3 impactDirection, bool wasAirborne);
    
    // damage character
    protected abstract void TakeDamage();
    
    // flip through walking animation with timePerFrame between each sprite
    protected abstract IEnumerator walkingAnimation(float timePerFrame);

    private void OnCollisionStay2D(Collision2D collision)
    {
        // check that the collision came from another character
        CharacterInheritance controller = collision.gameObject.GetComponent<CharacterInheritance>();
        
        // hurt if not already hurting
        if (controller != null && !isHurting)
        {
            Vector3 impactDirection = collision.gameObject.transform.position - transform.position;
            bool wasAirborne = !controller.isGrounded;
            pushed = impactDirection;
            Hurt(impactDirection, wasAirborne);
        }
    }
}
