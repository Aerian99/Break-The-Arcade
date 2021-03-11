using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private int healRatio;
    public Sprite health, ammo, inmunity;
    int randomObject;
    public GameObject reloadText;
    public bool pHealth, pAmmo, pInmunity;
    Interpolator _interpolator = new Interpolator(1f, Interpolator.Type.SMOOTH);
    private void Start()
    {
        healRatio = 1 + GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.healPowerUp;
        _interpolator.ToMax();
        randomObject = Random.Range(1, 4);
        if (pHealth)
            randomObject = 1;
        if (pAmmo)
            randomObject = 2;
        if (pInmunity)
            randomObject = 3;
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

    private void Update()
    {
        _interpolator.Update(Time.deltaTime);

        if(_interpolator.IsMaxPrecise)
            _interpolator.ToMin();
        else if (_interpolator.IsMinPrecise)
            _interpolator.ToMax();

        this.transform.position = 2 * Vector3.up * _interpolator.Value + Vector3.down;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int tempAmmo = 0;
        int tempAmmo2 = 0;

        if (collision.CompareTag("Player"))
        {
            if (reloadText.activeSelf == true)
            {
                reloadText.SetActive(false);
            }

            if (randomObject == 1) //health
            {
                SoundManagerScript.PlaySound("dropSound");
                playerBehaviour._playerLifes += healRatio;
               
            }
            else if (randomObject == 2) //ammo
            {
                SoundManagerScript.PlaySound("dropSound");
                if (handController.currentPos == 0) //purple
                {
                    if (playerBehaviour.bulletsPurple < playerBehaviour.MAX_PURPLE_SHOOT)
                    {
                        playerBehaviour.bulletsPurple += 5;
                        if (playerBehaviour.bulletsPurple > playerBehaviour.MAX_PURPLE_SHOOT)
                        {
                            tempAmmo = playerBehaviour.bulletsPurple - playerBehaviour.MAX_PURPLE_SHOOT;
                            playerBehaviour.bulletsPurple -= tempAmmo;
                            playerBehaviour.reservedAmmoPurple += tempAmmo;

                            if (playerBehaviour.reservedAmmoPurple > playerBehaviour.MAX_BULLETS_PURPLE)
                            {
                                tempAmmo2 = playerBehaviour.reservedAmmoPurple - playerBehaviour.MAX_BULLETS_PURPLE;
                                playerBehaviour.reservedAmmoPurple -= tempAmmo2;
                            }

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
                            playerBehaviour.reservedAmmoYellow += tempAmmo;

                            if (playerBehaviour.reservedAmmoYellow > playerBehaviour.MAX_BULLETS_YELLOW)
                            {
                                tempAmmo2 = playerBehaviour.reservedAmmoYellow - playerBehaviour.MAX_BULLETS_YELLOW;
                                playerBehaviour.reservedAmmoYellow -= tempAmmo2;
                            }
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
                            playerBehaviour.reservedAmmoShotgun += tempAmmo;

                            if (playerBehaviour.reservedAmmoShotgun > playerBehaviour.MAX_BULLETS_SHOTGUN)
                            {
                                tempAmmo2 = playerBehaviour.reservedAmmoShotgun - playerBehaviour.MAX_BULLETS_SHOTGUN;
                                playerBehaviour.reservedAmmoShotgun -= tempAmmo2;
                            }
                        }
                    }
                }
            }
            else if (randomObject == 3)// inmunity
            {
                playerBehaviour.activePowerUp = true;
                SoundManagerScript.PlaySound("powerup");
            }
            Destroy(this.gameObject);
        }
    }
}
