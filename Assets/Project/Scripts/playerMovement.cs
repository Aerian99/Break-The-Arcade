using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class playerMovement : MonoBehaviour
{
    // System components
    public static LayerMask platformsLayerMask;
    [HideInInspector] public LayerMask copyLayerMask;

    GhostController ghostController;

    // Player components
    private Rigidbody2D rb;
    private Animator animator;
    [HideInInspector] public SpriteRenderer p_sprite;
    public static BoxCollider2D p_collider;

    // Particles
    private ParticleSystem p_JumpParticle;
    private ParticleSystem p_FallParticle;
    private ParticleSystem p_RunParticleLeft;
    private ParticleSystem p_RunParticleRight;

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
    public Image dashImage;
    public GameObject postProcessing;

    // Other variables
    public static float movX;

    // Audio
    public AudioSource aud;

    void Start()
    {
        postProcessing.SetActive(false);
        ghostController = GetComponent<GhostController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        p_sprite = GetComponent<SpriteRenderer>();
        p_collider = GetComponent<BoxCollider2D>();

        p_JumpParticle = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        p_FallParticle = this.transform.GetChild(1).GetComponent<ParticleSystem>();
        p_RunParticleLeft = this.transform.GetChild(2).GetComponent<ParticleSystem>();
        p_RunParticleRight = this.transform.GetChild(3).GetComponent<ParticleSystem>();

        platformsLayerMask = copyLayerMask;

        moveSpeed = 7f;
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
        Jump();
        Dash();
        SetAnimationState();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            movX = -1;
            if (!aud.isPlaying && IsGrounded())
            {
                aud.Play();
            }

            if (IsGrounded())
            {
                p_RunParticleLeft.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(+moveSpeed, rb.velocity.y);
            movX = 1;
            if (!aud.isPlaying && IsGrounded())
            {
                aud.Play();
            }

            if (IsGrounded())
            {
                p_RunParticleRight.Play();
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            aud.Stop();
        }
    }

    // This Jump function works with key hold detection. Hold to jump higher.
    void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && !landCollision.groundSuperJump)
        {
            SoundManagerScript.PlaySound("jump");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                p_JumpParticle.Play();
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
            isJumping = false;
        }

        if (rb.velocity.y > -1f)
        {
            p_FallParticle.Play();
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && movX != 0 && canDash)
        {
            postProcessing.SetActive(true);
            SoundManagerScript.PlaySound("dash");
            isDashing = true;
            CurrentDashTimer = StartDashTimer;
            rb.velocity = Vector2.zero;
            DashDirection = (int) movX;
            ghostController.enabled = true;
            canDash = false;
            dashImage.fillAmount = 0f;
        }

        if (isDashing)
        {
            rb.velocity = transform.right * DashDirection * dashForce;
            CurrentDashTimer -= Time.deltaTime;

            if (CurrentDashTimer <= 0)
            {
                isDashing = false;
                ghostController.enabled = false;
                postProcessing.SetActive(false);
            }
        }

        if (!canDash)
        {
            dashCooldown -= Time.deltaTime;
        }

        if (dashCooldown <= 0)
        {
            canDash = true;
            dashImage.fillAmount = 1f;
            dashCooldown = 3f;
            dashImage.GetComponent<Animator>().SetTrigger("ready");
        }

        dashImage.fillAmount += 1.0f / 3.0f * Time.deltaTime;
    }

    public static bool IsGrounded()
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