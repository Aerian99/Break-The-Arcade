using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform target;
    public GameObject enemyBulletAttack, enemyBullet;
    private float bulletSpeed;

    [HideInInspector] public bool onArea = false;
    private float cadency; private int shootCounter; private float keepCadency;
    private float timeBtwShoots;
    public float startTimeBtwShoots;

    public List<GameObject> firePoints;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        shootCounter = 2; //added
        bulletSpeed = 15f;
        cadency = 0;
        keepCadency = 5;
        startTimeBtwShoots = 2f;
        timeBtwShoots = startTimeBtwShoots;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onArea)
        {
            if (cadency <= 0)
            {
                StartCoroutine(Shooting());
                cadency = keepCadency;
            }
            else
                cadency -= Time.fixedDeltaTime;

        }
    }


    IEnumerator Shooting()
    {
        GameObject bullet;
        Rigidbody2D rb;
        Vector2 moveDirection;
        moveDirection = (target.transform.position - transform.position).normalized * bulletSpeed;
        SoundManagerScript.PlaySound("EnemyShoot");
        if (Random.Range(0f, 100f) <= 35.0f)
        {
            bullet = Instantiate(enemyBulletAttack, firePoints[0].transform.position, firePoints[0].transform.localRotation);
            rb = bullet.GetComponent<Rigidbody2D>();
        }
        else
        {

            bullet = Instantiate(enemyBullet, firePoints[0].transform.position, firePoints[0].transform.localRotation);
            rb = bullet.GetComponent<Rigidbody2D>();
        }

        rb.AddForce(bullet.transform.right * bulletSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2f);

    }
}
