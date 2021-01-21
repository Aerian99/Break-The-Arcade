using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class radialEnemyBehaviour : MonoBehaviour
{
    public float lifes;
    [HideInInspector] public bool isDying;
    float fade;
    public Material mat;
    public GameObject explosionEffect;

    void Start()
    {
        lifes = 50f;
        fade = 1;
        isDying = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (lifes <= 0f)
        {
            gameObject.GetComponent<radialEnemyShoot>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<Collider2D>().enabled = false;
            //Dead();
            GameObject explosionGO = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            SoundManagerScript.PlaySound("radialEnemyDeath");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PurpleBullet") || other.gameObject.CompareTag("RedBullet"))
        {
            //SoundManagerScript.PlaySound("radialEnemyHurt");
        }
    }
}