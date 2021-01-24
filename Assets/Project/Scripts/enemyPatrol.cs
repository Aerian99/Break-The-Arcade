using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    [HideInInspector] public float patrolSpeed;
    private float patrolDistance;
    private Rigidbody2D rb;
    private GameObject canvasGO;

    private bool movingRight = true;
    public Transform groundDetecion;
    private Animator anim;
    public LayerMask platformLayer;

    private Vector2 vecDir;

    private RaycastHit2D groundInfo;
    private RaycastHit2D groundInfo2;
    private RaycastHit2D groundInfo3;

    public GameObject bulletPrefab, bulletPacman;
    private float FireRate = 0.5f;
    private float NextTimeToFire = 1f;
    private float shootForce = 15f;

    private GameObject player;

    public float lifes;
    [HideInInspector] public bool isDying;
    float fade;
    public Material mat;

    GameObject bulletGO;
    
    public GameObject explosionEffect;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        patrolSpeed = 2f;
        patrolDistance = 1f;
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        lifes = 10f;
        fade = 1;
        isDying = false;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        groundInfo = Physics2D.Raycast(groundDetecion.position, Vector2.down, patrolDistance, platformLayer);
        changeDirection();
        triggerDetection();
        if (!GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().activatedAbsorb)
        {
            if (Time.time > NextTimeToFire)
            {
                if (dist < 13f)
                {
                    this.GetComponent<Rigidbody2D>().isKinematic = true;
                    if ((player.transform.position.x - this.transform.position.x) > 0 && movingRight)
                    {
                        Shoot();
                        patrolSpeed = 0f;
                        rb.velocity = Vector2.zero;
                        anim.SetBool("isRunning", false);
                    }

                    if ((player.transform.position.x - this.transform.position.x) < 0 && !movingRight)
                    {
                        Shoot();
                        patrolSpeed = 0f;
                        rb.velocity = Vector2.zero;
                        anim.SetBool("isRunning", false);
                    }
                }
                else
                {
                    patrolSpeed = 2f;
                    this.GetComponent<Rigidbody2D>().isKinematic = false;
                }
            }
        }

        if (lifes <= 0f)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isDead", true);
            
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<Collider2D>().enabled = false;
            //Dead();
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            SoundManagerScript.PlaySound("radialEnemyDeath");
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
        if (other.gameObject.CompareTag("Bullet"))
        {
            lifes -= 10f;
        }
    }

    void Shoot()
    {
        int rand = Random.Range(0, 100);

        if (rand < 30)
        {
            bulletGO = Instantiate(bulletPacman, this.transform.position, Quaternion.identity);
        }
        else
        {
            bulletGO = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        }

        bulletGO.GetComponent<Rigidbody2D>().AddForce(transform.right * shootForce, ForceMode2D.Impulse);
        NextTimeToFire = Time.time + FireRate;
    }

   /* void Dead()
    {
        mat.SetColor("_Color", new Color(0.1294118f, 0.5921569f, 0.8039216f));
        this.GetComponent<SpriteRenderer>().material = mat;
        isDying = true;
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        gameObject.GetComponent<Collider2D>().enabled = false;
        fade -= Time.deltaTime;
        mat.SetFloat("_Fade", fade);

        if (fade <= 0)
        {
            Destroy(this.gameObject);
        }
    }*/
}