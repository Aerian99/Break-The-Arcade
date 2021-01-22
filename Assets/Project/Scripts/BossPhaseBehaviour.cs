using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab, bulletGrenade;
    private float speed;
    Rigidbody2D EnemyRB;
    GameObject bulletGO;
    public GameObject rightCheck, roofCheck, groundCheck;
    public LayerMask groundLayer;
    private bool facingRight = true, groundTouch, roofTouch, rightTouch;
    public float dirX = 1, DIRy = 0.25f;
    public float circleRadius;
    SpriteRenderer renderer;
    public GameObject sliderHealth;
    Animator anim;
    enum Phases { INITPHASE, PHASE2, PHASE3 }
    Phases phase;
    public int health, maxHealth;

    private Vector3 startPoint;

    public Transform firePoint;

    private float shootForce = 0f;

    // Start is called before the first frame update
    void Start()
    {
        sliderHealth = GameObject.Find("HealthbarAlternative");
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        speed = 750;
        EnemyRB = GetComponent<Rigidbody2D>();
        health = maxHealth = 1000;
        phase = Phases.INITPHASE;
        StartCoroutine(shootBehaviour());
    }

    // Update is called once per frame
    void Update()
    {
        if (health >= 0)
        {
            Movement();
        if (health < maxHealth / 4)
        {
            phase = Phases.PHASE3;
        }
        else if (health < maxHealth / 2)
        {
            phase = Phases.PHASE2;
        }
        else
        {
            phase = Phases.INITPHASE;
        }


        }
        else
        {
            anim.SetBool("dead", true);
            Destroy(gameObject, 1.8f);
        }
    }

    void Movement()
    {
        EnemyRB.velocity = new Vector2(dirX, DIRy) * speed * Time.deltaTime;
        HitDetection();
    }


    void HitDetection()
    {

        rightTouch = Physics2D.OverlapCircle(rightCheck.transform.position, circleRadius, groundLayer);
        roofTouch = Physics2D.OverlapCircle(roofCheck.transform.position, circleRadius, groundLayer);
        groundTouch = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);
        HitLogic();

    }

    void HitLogic()
    {
        if (rightTouch && facingRight)
        {
            Flip();
        }
        else if (rightTouch && !facingRight)
        {
            Flip();
        }
        if (roofTouch)
        {
            DIRy = -0.25F;
        }
        else if (groundTouch)
        {
            DIRy = 0.25f;
        }



    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        dirX = -dirX;
    }
    IEnumerator shootBehaviour()
    {
        while (true)
        {
            if (phase == Phases.INITPHASE)
            {
                int rand = Random.Range(0,2);

                if (rand == 0)
                {
                    anim.SetBool("Attack", true);
                    ShootRadial(30, 10f, 20);
                }
                else
                {
                    anim.SetBool("Attack", true);
                    ShootGrenade();
                }

                yield return new WaitForSeconds(3);
            }
            else if (phase == Phases.PHASE2)
            {
                renderer.color = Color.yellow;
                speed = 1000;
                //Shoot
                int rand = Random.Range(0, 2);

                if (rand == 0)
                {
                    StartCoroutine(ShootRadial(30, 10f, 20, 2));
                }
                else
                {
                    StartCoroutine(ShootGrenade(2));
                }
                yield return new WaitForSeconds(5f);
            }
            else if (phase == Phases.PHASE3)
            {
                renderer.color = Color.red;
                int rand = Random.Range(0, 2);
                speed = 1250;
                //Shoot
                if (rand == 0)
                {
                    StartCoroutine(ShootRadial(30, 10f, 20, 3));
                }
                else
                {
                    StartCoroutine(ShootGrenade(4));
                }
                //Shoot

                yield return new WaitForSeconds(6f);
            }

        }

    }

    void ShootRadial(int numBullets, float radius, int bulletSpeed)
    {
        
        startPoint = transform.position;
        float angleStep = 360f / numBullets;
        float angle = 0f;
        for (int i = 0; i <= numBullets - 1; i++)
        {
            float bulletDirXPosition = startPoint.x + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
            float bulletDirYPosition = startPoint.y + Mathf.Sin((angle * Mathf.PI) / 180) * radius;

            Vector3 bulletVector = new Vector3(bulletDirXPosition, bulletDirYPosition, 0);
            Vector3 bulletMoveDirection = (bulletVector - startPoint).normalized * bulletSpeed;
            
            bulletGO = Instantiate(bulletPrefab, startPoint, Quaternion.identity);

            bulletGO.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletMoveDirection.x, bulletMoveDirection.y, 0);
            bulletGO.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Rotamos el gameobject en función de su dirección.
            Destroy(bulletGO, 2f);
            angle += angleStep;
        }
    }

    IEnumerator ShootRadial(int numBullets, float radius, int bulletSpeed, int nIterations)
    {
        anim.SetBool("AttackHard", true);
        yield return new WaitForSeconds(1f);
        startPoint = transform.position;
        float angleStep = 360f / numBullets;
        float angle = 0f;
        for (int y = 0; y < nIterations; y++)
        {
            for (int i = 0; i <= numBullets - 1; i++)
            {
                float bulletDirXPosition = startPoint.x + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
                float bulletDirYPosition = startPoint.y + Mathf.Sin((angle * Mathf.PI) / 180) * radius;

                Vector3 bulletVector = new Vector3(bulletDirXPosition, bulletDirYPosition, 0);
                Vector3 bulletMoveDirection = (bulletVector - startPoint).normalized * bulletSpeed;

                Debug.Log(bulletMoveDirection);
                bulletGO = Instantiate(bulletPrefab, startPoint, Quaternion.identity);

                bulletGO.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletMoveDirection.x, bulletMoveDirection.y, 0);
                bulletGO.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Rotamos el gameobject en función de su dirección.
                Destroy(bulletGO, 2f);
                angle += angleStep;
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0);
    }

    void ShootGrenade()
    {
        bulletGO = Instantiate(bulletGrenade, firePoint.position, transform.rotation);
        bulletGO.GetComponent<Rigidbody2D>().AddForce(-transform.up * shootForce, ForceMode2D.Impulse);
        bulletGO.GetComponent<GrenadeShoot>().bulletSpeed = 20;
        bulletGO.GetComponent<GrenadeShoot>().numBullets = 16;
    }
    IEnumerator ShootGrenade(int nIterations)
    {
        anim.SetBool("AttackHard", true);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < nIterations; i++)
        {
            bulletGO = Instantiate(bulletGrenade, firePoint.position, transform.rotation);
            bulletGO.GetComponent<Rigidbody2D>().AddForce(-transform.up * shootForce, ForceMode2D.Impulse);
            bulletGO.GetComponent<GrenadeShoot>().bulletSpeed = 20;
            bulletGO.GetComponent<GrenadeShoot>().numBullets = 16;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
