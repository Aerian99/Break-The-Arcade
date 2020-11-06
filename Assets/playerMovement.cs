﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    // System components
    [SerializeField] private LayerMask platformsLayerMask;

    // Player components
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer p_sprite;
    private BoxCollider2D p_collider;

    // Particles
    private ParticleSystem p_JumpDust;
    private ParticleSystem p_RunDust;

    // Player movement
    private float moveSpeed;
    private float jumpForce;

    public float dashForce;
    public float StartDashTimer;
    float CurrentDashTimer;
    float DashDirection;
    bool isDashing;
    bool canDash;


    // Other variables
    private float movX;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        p_sprite = GetComponent<SpriteRenderer>();
        p_collider = GetComponent<BoxCollider2D>();
        p_JumpDust = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        p_RunDust = this.transform.GetChild(1).GetComponent<ParticleSystem>();

        moveSpeed = 6f;
        jumpForce = 15f;
        dashForce = 25f;
        StartDashTimer = 0.1f;
    }


    void Update()
    {
        // InGame variables
        movX = Input.GetAxis("Horizontal");

        // Functions
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
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(+moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            p_JumpDust.Play();
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    #region Dash
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !IsGrounded() && movX != 0)
        {
            isDashing = true;
            CurrentDashTimer = StartDashTimer;
            rb.velocity = Vector2.zero;
            DashDirection = (int)movX;
            canDash = false;
        }
        if (isDashing)
        {
            rb.velocity = transform.right * DashDirection * dashForce;
            CurrentDashTimer -= Time.deltaTime;

            if (CurrentDashTimer <= 0)
            {
                isDashing = false;
            }
        }
    }


    #endregion


    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(p_collider.bounds.center, p_collider.bounds.size, 0f, Vector2.down, 0.2f, platformsLayerMask);
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
