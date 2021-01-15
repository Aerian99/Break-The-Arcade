﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Sprite health, ammo, inmunity;
    int randomObject;
    public GameObject reloadText;

    private void Start()
    {
        randomObject = Random.Range(1, 4);
        if (randomObject == 1) //health
        {
            this.GetComponent<SpriteRenderer>().sprite = health;
        }
        else if (randomObject == 2) //ammo
        {
            this.GetComponent<SpriteRenderer>().sprite = ammo;
        }
        else if (randomObject == 3)// inmunity
        {
            this.GetComponent<SpriteRenderer>().sprite = inmunity;
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int tempAmmo = 0;

        if (collision.CompareTag("Player"))
        {
            if (reloadText.activeSelf == true)
            {
                reloadText.SetActive(false);
            }

            if (randomObject == 1) //health
            {
                if (playerBehaviour._playerLifes < 6)
                {
                    playerBehaviour._playerLifes += 1;
                }
                Debug.Log(playerBehaviour._playerLifes);
            }
            else if (randomObject == 2) //ammo
            {
                if (handController.currentPos == 0) //purple
                {
                    if (playerBehaviour.bulletsPurple < playerBehaviour.MAX_PURPLE_SHOOT)
                    {
                        playerBehaviour.bulletsPurple += 5;
                        if (playerBehaviour.bulletsPurple > playerBehaviour.MAX_PURPLE_SHOOT)
                        {
                            tempAmmo = playerBehaviour.bulletsPurple - playerBehaviour.MAX_PURPLE_SHOOT;
                            playerBehaviour.bulletsPurple -= tempAmmo;
                        }
                    }
                }
                else if (handController.currentPos == 1) //laser
                {
                    if (playerBehaviour.bulletsYellow < playerBehaviour.MAX_YELLOW_SHOOT)
                    {
                        playerBehaviour.bulletsYellow += 5;
                        if (playerBehaviour.bulletsYellow > playerBehaviour.MAX_YELLOW_SHOOT)
                        {
                            tempAmmo = playerBehaviour.bulletsYellow - playerBehaviour.MAX_YELLOW_SHOOT;
                            playerBehaviour.bulletsYellow -= tempAmmo;
                        }
                    }
                }
                else if (handController.currentPos == 2) //shotgun
                {
                    if (playerBehaviour.bulletsShotgun < playerBehaviour.MAX_SHOTGUN_SHOOT)
                    {
                        playerBehaviour.bulletsShotgun += 5;
                        if (playerBehaviour.bulletsShotgun > playerBehaviour.MAX_SHOTGUN_SHOOT)
                        {
                            tempAmmo = playerBehaviour.bulletsShotgun - playerBehaviour.MAX_SHOTGUN_SHOOT;
                            playerBehaviour.bulletsShotgun -= tempAmmo;
                        }
                    }
                }
            }
            else if (randomObject == 3)// inmunity
            {
                playerBehaviour.activeImmunity = true;
                playerBehaviour._playerLifes++;
            }
            Destroy(this.gameObject);
        }
    }
}
