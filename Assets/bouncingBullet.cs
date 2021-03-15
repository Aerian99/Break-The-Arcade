using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class bouncingBullet : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        Destroy(this.gameObject, 8f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.GetComponent<playerBehaviour>().canBeDamaged && player.GetComponent<playerBehaviour>().canBeDamagedPowerup && player.GetComponent<playerBehaviour>().cdImmunity >= player.GetComponent<playerBehaviour>().maxCdImmunity)
            {
                player.GetComponent<playerBehaviour>().activeImmunity = true;
                player.GetComponent<Animator>().SetTrigger("hit");
            }
            Destroy(this.gameObject);
        }
    }
}