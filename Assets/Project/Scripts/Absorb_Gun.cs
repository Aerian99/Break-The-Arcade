using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb_Gun : MonoBehaviour
{
    private EdgeCollider2D arcCollider;
    private GameObject effect;
    public GameObject noAmmoText;
    public static bool firstTimeAbsorb, firstTimeAbsorb0, firstTimeAbsorb1, firstTimeAbsorb2, ammoFull, ammoFull0, ammoFull1, ammoFull2;

    void Start()
    {
        arcCollider = GetComponent<EdgeCollider2D>();
        firstTimeAbsorb = firstTimeAbsorb0 = firstTimeAbsorb1 = firstTimeAbsorb2 = true;
        ammoFull = ammoFull0 = ammoFull1 = ammoFull2 = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "AlienAttack" || other.gameObject.tag == "Bullet Pacman") && absorbCooldown.coolFull == false)
        {
            //si nos quedamos con esta version, podemos borrar los otros firstTimeAbsorb
            if (firstTimeAbsorb)
            {
                if (playerBehaviour.bulletsPurple < playerBehaviour.MAX_PURPLE_SHOOT)
                {
                    playerBehaviour.bulletsPurple += 5;
                }
                if (playerBehaviour.bulletsYellow < playerBehaviour.MAX_YELLOW_SHOOT)
                {
                    playerBehaviour.bulletsYellow += 5;
                }
                if (playerBehaviour.bulletsShotgun < playerBehaviour.MAX_SHOTGUN_SHOOT)
                {
                    playerBehaviour.bulletsShotgun += 2;
                }

                SoundManagerScript.PlaySound("absorbSound");

                if (playerBehaviour.bulletsPurple >= playerBehaviour.MAX_PURPLE_SHOOT || playerBehaviour.bulletsYellow >= playerBehaviour.MAX_YELLOW_SHOOT)
                {
                    firstTimeAbsorb = false;
                    ammoFull = true;
                }

                if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
            }



            /*if (firstTimeAbsorb0 && handController.currentPos == 0)
            {
                if (playerBehaviour.bulletsPurple < playerBehaviour.MAX_PURPLE_SHOOT)
                {
                    playerBehaviour.bulletsPurple += 5;
                    SoundManagerScript.PlaySound("absorbSound");
                }
                else
                {
                    firstTimeAbsorb0 = false;
                    ammoFull0 = true;
                }

                if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
            }

            else if (firstTimeAbsorb1 && handController.currentPos == 1)
            {
                if (handController.currentPos == 1 && playerBehaviour.bulletsYellow < playerBehaviour.MAX_YELLOW_SHOOT)
                { 
                    playerBehaviour.bulletsYellow += 3;
                    SoundManagerScript.PlaySound("absorbSound");
                }
                else
                {
                    firstTimeAbsorb1 = false;
                    ammoFull1 = true;
                }
                if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
            }

            else if (firstTimeAbsorb2 && handController.currentPos == 2)
            {
                if (handController.currentPos == 2 && playerBehaviour.bulletsShotgun < playerBehaviour.MAX_SHOTGUN_SHOOT)
                { 
                    playerBehaviour.bulletsShotgun +=2;
                    SoundManagerScript.PlaySound("absorbSound");
                }
                else
                {
                    firstTimeAbsorb2 = false;
                    ammoFull2 = true;
                }
                if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
            }*/
            if (ammoFull)
            {
                if (playerBehaviour.reservedAmmoPurple < playerBehaviour.MAX_BULLETS_PURPLE)
                {
                    playerBehaviour.reservedAmmoPurple += 5;
                }
                if (playerBehaviour.reservedAmmoYellow < playerBehaviour.MAX_BULLETS_YELLOW)
                {
                    playerBehaviour.reservedAmmoYellow += 5;
                }
                if (playerBehaviour.reservedAmmoShotgun < playerBehaviour.MAX_BULLETS_SHOTGUN)
                {
                    playerBehaviour.reservedAmmoShotgun += 2;
                }
                if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);

                SoundManagerScript.PlaySound("absorbSound");
            }
            /* if (ammoFull0 || ammoFull1 || ammoFull2)
             {
                 Debug.Log("ESTOY LLENO");
                 if (handController.currentPos == 0 && playerBehaviour.reservedAmmoPurple < playerBehaviour.MAX_BULLETS_PURPLE)
                 {
                     playerBehaviour.reservedAmmoPurple +=5;
                     if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
                     SoundManagerScript.PlaySound("absorbSound");
                 }
                 if (handController.currentPos == 1 && playerBehaviour.reservedAmmoYellow < playerBehaviour.MAX_BULLETS_YELLOW)
                 {
                     playerBehaviour.reservedAmmoYellow +=3;
                     if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
                     SoundManagerScript.PlaySound("absorbSound");
                 }
                 if (handController.currentPos == 2 && playerBehaviour.reservedAmmoShotgun < playerBehaviour.MAX_BULLETS_SHOTGUN)
                 {
                     playerBehaviour.reservedAmmoShotgun +=2;
                     if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
                     SoundManagerScript.PlaySound("absorbSound");
                 }
             }*/
            //Destroy(other.gameObject);
        }
    }
}
