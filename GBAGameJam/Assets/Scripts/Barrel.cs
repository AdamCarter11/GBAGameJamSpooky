using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] float knockbackAmount = 5f;
    [SerializeField] float upwardForce = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hitRb = collision.rigidbody;
            if (hitRb != null)
            {
                // knockback object
                Vector2 knockbackDirection = -collision.relativeVelocity.normalized;
                knockbackDirection.y = upwardForce;
                knockbackDirection = knockbackDirection.normalized;
                hitRb.AddForce(knockbackDirection * knockbackAmount, ForceMode2D.Impulse);


                Destroy(this.gameObject);
            }
        }
        
    }
}
