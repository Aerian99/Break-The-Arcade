using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // System components
    [SerializeField] private LayerMask platformsLayerMask;

    GhostController ghostController;

    // Player components
    private Rigidbody2D rb;
    private Animator animator;
    [HideInInspector] public SpriteRenderer p_sprite;
    private BoxCollider2D p_collider;

    // Particles
    private ParticleSystem p_JumpDust;
    private ParticleSystem p_RunDust;

    // Movement
    private float moveSpeed;

    // Jump
    public static float jumpForce;
    private bool isJumping;
    private float jumpTimeCounter;
    private float jumpTime;

    // Dash
    private float dashForce;
    private float StartDashTimer;
    private float CurrentDashTimer;
    private float DashDirection;
    private float dashCooldown;
    private bool isDashing;
    private bool canDash;
    public GameObject dashWarning;

    // Other variables
    private float movX;

    void Start()
    {
        ghostController = GetComponent<GhostController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        p_sprite = GetComponent<SpriteRenderer>();
        p_collider = GetComponent<BoxCollider2D>();
        p_JumpDust = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        p_RunDust = this.transform.GetChild(1).GetComponent<ParticleSystem>();

        moveSpeed = 6.5f;
        jumpForce = 15f;
        jumpTime = 0.13f;
        dashForce = 25f;
        StartDashTimer = 0.1f;
        dashCooldown = 0f;
        ghostController.enabled = false;
    }

    void Update()
    {
        // FUNCTIONS
        Move();
        alternativeJump();
        Dash();
        SetAnimationState();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            movX = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(+moveSpeed, rb.velocity.y);
            movX = 1;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            movX = 0;
        }
    }

    // Normal jump. Same jump force always.
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            p_JumpDust.Play();
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    // This Jump function works with key hold detection. Hold to jump higher.
    void alternativeJump()
    {
        if (IsGrounded() == true && Input.GetKeyDown(KeyCode.Space))
        {
            p_JumpDust.Play();
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                p_JumpDust.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            p_RunDust.Play();
            isJumping = false;
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && movX != 0 && canDash)
        {
            isDashing = true;
            CurrentDashTimer = StartDashTimer;
            rb.velocity = Vector2.zero;
            DashDirection = (int) movX;
            ghostController.enabled = true;
            canDash = false;
            dashWarning.SetActive(false);
        }

        if (isDashing)
        {
            rb.velocity = transform.right * DashDirection * dashForce;
            CurrentDashTimer -= Time.deltaTime;

            if (CurrentDashTimer <= 0)
            {
                isDashing = false;
                ghostController.enabled = false;
            }
        }

        dashCooldown -= Time.deltaTime;
        if (dashCooldown <= 0)
        {
            canDash = true;
            dashWarning.SetActive(true);
            dashCooldown = 3f;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(p_collider.bounds.center, p_collider.bounds.size, 0f,
            Vector2.down, 0.2f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }

    void SetAnimationState()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && rb.velocity.y == 0)
        {
            animator.SetBool("isRunning", true);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && rb.velocity.y < 0)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (rb.velocity.y > 0)
        {
            animator.SetBool("isJumping", true);
        }

        if (rb.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }

        if (rb.velocity.y == 0 && IsGrounded())
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }
}