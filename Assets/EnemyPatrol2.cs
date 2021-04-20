using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol2 : MonoBehaviour
{
    public float lifes;
    
    private GameObject player;
    private Animator anim;
    private Rigidbody2D rb;

    private Transform firePoint;
    private GameObject bulletGO;
    public GameObject bulletPrefab;
    public GameObject explosionEffect;

    private bool movingRight = true;
    private Transform groundDetecion;
    public LayerMask platformLayer;
    private RaycastHit2D groundInfo;

    private float patrolDistance;
    private float patrolSpeed;
    
    private float FireRate = 0.3f;
    private float NextTimeToFire = 0.2f;
    private float shootForce = 50f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        groundDetecion = transform.GetChild(0);
        firePoint = transform.GetChild(1);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        patrolSpeed = 5f;
        patrolDistance = 1f;
        lifes = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        groundInfo = Physics2D.Raycast(groundDetecion.position, Vector2.right, patrolDistance, platformLayer);

        if (Vector2.Distance(player.transform.position, this.transform.position) < 15f)
        {
            changeDirection();
            triggerDetection();
            if ((player.transform.position.x - this.transform.position.x) > 0 && movingRight)
            {
                if (Time.time > NextTimeToFire)
                {
                    Shoot();
                }
                patrolSpeed = 0f;
                rb.velocity = Vector2.zero;
                anim.SetTrigger("FlyDown");
                anim.SetBool("Flying", false);
            }

            if ((player.transform.position.x - this.transform.position.x) < 0 && !movingRight)
            {
                if (Time.time > NextTimeToFire)
                {
                    Shoot();
                }
                patrolSpeed = 0f;
                rb.velocity = Vector2.zero;
                anim.SetTrigger("FlyDown");
                anim.SetBool("Flying", false);
            }
            //anim.SetTrigger("FlyDown");
            //anim.SetBool("Flying", false);
            //rb.velocity = new Vector2(0, rb.velocity.y);
            //patrolSpeed = 0f;
            

            this.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        else
        {

            anim.SetTrigger("FlyUp");
            changeDirection();
            triggerDetection();
            rb.gravityScale = 1f;
            patrolSpeed = 5f;
            this.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        
        if (lifes <= 0f)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            SoundManagerScript.PlaySound("patrolEnemyDeath");
        }
    }

    void flyUpEnd()
    {
        anim.SetBool("Flying", true);
        rb.gravityScale = 0f;
    }

    void changeDirection()
    {
        if (movingRight == true && patrolSpeed > 0)
        {
            rb.velocity = Vector2.right * patrolSpeed;
            //anim.SetBool("isRunning", true);
        }
        else if (movingRight == false && patrolSpeed > 0)
        {
            rb.velocity = Vector2.left * patrolSpeed;
            //anim.SetBool("isRunning", true);
        }
        //else
        //{
        //    anim.SetBool("isRunning", false);
        //}
    }

    void triggerDetection()
    {
        if (groundInfo.collider == true)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
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
        if (other.gameObject.CompareTag("PurpleBullet") || other.gameObject.CompareTag("RedBullet"))
        {
            GetComponent<Animator>().SetTrigger("hit");
        }
    }

    void Shoot()
    {
      
            bulletGO = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
            bulletGO.GetComponent<Rigidbody2D>().AddForce(transform.right * shootForce, ForceMode2D.Impulse);
            bulletGO.GetComponent<MoveSinus>().top = true;
            NextTimeToFire = Time.time + FireRate;
            bulletGO = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
            bulletGO.GetComponent<Rigidbody2D>().AddForce(transform.right * shootForce, ForceMode2D.Impulse);
            bulletGO.GetComponent<MoveSinus>().top = false;
            NextTimeToFire = Time.time + FireRate;
       
    }
}