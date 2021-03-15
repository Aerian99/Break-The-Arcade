using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ammoBoxScript : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManagerScript.PlaySound("dropSound");
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
            Destroy(this.gameObject);
            
            if (handController.currentPos == 0)
            {
                player.GetComponent<playerBehaviour>().bulletsPurple += 5;
            }

            if (handController.currentPos == 1)
            {
                player.GetComponent<playerBehaviour>().bulletsYellow += 3;
            }

            if (handController.currentPos == 2)
            {
                player.GetComponent<playerBehaviour>().bulletsShotgun += 1;
            }
        }
    }
}
