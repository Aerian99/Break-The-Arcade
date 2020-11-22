using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class droneBehaviour : MonoBehaviour
{
    private Transform playerCharacter;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    public static bool canBeAttacked;
    private float boolCounter, boolMaxCounter;

    [HideInInspector] public GameObject[] hitDamagePopUp;
    private float actualHealth;
    private float maxHealth;
    public Image life;

    public void Awake()
    {
        playerCharacter = GameObject.FindWithTag("Player").transform;
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        maxHealth = 15f;
        actualHealth = maxHealth;
        life.fillAmount = actualHealth;
        boolCounter = 0f;
        boolMaxCounter = 5f;
        anim.enabled = true;
    }

    public void Update()
    {
        this.spriteRenderer.flipX = playerCharacter.transform.position.x > this.transform.position.x;
        if (actualHealth <= 0)
        {
            anim.SetBool("dead", true);
            Destroy(this.gameObject, 0.1f);
        }
        if (canBeAttacked)
            boolCounter -= Time.deltaTime;

        if (boolCounter <= 0f)
        {
            boolCounter = boolMaxCounter;
            canBeAttacked = false;
        }
        Hittable();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(canBeAttacked)
        { 
            if (other.gameObject.tag == "PurpleBullet")
            {
                anim.SetTrigger("hit");
                actualHealth -= PurpleShoot.bulletDamage;
                life.fillAmount -= PurpleShoot.bulletDamage / maxHealth;
                popUpDamage(PurpleShoot.bulletDamage);
            }
            else if (other.gameObject.tag == "YellowBullet")
            {
                anim.SetTrigger("hit");
                actualHealth -= YellowShoot.bulletDamage;
                life.fillAmount -= YellowShoot.bulletDamage / maxHealth;
                popUpDamage(YellowShoot.bulletDamage);
            }
            else if (other.gameObject.tag == "RedBullet")
            {
                anim.SetTrigger("hit");
                actualHealth -= RedShoot.bulletDamage;
                life.fillAmount -= RedShoot.bulletDamage / maxHealth;
                popUpDamage(RedShoot.bulletDamage);
            }
        }
    }

    void Hittable()
    {
        if (canBeAttacked)
        {
            anim.SetBool("hittable", true);
        }
        else
        {
            anim.SetBool("hittable", false);
        }
    }
    void popUpDamage(float hitdamage)
    {
        GameObject dmg = Instantiate(hitDamagePopUp[Random.Range(0,4)], transform.position, Quaternion.identity);
        dmg.GetComponent<TextMeshPro>().text = "-" + hitdamage;
    }
}