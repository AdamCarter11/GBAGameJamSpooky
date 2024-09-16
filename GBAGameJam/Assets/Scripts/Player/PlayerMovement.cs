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
    [SerializeField] GameObject footstepManager;
    Rigidbody2D rb;
    float horizontalMove = 0;
    bool isFacingRight = true;
    [HideInInspector] public bool canMove = true;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = playerSprite.GetComponent<Animator>();
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
        if(!IsGrounded())
        {
            animator.SetFloat("Jump", rb.velocity.y);
        }
        else
        {
            animator.SetFloat("Jump", 0);
        }
        
        animator.SetBool("Grounded", IsGrounded());
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
        else 
            horizontalMove = 0f;
        playerSprite.GetComponent<Animator>().SetFloat("Move", Mathf.Abs(horizontalMove));
        if (Mathf.Abs(horizontalMove) > 0 && IsGrounded())
        {
            footstepManager.SetActive(true);
        }
        else
        {
            footstepManager.SetActive(false);
        }
    }
}
