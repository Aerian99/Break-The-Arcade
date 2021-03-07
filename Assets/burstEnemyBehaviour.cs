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

    void Start()
    {
        lifes = 20f;
        fade = 1;
        isDying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifes <= 0f)
        {
            if (gameObject.GetComponent<radialEnemyShoot>() == true)
            {
                gameObject.GetComponent<radialEnemyShoot>().enabled = false;
            }
            if (gameObject.GetComponent<radialEnemyBounce>() == true)
            {
                gameObject.GetComponent<radialEnemyBounce>().enabled = false;
            }
            
            //gameObject.GetComponent<radialEnemyBounce>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<Collider2D>().enabled = false;
            //Dead();
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            SoundManagerScript.PlaySound("radialEnemyDeath");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PurpleBullet") || other.gameObject.CompareTag("RedBullet"))
        {
            GetComponent<Animator>().SetTrigger("hit");
        }
    }
}