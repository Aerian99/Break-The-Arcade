using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Timers;
using UnityEngine;

public class SuperJumpCollision : MonoBehaviour
{
    // Si hace trigger con las plataformas de salto, aumentamos la velocidad del Rigidbody hacía arriba
    private float platformJumpForce = 20f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "JumpPlatform")
        {
            this.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.up * platformJumpForce;
        }
    }
}