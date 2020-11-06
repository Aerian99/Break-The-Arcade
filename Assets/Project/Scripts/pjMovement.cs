using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pjMovement : MonoBehaviour
{
    private const float JUMP_FORCE = 100f;
    [SerializeField] private LayerMask platformsLayerMask;
    private Vector3 moveDir;
    private Rigidbody2D rb;
    public ParticleSystem dust;
    private Animator anim;
    private BoxCollider2D playerCollider;

    public AudioSource jump;


    void Start()
    {
        playerCollider = transform.GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        float moveSpeed = 1f;
        anim.SetBool("Idle", true); // Activamos el idle animation
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            anim.SetTrigger("IsRunning"); // Activamos el run animation
            anim.SetBool("Idle", false); // Desactivamos el idle animation
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(+moveSpeed, rb.velocity.y);
            anim.SetTrigger("IsRunning"); // Activamos el run animation
            anim.SetBool("Idle", false); // Desactivamos el idle animation
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
            float jumpForce = 2.75f;
            rb.velocity = Vector2.up * jumpForce;
            jump.Play(); // Jump sound
            //anim.SetBool("Idle", true);
        }
        else if(!IsGrounded())
        {
            anim.SetBool("Idle", false);
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0) 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }

    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        //Debug.Log("out");
        dust.Play();
    }
}
