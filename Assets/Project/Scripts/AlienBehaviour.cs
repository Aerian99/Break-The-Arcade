using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class AlienBehaviour : MonoBehaviour
{
    private Animator anim;
    public GameObject triggerZone;
    [HideInInspector] public GameObject[] hitDamagePopUp;
    private float actualHealth;
    private float maxHealth;
    public Image life;
    public LayerMask layer;
    private float cdExplosion, cdMaxExplosion;
    private Canvas canvas;
    public GameObject deathExplosion;
    private GameObject player;

    [HideInInspector] public bool laserDamage;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 15f;
        actualHealth = maxHealth;
        anim = GetComponent<Animator>();
        cdMaxExplosion = 4f;
        cdExplosion = cdMaxExplosion;
        canvas = transform.GetChild(1).GetComponent<Canvas>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (actualHealth <= 0)
        {
           
            Falling();

        }

        if (laserDamage)
        {
            anim.SetTrigger("hit");
            actualHealth -= LaserShoot.damage;
            life.fillAmount -= LaserShoot.damage / maxHealth;
            popUpDamage(LaserShoot.damage);
            laserDamage = false;
        }
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

    void popUpDamage(float hitdamage)
    {
        GameObject dmg = Instantiate(hitDamagePopUp[Random.Range(0, 4)], transform.position, Quaternion.identity);
        dmg.GetComponent<TextMeshPro>().text = "-" + hitdamage;
    }

    void Falling()
    {
        gameObject.tag = "Tower";
        GetComponentInChildren<AlienAttack>().enabled = false;
        this.gameObject.transform.parent = null;
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().gravityScale = 2.0f;
        if (cdExplosion <= 0.0f)
        {
            bool hasExploted = Physics2D.OverlapCircle(transform.position, 2, layer);

            if (hasExploted)
            {
                player.GetComponent<playerBehaviour>().activeImmunity = true;
            }
           
            Destroy(this.gameObject, 0.1f);
            GameObject explosionGO = Instantiate(deathExplosion, transform.position, Quaternion.identity);
            Destroy(explosionGO, 0.7f);
            //SoundManagerScript.PlaySound("alienExplosion");
        }

        cdExplosion -= Time.deltaTime;
        canvas.enabled = false;
    }

    private void OnDestroy()
    {
        triggerZone.GetComponent<triggerZone_1>().radialEnemy--;
        for (int i = 0; i < GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest.Length; i++)
        {
            if (GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest[i].typesOfMonsters == "Aliens")
            {
                GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest[i].actualMonstersKilled += 1;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (actualHealth <= 0)
        {
            anim.SetBool("dead", true);
        }
    }
}