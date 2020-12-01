using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletBehaviour : MonoBehaviour
{
    private float cdExplote, cdMaxExplote;
    private bool canExplote;
    private void Start()
    {
        cdMaxExplote = 3f;
        cdExplote = cdMaxExplote;
        canExplote = false;
    }

    private void FixedUpdate()
    {
        if (canExplote)
            cdExplote -= Time.fixedDeltaTime;

        if (cdExplote <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.tag != "AbsorbGun" && other.gameObject.tag != "Range")
        {
            if(playerBehaviour.canBeDamaged)
            { 
                playerBehaviour.activeImmunity = true;
                other.GetComponent<Animator>().SetTrigger("hit");
            }
            canExplote = true;
        }
        else if 
        (other.gameObject.tag != "Enemy" 
        && other.gameObject.tag != "PurpleBullet" 
        && other.gameObject.tag != "YellowBullet"
        && other.gameObject.tag != "RedBullet"
        && other.gameObject.tag != "EnemyBullet"
        && other.gameObject.tag != "Bullet Pacman"
        && other.gameObject.tag != "Range" 
        && other.gameObject.tag != "NPC" && other.gameObject.tag != "greyPlatform")
        {
            canExplote = true;
        }

       
    }
}
