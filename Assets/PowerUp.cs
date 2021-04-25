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
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healRatio = 1 + GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.healPowerUp;
        _interpolator.ToMax();
        randomObject = Random.Range(1, 3);
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
                if(player.GetComponent<playerBehaviour>()._playerLifes < 6)
                {
                    if (GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.lifes + healRatio >= 5)
                        GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.lifes = 5;
                    else
                        GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.lifes += healRatio;
                    
                }
               
            }
            else if (randomObject == 2) //ammo
            {
                SoundManagerScript.PlaySound("dropSound");
                if (handController.currentPos == 0) //purple
                {
                    if (player.GetComponent<playerBehaviour>().bulletsPurple < player.GetComponent<playerBehaviour>().MAX_PURPLE_SHOOT)
                    {
                        player.GetComponent<playerBehaviour>().bulletsPurple += 5;
                        if (player.GetComponent<playerBehaviour>().bulletsPurple > player.GetComponent<playerBehaviour>().MAX_PURPLE_SHOOT)
                        {
                            tempAmmo = player.GetComponent<playerBehaviour>().bulletsPurple - player.GetComponent<playerBehaviour>().MAX_PURPLE_SHOOT;
                            player.GetComponent<playerBehaviour>().bulletsPurple -= tempAmmo;
                            player.GetComponent<playerBehaviour>().reservedAmmoPurple += tempAmmo;

                            if (player.GetComponent<playerBehaviour>().reservedAmmoPurple > player.GetComponent<playerBehaviour>().MAX_BULLETS_PURPLE)
                            {
                                tempAmmo2 = player.GetComponent<playerBehaviour>().reservedAmmoPurple - player.GetComponent<playerBehaviour>().MAX_BULLETS_PURPLE;
                                player.GetComponent<playerBehaviour>().reservedAmmoPurple -= tempAmmo2;
                            }

                        }
                    }
                    else
                    {
                        player.GetComponent<playerBehaviour>().reservedAmmoPurple += 5;
                    }
                }
                else if (handController.currentPos == 1) //laser
                {
                    if (player.GetComponent<playerBehaviour>().bulletsYellow < player.GetComponent<playerBehaviour>().MAX_YELLOW_SHOOT)
                    {
                        player.GetComponent<playerBehaviour>().bulletsYellow += 5;
                        if (player.GetComponent<playerBehaviour>().bulletsYellow > player.GetComponent<playerBehaviour>().MAX_YELLOW_SHOOT)
                        {
                            tempAmmo = player.GetComponent<playerBehaviour>().bulletsYellow - player.GetComponent<playerBehaviour>().MAX_YELLOW_SHOOT;
                            player.GetComponent<playerBehaviour>().bulletsYellow -= tempAmmo;
                            player.GetComponent<playerBehaviour>().reservedAmmoYellow += tempAmmo;

                            if (player.GetComponent<playerBehaviour>().reservedAmmoYellow > player.GetComponent<playerBehaviour>().MAX_BULLETS_YELLOW)
                            {
                                tempAmmo2 = player.GetComponent<playerBehaviour>().reservedAmmoYellow - player.GetComponent<playerBehaviour>().MAX_BULLETS_YELLOW;
                                player.GetComponent<playerBehaviour>().reservedAmmoYellow -= tempAmmo2;
                            }
                        }
                    }
                    else
                    {
                        player.GetComponent<playerBehaviour>().reservedAmmoYellow += 5;
                    }
                }
                else if (handController.currentPos == 2) //shotgun
                {
                    if (player.GetComponent<playerBehaviour>().bulletsShotgun < player.GetComponent<playerBehaviour>().MAX_SHOTGUN_SHOOT)
                    {
                        player.GetComponent<playerBehaviour>().bulletsShotgun += 5;
                        if (player.GetComponent<playerBehaviour>().bulletsShotgun > player.GetComponent<playerBehaviour>().MAX_SHOTGUN_SHOOT)
                        {
                            tempAmmo = player.GetComponent<playerBehaviour>().bulletsShotgun - player.GetComponent<playerBehaviour>().MAX_SHOTGUN_SHOOT;
                            player.GetComponent<playerBehaviour>().bulletsShotgun -= tempAmmo;
                            player.GetComponent<playerBehaviour>().reservedAmmoShotgun += tempAmmo;

                            if (player.GetComponent<playerBehaviour>().reservedAmmoShotgun > player.GetComponent<playerBehaviour>().MAX_BULLETS_SHOTGUN)
                            {
                                tempAmmo2 = player.GetComponent<playerBehaviour>().reservedAmmoShotgun - player.GetComponent<playerBehaviour>().MAX_BULLETS_SHOTGUN;
                                player.GetComponent<playerBehaviour>().reservedAmmoShotgun -= tempAmmo2;
                            }
                        }
                    }
                    else
                    {
                        player.GetComponent<playerBehaviour>().reservedAmmoShotgun += 3;
                    }
                }
            }
            else if (randomObject == 3)// inmunity
            {
                player.GetComponent<playerBehaviour>().activePowerUp = true;
                SoundManagerScript.PlaySound("powerup");
            }
            Destroy(this.gameObject);
        }
    }
}
