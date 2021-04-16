using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class demoEnemyBehaviour : MonoBehaviour
{
    public float lifes;
    [HideInInspector] public bool isDying;
    float fade;
    public Material mat;
    public GameObject explosionEffect;
    
    public Material defaultMaterial;
    public Material hitMaterial;
    
    private Animator anim;
    
    private bool activationAttack;
    private bool triggerAttack;

    private GameObject player;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        
        lifes = 20f;
        fade = 1;
        isDying = false;
        activationAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        activateAttack();
        
        if (lifes <= 0f)
        {
            if(gameObject.name == "4_Enemy(Clone)")
            {
                GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().throwCoins("radialEnemy", this.gameObject);
                for (int i = 0; i < GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest.Length; i++)
                {
                    if (GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest[i].typesOfMonsters == "Rotators")
                    {
                        GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest[i].actualMonstersKilled += 1;
                    }

                }
            }
            if (gameObject.GetComponent<radialEnemyShoot>() == true)
            {
                gameObject.GetComponent<radialEnemyShoot>().enabled = false;
                for (int i = 0; i < GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest.Length; i++)
                {
                    if(GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest[i].typesOfMonsters == "Radials")
                    {
                        GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest[i].actualMonstersKilled += 1;
                    }

                }
            }
            if (gameObject.GetComponent<PatrolTop>() == true)
            {
                GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().throwCoins("patrolTop", this.gameObject);
                gameObject.GetComponent<PatrolTop>().enabled = false;
                for (int i = 0; i < GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest.Length; i++)
                {
                    if (GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest[i].typesOfMonsters == "Roof Patrols")
                    {
                        GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest[i].actualMonstersKilled += 1;
                    }

                }
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


    void activateAttack()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) < 10)
        {
            activationAttack = true;
        }
        else
        {
            activationAttack = false;
        }
        
        if (activationAttack)
        {
            anim.SetBool("AttackReady", true);
            
            if (triggerAttack)
            {
                this.transform.GetChild(0).GetComponent<demoEnemyShoot>().enabled = true;
            }
        }
        else
        {
            anim.SetBool("AttackReady", false);
            if (!triggerAttack)
            {
                this.transform.GetChild(0).GetComponent<demoEnemyShoot>().enabled = false;
            }
        }
    }

    public void setTriggerAttack()
    {
        triggerAttack = true;
    }
    public void setTriggerAttackOff()
    {
        triggerAttack = false;
    }
}