using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterInheritance : MonoBehaviour
{
    // attributes and functions that are shared between player and enemies
    public bool isGrounded = false;
    protected Rigidbody2D rb2d;

    protected abstract void Hurt(Vector3 impactDirection);
    protected abstract void TakeDamage();
    protected abstract IEnumerator walkingAnimation(float timePerFrame);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        CharacterInheritance controller = collision.gameObject.GetComponent<CharacterInheritance>();
        if (controller != null)
        {
            Vector3 impactDirection = collision.gameObject.transform.position - transform.position;
            Hurt(impactDirection);
        }
    }
}
