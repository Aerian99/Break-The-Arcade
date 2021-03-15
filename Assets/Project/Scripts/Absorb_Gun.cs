using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb_Gun : MonoBehaviour
{
    private EdgeCollider2D arcCollider;
    private GameObject effect, player;
    public GameObject noAmmoText;
    public static bool firstTimeAbsorb, firstTimeAbsorb0, firstTimeAbsorb1, firstTimeAbsorb2, ammoFull, ammoFull0, ammoFull1, ammoFull2;

    void Start()
    {
        arcCollider = GetComponent<EdgeCollider2D>();
        firstTimeAbsorb = firstTimeAbsorb0 = firstTimeAbsorb1 = firstTimeAbsorb2 = true;
        ammoFull = ammoFull0 = ammoFull1 = ammoFull2 = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "AlienAttack" || other.gameObject.tag == "Bullet Pacman") && absorbCooldown.coolFull == false)
        {
            //si nos quedamos con esta version, podemos borrar los otros firstTimeAbsorb
            if (firstTimeAbsorb)
            {
                if (player.GetComponent<playerBehaviour>().bulletsPurple < player.GetComponent<playerBehaviour>().MAX_PURPLE_SHOOT)
                {
                    player.GetComponent<playerBehaviour>().bulletsPurple += 5;
                }
                if (player.GetComponent<playerBehaviour>().bulletsYellow < player.GetComponent<playerBehaviour>().MAX_YELLOW_SHOOT)
                {
                    player.GetComponent<playerBehaviour>().bulletsYellow += 5;
                }
                if (player.GetComponent<playerBehaviour>().bulletsShotgun < player.GetComponent<playerBehaviour>().MAX_SHOTGUN_SHOOT)
                {
                    player.GetComponent<playerBehaviour>().bulletsShotgun += 2;
                }

                SoundManagerScript.PlaySound("absorbSound");

                if (player.GetComponent<playerBehaviour>().bulletsPurple >= player.GetComponent<playerBehaviour>().MAX_PURPLE_SHOOT || player.GetComponent<playerBehaviour>().bulletsYellow >= player.GetComponent<playerBehaviour>().MAX_YELLOW_SHOOT)
                {
                    firstTimeAbsorb = false;
                    ammoFull = true;
                }

                if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
            }
            if (ammoFull)
            {
                if (player.GetComponent<playerBehaviour>().reservedAmmoPurple < player.GetComponent<playerBehaviour>().MAX_BULLETS_PURPLE)
                {
                    player.GetComponent<playerBehaviour>().reservedAmmoPurple += 5;
                }
                if (player.GetComponent<playerBehaviour>().reservedAmmoYellow < player.GetComponent<playerBehaviour>().MAX_BULLETS_YELLOW)
                {
                    player.GetComponent<playerBehaviour>().reservedAmmoYellow += 5;
                }
                if (player.GetComponent<playerBehaviour>().reservedAmmoShotgun < player.GetComponent<playerBehaviour>().MAX_BULLETS_SHOTGUN)
                {
                    player.GetComponent<playerBehaviour>().reservedAmmoShotgun += 2;
                }
                if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);

                SoundManagerScript.PlaySound("absorbSound");
            }
        }
    }
}
