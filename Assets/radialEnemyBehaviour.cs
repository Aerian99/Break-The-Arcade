﻿using System;
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
            if(gameObject.name == "4_Enemy(Clone)")
            {
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
}