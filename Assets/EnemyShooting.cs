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
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        shootCounter = 2; //added
        bulletSpeed = 2.5f;
        cadency = 0;
        keepCadency = 1;
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
                GameObject bullet;
                Rigidbody2D rb;

                SoundManagerScript.PlaySound("EnemyShoot");
                if (Random.Range(0f, 100f) <= 35.0f)
                {
                    bullet = Instantiate(enemyBulletAttack, this.transform.position, this.transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                }
                else
                {
                    bullet = Instantiate(enemyBullet, this.transform.position, this.transform.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                }
                target = GameObject.FindGameObjectWithTag("Player").transform;  
                rb.AddForce(-target.position * bulletSpeed, ForceMode2D.Impulse);
                shootCounter--;
                cadency = keepCadency;
            }
            else
            {
                cadency -= Time.fixedDeltaTime;
            }
        }
    }
}
