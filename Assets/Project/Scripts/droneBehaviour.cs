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
    public static bool canBeAttacked, activeAttack, beHaunted;
    [HideInInspector] public bool Laserdamaged;
    private float boolCounter, boolMaxCounter, laserDamagecd, laserDamagecdMax;

    [HideInInspector] public GameObject[] hitDamagePopUp;
    private float actualHealth;
    private float maxHealth;
    public Image life;
    public GameObject bullet;

    [HideInInspector]public bool isDying;
    float fade;
    Material mat;

    public void Awake()
    {
        laserDamagecdMax = 0.5f;
        laserDamagecd = laserDamagecdMax;
        playerCharacter = GameObject.FindWithTag("Player").transform;
        mat = GetComponent<SpriteRenderer>().material;
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        maxHealth = 15f;
        actualHealth = maxHealth;
        life.fillAmount = actualHealth;
        boolMaxCounter = 5f;
        boolCounter = boolMaxCounter;
        anim.enabled = true;
        canBeAttacked = true;
        fade = 1;
        isDying = false;
    }

    public void Update()
    {
        if (actualHealth <= 0)
        {
            Dead();
        }
        else
        {
            /* if(canBeAttacked)
                 Attacked();
             else
                 boolCounter = boolMaxCounter; */
            if (Laserdamaged && laserDamagecd <= 0.0f)
            {
                anim.SetTrigger("hit");
                actualHealth -= LaserShoot.damage;
                life.fillAmount -= LaserShoot.damage / maxHealth;
                popUpDamage(LaserShoot.damage);
                laserDamagecd = laserDamagecdMax;
            }

            laserDamagecd -= Time.deltaTime;
            EscapeAnim();

            if (beHaunted)
            {
                Escape();
                bullet.SetActive(false);
            }
            else
            {
                boolCounter = boolMaxCounter;
                bullet.SetActive(true);
            }
        }
    }

    void Dead()
    {
        isDying = true;
        gameObject.GetComponent<FlyingBehaviour>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        gameObject.GetComponent<Collider2D>().enabled = false;
        fade -= Time.deltaTime;
        mat.SetFloat("_Fade", fade);
        if (fade <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
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

    void Attacked()
    {
        if (boolCounter <= 0f)
        {
            canBeAttacked = false;
        }

        boolCounter -= Time.deltaTime;
    }

    void EscapeAnim()
    {
        if (beHaunted)
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
        GameObject dmg = Instantiate(hitDamagePopUp[Random.Range(0, 4)], transform.position, Quaternion.identity);
        dmg.GetComponent<TextMeshPro>().text = "-" + hitdamage;
    }

    void Escape()
    {
        if (boolCounter <= 0f)
        {
            beHaunted = false;
        }

        boolCounter -= Time.deltaTime;
    }
}