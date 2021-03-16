using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class bouncingBullet : MonoBehaviour
{
    private GameObject player;
    public bool absorbed;

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
            if (player.GetComponent<playerBehaviour>().canBeDamaged && player.GetComponent<playerBehaviour>().canBeDamagedPowerup && player.GetComponent<playerBehaviour>().cdImmunity >= player.GetComponent<playerBehaviour>().maxCdImmunity && !absorbed)
            {
                player.GetComponent<playerBehaviour>().activeImmunity = true;
                player.GetComponent<Animator>().SetTrigger("hit");
            }
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("absorbZone"))
        {
            absorbed = true;
        }

        if (collision.gameObject.CompareTag("absorbPoint") && !collision.gameObject.CompareTag("AbsorbGun") &&
            !collision.gameObject.CompareTag("absorbZone")
        ) // Si la bala a entrado en la zona de absorción no puede hacer daño y ponemos "absorbed" a true.
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("absorbZone"))
        {
            absorbed = true;
        }

        if (collision.gameObject.CompareTag("absorbPoint") && !collision.gameObject.CompareTag("AbsorbGun") &&
            !collision.gameObject.CompareTag("absorbZone")
        ) // Si la bala a entrado en la zona de absorción no puede hacer daño y ponemos "absorbed" a true.
        {
            Destroy(this.gameObject);
        }
    }
}