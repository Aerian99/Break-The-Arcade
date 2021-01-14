using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb_Gun : MonoBehaviour
{
    private EdgeCollider2D arcCollider;
    private GameObject effect;
    public GameObject noAmmoText;
    public static bool firstTimeAbsorb0, firstTimeAbsorb1, firstTimeAbsorb2, ammoFull0, ammoFull1, ammoFull2;

    void Start()
    {
        arcCollider = GetComponent<EdgeCollider2D>();
        firstTimeAbsorb0 = firstTimeAbsorb1 = firstTimeAbsorb2 = true;
        ammoFull0 = ammoFull1= ammoFull2 = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "AlienAttack" || other.gameObject.tag == "Bullet Pacman")  && absorbCooldown.coolFull == false)
        {
            // ABSORB MAGNET EFFECT

            // CONTADOR DE BALAS
            if (firstTimeAbsorb0 && handController.currentPos == 0)
            {
                if (playerBehaviour.bulletsPurple < playerBehaviour.MAX_PURPLE_SHOOT)
                { 
                    playerBehaviour.bulletsPurple++;
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
                    playerBehaviour.bulletsYellow++;
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
                    playerBehaviour.bulletsShotgun++;
                    SoundManagerScript.PlaySound("absorbSound");
                }
                else
                {
                    firstTimeAbsorb2 = false;
                    ammoFull2 = true;
                }
                if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
            }

            if (ammoFull0 || ammoFull1 || ammoFull2)
            {
                if (handController.currentPos == 0 && playerBehaviour.reservedAmmoPurple != playerBehaviour.MAX_BULLETS_PURPLE)
                { 
                    playerBehaviour.reservedAmmoPurple++;
                    if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
                    SoundManagerScript.PlaySound("absorbSound");
                }
                if (handController.currentPos == 1 && playerBehaviour.reservedAmmoYellow != playerBehaviour.MAX_BULLETS_YELLOW)
                { 
                    playerBehaviour.reservedAmmoYellow++;
                    if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
                    SoundManagerScript.PlaySound("absorbSound");
                }
                if (handController.currentPos == 2 && playerBehaviour.reservedAmmoShotgun != playerBehaviour.MAX_BULLETS_SHOTGUN)
                { 
                    playerBehaviour.reservedAmmoShotgun++;
                    if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
                    SoundManagerScript.PlaySound("absorbSound");
                }
            }
            //Destroy(other.gameObject);
        }
    }
}
