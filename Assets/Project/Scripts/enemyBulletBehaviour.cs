﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletBehaviour : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.tag != "AbsorbGun" && other.gameObject.tag != "Range")
        {
            playerBehaviour.activePostProcessing = true;
            playerBehaviour._playerLifes--;
            other.GetComponent<Animator>().SetTrigger("hit");
            Destroy(this.gameObject);
        }
        else if 
        (other.gameObject.tag != "Enemy" 
        && other.gameObject.tag != "PurpleBullet" 
        && other.gameObject.tag != "YellowBullet"
        && other.gameObject.tag != "RedBullet"
        && other.gameObject.tag != "EnemyBullet"
        && other.gameObject.tag != "Bullet Pacman"
        && other.gameObject.tag != "Range" 
        && other.gameObject.tag != "NPC")
        {
            Destroy(this.gameObject);
        }

       
    }
}
