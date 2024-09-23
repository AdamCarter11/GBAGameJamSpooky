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
    [SerializeField] float hitGroundVelocity = 8f;
    [SerializeField] float hitGroundPauseTime = .2f;
    Rigidbody2D rb;
    bool onGround = false;
    bool groundPause = false;
    float horizontalMove = 0;
    bool isFacingRight = true;
    [HideInInspector] public bool canMove = true;
    private Animator animator;
    [HideInInspector] public bool marioHat = false;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = playerSprite.GetComponent<Animator>();
    }
    private void Update()
    {
        MovementLogic();
        FallLogic();

        AnimationLogic();
        //print(canMove);
    }

    #region Falling Logic
    private void FallLogic()
    {
        if (!IsGrounded())
        {
            onGround = false;
        }
        else
        {
            if (!onGround && Mathf.Abs(rb.velocity.y) > hitGroundVelocity)
            {
                // trigger hit ground animation + maybe sfx

                // pause movement so you can't 
                StartCoroutine(PauseMovement(hitGroundPauseTime));
            }
            onGround = true;
        }
    }
    IEnumerator PauseMovement(float pauseTime)
    {
        animator.SetBool("HitGround", true);
        groundPause = true;
        yield return new WaitForSeconds(pauseTime);
        groundPause = false;
        animator.SetBool("HitGround", false);
    }
    #endregion

    #region Movement logic + animations
    private void MovementLogic()
    {
        if(canMove && !groundPause)
        {
            rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);
        }
        else
        {
            if(!groundPause)
                rb.velocity = new Vector2(0,0);
            else
                rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (!isFacingRight && horizontalMove > 0)
        {
            Flip();
        }
        else if (isFacingRight && horizontalMove < 0)
        {
            Flip();
        }
    }
    private void AnimationLogic()
    {
        if (!IsGrounded())
        {
            animator.SetFloat("Jump", rb.velocity.y);
        }
        else
        {
            animator.SetFloat("Jump", 0);
        }

        animator.SetBool("Grounded", IsGrounded());
    }
    #endregion

    #region Input logic
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

    public void Move(InputAction.CallbackContext context)
    {
        if (canMove)
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
    #endregion

    #region Movement helper functions
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
    #endregion

}
