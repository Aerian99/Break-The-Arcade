using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Timers;
using UnityEngine;
using UnityEngine.Serialization;

public class landCollision : MonoBehaviour
{
    // Si hace trigger con las plataformas de salto, aumentamos la velocidad del Rigidbody hacía arriba
    private float platformJumpForce = 30f;
    public static bool groundSuperJump;

    public GameObject landEffect;
    private GameObject landGameObject;

    private void Update()
    {
        Destroy(landGameObject, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            landGameObject = Instantiate(landEffect, this.transform.position, landEffect.transform.rotation);
            SoundManagerScript.PlaySound("landing");
        }
        else if (other.gameObject.tag == "JumpPlatform")
        {
            this.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.up * platformJumpForce;
            landGameObject = Instantiate(landEffect, this.transform.position, landEffect.transform.rotation);
            groundSuperJump = true;
            SoundManagerScript.PlaySound("platformSuperJump");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        groundSuperJump = false;
    }
}