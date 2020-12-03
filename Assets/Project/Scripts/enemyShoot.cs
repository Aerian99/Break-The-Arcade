﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    // COMPONENTES
    private Transform target;
    public GameObject enemyBullet, enemyBulletAttack;
    private float keepCadency;


    // BULLET 
    private float bulletSpeed;
    private float timeBtwShoots;
    public float startTimeBtwShoots;
    private float cadency;
    private int shootCounter;
    public float playerRange;
    public LayerMask playerLayer;
    
    // ShootPoint transforms
    public Transform leftUP, left, leftDOWN, down, rightDOWN, right, rightUP;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        /*if (this.gameObject.tag == "OrangeEnemy")
            shootCounter = 3;
        else if (this.gameObject.tag == "CyanEnemy")
            shootCounter = 2;
        if (this.gameObject.tag == "RedEnemy")
            shootCounter = 2;*/

       /* if (this.gameObject.tag == "OrangeEnemy")
        {
            bulletSpeed = 2f;
            cadency = 4;
            startTimeBtwShoots = 2f;
        }
        else if (this.gameObject.tag == "CyanEnemy")
        {

            bulletSpeed = 30.0f;
            cadency = 2;
            startTimeBtwShoots = 0.7f;
        }*/
        /*else*/ if (this.gameObject.tag == "RedEnemy")
        {
            shootCounter = 2; //added
            bulletSpeed = 8f;
            cadency = 1;
        }

        keepCadency = cadency;

        timeBtwShoots = startTimeBtwShoots;
    }


    void FixedUpdate()
    {
        cadency -= Time.fixedDeltaTime;
        if (!this.gameObject.CompareTag("RedEnemy"))
        {
            RotateTowards(target.position);
        }
        if (cadency <= 0)
        {
            if (timeBtwShoots <= 0)
            {
                //ShootPlayer();
                redShoot();
                shootCounter--;
                timeBtwShoots = startTimeBtwShoots;
            }
            else
            {
                timeBtwShoots -= Time.deltaTime;
            }

            /*if (shootCounter == 0)
            {
                if (this.gameObject.tag == "OrangeEnemy")
                    shootCounter = 5;
                else if (this.gameObject.tag == "CyanEnemy")
                    shootCounter = 3;
                else if (this.gameObject.tag == "RedEnemy")
                    shootCounter = 8;
                cadency = keepCadency;
            }*/
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
        Vector2 direction = target - (Vector2) transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    void redShoot()
    {
        SoundManagerScript.PlaySound("EnemyShoot");
        // UP 
        GameObject bulletUP;
        Rigidbody2D rbUP;
        if (Random.Range(0f, 100f) <= 10.0f)
        {
            bulletUP = Instantiate(enemyBulletAttack, this.transform.position, this.transform.rotation);
            rbUP = bulletUP.GetComponent<Rigidbody2D>();
            rbUP.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }

        else
        {
            bulletUP = Instantiate(enemyBullet, this.transform.position, this.transform.rotation);
            rbUP = bulletUP.GetComponent<Rigidbody2D>();
            rbUP.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);

        }
        
        // LEFT UP
        GameObject bulletLeftUP;
        Rigidbody2D rbLeftUP;
        if (Random.Range(0f, 100f) <= 10.0f)
        {
            bulletLeftUP = Instantiate(enemyBulletAttack, leftUP.transform.position, leftUP.transform.localRotation);
            rbLeftUP = bulletLeftUP.GetComponent<Rigidbody2D>();
            rbLeftUP.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }

        else {
            bulletLeftUP = Instantiate(enemyBullet, leftUP.transform.position, leftUP.transform.localRotation);
            rbLeftUP = bulletLeftUP.GetComponent<Rigidbody2D>();
            rbLeftUP.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        
        // LEFT 
        GameObject bulletLeft;
        Rigidbody2D rbLeft;
        if (Random.Range(0f, 100f) <= 10.0f)
        {
            bulletLeft = Instantiate(enemyBulletAttack, left.transform.position, left.transform.localRotation);
            rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
            rbLeft.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        else {
            bulletLeft = Instantiate(enemyBullet, left.transform.position, left.transform.localRotation);
            rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
            rbLeft.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        
        // LEFT DOWN
        GameObject bulletLeftDOWN;
        Rigidbody2D rbLeftDOWN;
        if (Random.Range(0f, 100f) <= 30.0f)
        {
            bulletLeftDOWN = Instantiate(enemyBulletAttack, leftDOWN.transform.position, leftDOWN.transform.localRotation);
            rbLeftDOWN = bulletLeftDOWN.GetComponent<Rigidbody2D>();
            rbLeftDOWN.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }

        else
        {
            bulletLeftDOWN = Instantiate(enemyBullet, leftDOWN.transform.position, leftDOWN.transform.localRotation);
            rbLeftDOWN = bulletLeftDOWN.GetComponent<Rigidbody2D>();
            rbLeftDOWN.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        // DOWN
        GameObject bulletDOWN;
        Rigidbody2D rbDOWN;
        if (Random.Range(0f, 100f) <= 30.0f)
        {
            bulletDOWN = Instantiate(enemyBulletAttack, down.transform.position, down.transform.localRotation);
            rbDOWN = bulletDOWN.GetComponent<Rigidbody2D>();
            rbDOWN.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }

        else
        {
            bulletDOWN = Instantiate(enemyBullet, down.transform.position, down.transform.localRotation);
            rbDOWN = bulletDOWN.GetComponent<Rigidbody2D>();
            rbDOWN.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        
        // RIGHT DOWN
        GameObject bulletRightDOWN;
        Rigidbody2D rbRightDOWN;
        if (Random.Range(0f, 100f) <= 50.0f)
        {
            bulletRightDOWN = Instantiate(enemyBulletAttack, rightDOWN.transform.position, rightDOWN.transform.localRotation);
            rbRightDOWN = bulletRightDOWN.GetComponent<Rigidbody2D>();
            rbRightDOWN.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        else {
            bulletRightDOWN = Instantiate(enemyBullet, rightDOWN.transform.position, rightDOWN.transform.localRotation);
            rbRightDOWN = bulletRightDOWN.GetComponent<Rigidbody2D>();
            rbRightDOWN.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        
        // RIGHT
        GameObject bulletRight;
        Rigidbody2D rbRight;
        if (Random.Range(0f, 100f) <= 40.0f)
        {
            bulletRight = Instantiate(enemyBulletAttack, right.transform.position, right.transform.localRotation);
            rbRight = bulletRight.GetComponent<Rigidbody2D>();
            rbRight.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        else {
            bulletRight = Instantiate(enemyBullet, right.transform.position, right.transform.localRotation);
            rbRight = bulletRight.GetComponent<Rigidbody2D>();
            rbRight.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        
        // RIGHT UP
        GameObject bulletRightUP;
        Rigidbody2D rbRightUP;
        if (Random.Range(0f, 100f) <= 10.0f)
        {
            bulletRightUP = Instantiate(enemyBulletAttack, rightUP.transform.position, rightUP.transform.localRotation);
            rbRightUP = bulletRightUP.GetComponent<Rigidbody2D>();
            rbRightUP.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        else {
            bulletRightUP = Instantiate(enemyBullet, rightUP.transform.position, rightUP.transform.localRotation);
            rbRightUP = bulletRightUP.GetComponent<Rigidbody2D>();
            rbRightUP.AddRelativeForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        
    }
}