using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    private float patrolSpeed;
    private float patrolDistance;
    private Rigidbody2D rb;
    private GameObject canvasGO;

    private bool movingRight = true;
    public Transform groundDetecion;
    private Animator anim;

    private Vector2 vecDir;

    private RaycastHit2D groundInfo;
    private RaycastHit2D groundInfo2;
    private RaycastHit2D groundInfo3;

    public GameObject bulletPrefab;
    private float FireRate = 0.5f;
    private float NextTimeToFire = 1f;
    private float shootForce = 15f;

    private GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        patrolSpeed = 2f;
        patrolDistance = 1f;
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        groundInfo = Physics2D.Raycast(groundDetecion.position, Vector2.down, patrolDistance);
        changeDirection();
        triggerDetection();
        
        if (Time.time > NextTimeToFire)
        {
            if (dist < 8f)
            {
                if ((player.transform.position.x - this.transform.position.x) > 0 && movingRight)
                {
                    Debug.Log("DERECHA");
                    Shoot();
                    patrolSpeed = 0f;
                    rb.velocity = Vector2.zero;
                    anim.SetBool("isRunning", false);
                }

                if ((player.transform.position.x - this.transform.position.x) < 0 && !movingRight)
                {
                    Debug.Log("IZQUIERDA");
                    Shoot();
                    patrolSpeed = 0f;
                    rb.velocity = Vector2.zero;
                    anim.SetBool("isRunning", false);
                }
            }
            else
            {
                patrolSpeed = 2f; 
            }
        }
    }

    void changeDirection()
    {
        if (movingRight == true && patrolSpeed > 0)
        {
            rb.velocity = Vector2.right * patrolSpeed;
            anim.SetBool("isRunning", true);
        }
        else if (movingRight == false && patrolSpeed > 0)
        {
            rb.velocity = Vector2.left * patrolSpeed;
            anim.SetBool("isRunning", true);
        }
    }

    void triggerDetection()
    {
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                //canvasGO.transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                //canvasGO.transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    public bool getMovingRight()
    {
        return movingRight;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            anim.SetTrigger("hit");
        }
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        bulletGO.GetComponent<Rigidbody2D>().AddForce(transform.right * shootForce, ForceMode2D.Impulse);
        NextTimeToFire = Time.time + FireRate;
    }
}