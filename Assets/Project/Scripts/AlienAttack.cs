using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAttack : MonoBehaviour
{
    float cdShoot, maxCdShoot;
    public GameObject bullet;
    private Vector3 positionBulletPointer;
    private float force;
    void Start()
    {
        force = 30f;
        maxCdShoot = 1;
        cdShoot = maxCdShoot;
    }


    void FixedUpdate()
    {
        if (cdShoot <= 0)
        { 
            ShootVertically();
            cdShoot = maxCdShoot;
        }

        cdShoot -= Time.fixedDeltaTime;
    }
    private void ShootVertically()
    {
        GameObject rb;
        rb = Instantiate(bullet, this.transform.position, this.transform.rotation);
        rb.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -force), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
