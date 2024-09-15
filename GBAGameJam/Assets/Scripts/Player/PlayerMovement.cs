using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject playerSprite;
    Rigidbody2D rb;
    float horizontalMove;
    bool isFacingRight = true;
    [HideInInspector] public bool canMove = true;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);
        if(!isFacingRight && horizontalMove > 0)
        {
            Flip();
        }
        else if (isFacingRight && horizontalMove < 0)
        {
            Flip();
        }
        animator = playerSprite.GetComponent<Animator>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
        animator.SetFloat("Jump", rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(canMove)
            horizontalMove = context.ReadValue<Vector2>().x;
            playerSprite.GetComponent<Animator>().SetFloat("Move", Mathf.Abs(horizontalMove));
    }
}
