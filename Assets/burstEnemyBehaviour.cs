using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class burstEnemyBehaviour : MonoBehaviour
{
    public float lifes;
    [HideInInspector] public bool isDying;
    float fade;
    public Material mat;
    public GameObject explosionEffect;
    
    public Material defaultMaterial;
    public Material hitMaterial;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lifes = 20f;
        fade = 1;
        isDying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {    
            if (lifes <= 0f)
            {
                GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().throwCoins("burstEnemy", this.gameObject);
                player.GetComponent<playerBehaviour>().cdShield = 0f;
                player.GetComponent<playerBehaviour>().shieldActivated = true;
            
                //gameObject.GetComponent<radialEnemyBounce>().enabled = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                gameObject.GetComponent<Collider2D>().enabled = false;
                //Dead();
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
                SoundManagerScript.PlaySound("radialEnemyDeath");
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PurpleBullet") || collision.gameObject.CompareTag("RedBullet"))
        {
            GetComponent<Animator>().SetTrigger("hit");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Platform")
        {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }
}