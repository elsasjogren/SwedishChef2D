using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterInheritance : MonoBehaviour
{

    public bool isGrounded = false;

    protected abstract void Hurt(Vector3 impactDirection);

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
