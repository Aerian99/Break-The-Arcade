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
    public GameObject explosionEffect;

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
            if (gameObject.GetComponent<Animator>())
                gameObject.GetComponent<Animator>().SetTrigger("hit");
        }
    }
}