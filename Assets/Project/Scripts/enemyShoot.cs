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
    private float startTimeBtwShoots;
    private float cadency;
    private int shootCounter;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (this.gameObject.tag == "OrangeEnemy")
            shootCounter = 5;
        else if (this.gameObject.tag == "CyanEnemy")
            shootCounter = 3;
        else if (this.gameObject.tag == "RedEnemy")
            shootCounter = 8;
        bulletSpeed = 15f;
        cadency = Random.Range(2.0f, 3.5f);
        keepCadency = cadency;
        startTimeBtwShoots = 0.5f; // Rango aleatorio entre el disparo de los enemigos, así los disparos se independizan según el enemigo.
        timeBtwShoots = startTimeBtwShoots;
    }


    void FixedUpdate()
    { 
        cadency -= Time.fixedDeltaTime;
        RotateTowards(target.position);
        if(cadency <=0)
        { 
            if (timeBtwShoots <= 0)
            {
                ShootPlayer();
                shootCounter--;
                timeBtwShoots = startTimeBtwShoots;
            }
            else
            {
                timeBtwShoots -= Time.deltaTime;
            }

            if (shootCounter == 0)
            {
                if(this.gameObject.tag == "OrangeEnemy")
                    shootCounter = 5;
                else if (this.gameObject.tag == "CyanEnemy")
                    shootCounter = 3;
                else if (this.gameObject.tag == "RedEnemy")
                    shootCounter = 8;
                cadency = keepCadency;
            }
        }
    }
    private void ShootPlayer() // Función para disparar hacia la ultima dirección en el frame del jugador.
    {
        GameObject bullet;
        Rigidbody2D rb;
       if(!droneBehaviour.canBeAttacked)
       {
           SoundManagerScript.PlaySound("EnemyShoot");
            if (Random.Range(0f,100f) <= 35.0f)
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
    private void RotateTowards(Vector2 target) // funcion para rotar mirando al player
    {
        float offset = -90f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
