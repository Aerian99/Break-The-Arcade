using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radialEnemyBehaviour : MonoBehaviour
{
    private float lifes;
    [HideInInspector] public bool isDying;
    float fade;
    Material mat;

    void Start()
    {
        lifes = 50f;
        mat = GetComponent<SpriteRenderer>().material;
        fade = 1;
        isDying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifes < 0f)
        {
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            lifes -= 10f;
        }
    }

    void getDamage(float dmg)
    {
    }

    void Dead()
    {
        isDying = true;
        //gameObject.GetComponent<FlyingBehaviour>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        gameObject.GetComponent<Collider2D>().enabled = false;
        fade -= Time.deltaTime;
        mat.SetFloat("_Fade", fade);
        if (fade <= 0)
        {
            Destroy(gameObject);
        }
    }
}