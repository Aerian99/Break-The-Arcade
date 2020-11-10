using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumpPlatform : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerMovement.jumpForce = 22.5f;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerMovement.jumpForce = 17f;
        }
    }
}
