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
            this.GetComponent<Animator>().SetBool("dead", true);
            Destroy(gameObject);
            //Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //lifes -= 10f;
        }
    }
    /*void Dead()
    {
        mat.SetColor("_Color", new Color(0.9960784f, 0.8f, 0.05490196f));
        this.GetComponent<SpriteRenderer>().material = mat;
        isDying = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        gameObject.GetComponent<Collider2D>().enabled = false;
        fade -= Time.deltaTime;
        mat.SetFloat("_Fade", fade);
        if (fade <= 0)
        {
            Destroy(gameObject);
        }
    }*/

  
}