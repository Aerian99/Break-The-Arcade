using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    private BoxCollider2D p_collider;
    private float movX;
    private Rigidbody2D rb;

    private float jumpPower;
    private float speed;

    public float dashForce;
    public float StartDashTimer;

    float CurrentDashTimer;
    float DashDirection;

    bool isDashing;

    void Start()
    {
        p_collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        jumpPower = 20f;
        speed = 10f;
        dashForce = 25f;
        StartDashTimer = 0.15f;
    }

    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movX * speed, rb.velocity.y);

        Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && !IsGrounded() && movX != 0) 
        {
            isDashing = true;
            CurrentDashTimer = StartDashTimer;
            rb.velocity = Vector2.zero;
            DashDirection = (int)movX;
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

    void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(p_collider.bounds.center, p_collider.bounds.size, 0f, Vector2.down, 0.2f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }
}
