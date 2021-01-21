using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ammoBoxScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
            Destroy(this.gameObject);
            
            if (handController.currentPos == 0)
            {
                playerBehaviour.bulletsPurple += 5;
            }

            if (handController.currentPos == 1)
            {
                playerBehaviour.bulletsYellow += 3;
            }

            if (handController.currentPos == 2)
            {
                playerBehaviour.bulletsShotgun += 1;
            }
        }
    }
}
