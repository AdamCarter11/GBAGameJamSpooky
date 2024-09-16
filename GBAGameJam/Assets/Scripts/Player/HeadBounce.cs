using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBounce : MonoBehaviour
{
    [SerializeField] float bounceHeight;
    [SerializeField] Rigidbody2D playerRb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Barrel"))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, bounceHeight);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Barrel"))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, bounceHeight);
        }
    }
}
