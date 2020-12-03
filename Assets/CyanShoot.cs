﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyanShoot : MonoBehaviour
{
    // COMPONENTES
    private Transform target;
    public GameObject enemyBullet, enemyBulletAttack;
    private float keepCadency;


    // BULLET 
    private float bulletSpeed;
    private float timeBtwShoots;
    private float startTimeBtwShoots;
    private float cadency;
    private int shootCounter;
    public float playerRange;
    public LayerMask playerLayer;

    // ShootPoint transforms
    public Transform /*leftUP, left, leftDOWN, down, */left;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (this.gameObject.tag == "CyanEnemy")
        {
            shootCounter = 3;
            bulletSpeed = 8f;
            cadency = 0;
            startTimeBtwShoots = 1f;
        }

        keepCadency = 1;

        timeBtwShoots = startTimeBtwShoots;
    }


    void FixedUpdate()
    {
        Debug.Log(cadency);
        cadency -= Time.fixedDeltaTime;
        if (!this.gameObject.CompareTag("CyanEnemy"))
        {
            RotateTowards(target.position);
        }
        if (cadency <= 0)
        {
                //ShootPlayer();
                orangeShoot();
                shootCounter--;
                cadency = keepCadency;
        }
    }

    private void ShootPlayer() // Función para disparar hacia la ultima dirección en el frame del jugador.
    {
        GameObject bullet;
        Rigidbody2D rb;
        bool canShoot = false;
        bool inRange = false;
        if (gameObject.GetComponentInParent<FlyingBehaviour>() != null)
        {
            canShoot = gameObject.GetComponentInParent<FlyingBehaviour>().inRange;
        }

        if (!droneBehaviour.canBeAttacked && canShoot)
        {
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

            rb.AddForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        else if (gameObject.GetComponentInParent<FlyingBehaviour>() == null)
        {
            inRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);
            if (inRange)
            {
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

                rb.AddForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void RotateTowards(Vector2 target) // funcion para rotar mirando al player
    {
        float offset = -90f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    void orangeShoot()
    {
        if (GetComponentInParent<FlyingBehaviour>().inRange)
        {
          
            SoundManagerScript.PlaySound("EnemyShoot");
            // UP 

            // RIGHT DOWN
            GameObject bulletRightDOWN;
            Rigidbody2D rbRightDOWN;
            bulletRightDOWN = Instantiate(enemyBullet, left.transform.position, left.transform.localRotation);
            rbRightDOWN = bulletRightDOWN.GetComponent<Rigidbody2D>();
            rbRightDOWN.AddForce(this.transform.position * bulletSpeed, ForceMode2D.Impulse);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, GetComponentInParent<FlyingBehaviour>().playerRange);
    }
}