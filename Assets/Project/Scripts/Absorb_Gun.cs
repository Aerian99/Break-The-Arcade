using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb_Gun : MonoBehaviour
{
    private EdgeCollider2D arcCollider;
    public GameObject arcEffect;
    private Transform arcTransform;
    private GameObject effect;
    private bool firstTimeAbsorb0, firstTimeAbsorb1, firstTimeAbsorb2, ammoFull0, ammoFull1, ammoFull2;

    void Start()
    {
        arcCollider = GetComponent<EdgeCollider2D>();
        arcTransform = transform.GetChild(0).transform;
        firstTimeAbsorb0 = firstTimeAbsorb1 = firstTimeAbsorb2 = true;
        ammoFull0 = ammoFull1= ammoFull2 = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyBullet" && absorbCooldown.coolFull == false)
        {

            if (firstTimeAbsorb0 && handController.currentPos == 0)
            {
                if (playerBehaviour.bulletsPurple < playerBehaviour.MAX_PURPLE_SHOOT)
                { playerBehaviour.bulletsPurple++; }
                else
                {
                    firstTimeAbsorb0 = false;
                    ammoFull0 = true;
                }                          
            }
            /*else if(!firstTimeAbsorb0)
            {
                if (handController.currentPos == 0 && playerBehaviour.bulletsPurple != playerBehaviour.MAX_PURPLE_SHOOT)
                { playerBehaviour.bulletsPurple++; }
                else
                {
                    ammoFull0 = true;
                }
            }*/

            else if (firstTimeAbsorb1 && handController.currentPos == 1)
            {
                if (handController.currentPos == 1 && playerBehaviour.bulletsYellow < playerBehaviour.MAX_YELLOW_SHOOT)
                { playerBehaviour.bulletsYellow++; }
                else
                {
                    firstTimeAbsorb1 = false;
                    ammoFull1 = true;
                }
            }

            /*else if(!firstTimeAbsorb1)
            {
                if (handController.currentPos == 1 && playerBehaviour.bulletsYellow != playerBehaviour.MAX_YELLOW_SHOOT)
                { playerBehaviour.bulletsYellow++; }
                else
                    ammoFull1 = true;
            }*/

            else if (firstTimeAbsorb2 && handController.currentPos == 2)
            {
                if (handController.currentPos == 2 && playerBehaviour.bulletsShotgun < playerBehaviour.MAX_SHOTGUN_SHOOT)
                { playerBehaviour.bulletsShotgun++; }
                else
                {
                    firstTimeAbsorb2 = false;
                    ammoFull2 = true;
                }
            }

            /*else if(!firstTimeAbsorb2)
            {
                if (handController.currentPos == 2 && playerBehaviour.bulletsShotgun != playerBehaviour.MAX_SHOTGUN_SHOOT)
                { playerBehaviour.bulletsShotgun++; }
                else
                    ammoFull2 = true;
            }*/

            if (ammoFull0 || ammoFull1 || ammoFull2)
            {
                if (handController.currentPos == 0 && playerBehaviour.reservedAmmoPurple != playerBehaviour.MAX_BULLETS_PURPLE)
                { playerBehaviour.reservedAmmoPurple++; }
                if (handController.currentPos == 1 && playerBehaviour.reservedAmmoYellow != playerBehaviour.MAX_BULLETS_YELLOW)
                { playerBehaviour.reservedAmmoYellow++; }
                if (handController.currentPos == 2 && playerBehaviour.reservedAmmoShotgun != playerBehaviour.MAX_BULLETS_SHOTGUN)
                { playerBehaviour.reservedAmmoShotgun++; }

            }

            Destroy(other.gameObject);
        }


        else if (other.gameObject.tag == "Bullet Pacman")
        {
            droneBehaviour.canBeAttacked = true;        
        }

    }
}
