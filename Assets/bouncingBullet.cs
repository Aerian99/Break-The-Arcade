using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class bouncingBullet : MonoBehaviour
{

    private void Update()
    {
        Destroy(this.gameObject, 8f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (playerBehaviour.canBeDamaged && playerBehaviour.canBeDamagedPowerup && player.GetComponent<playerBehaviour>().cdImmunity >= player.GetComponent<playerBehaviour>().maxCdImmunity)
            {
                playerBehaviour.activeImmunity = true;
                player.GetComponent<Animator>().SetTrigger("hit");
            }
           //playerBehaviour.activeImmunity = true;
            Destroy(this.gameObject);
            //SoundManagerScript.PlaySound("alienExplosion");
        }
    }
}