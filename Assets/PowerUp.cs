using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Sprite health, ammo, inmunity;
    int randomObject;
    public GameObject reloadText;
    public bool pHealth, pAmmo, pInmunity;
    Interpolator _interpolator = new Interpolator(1f, Interpolator.Type.SMOOTH);
    private void Start()
    {
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

        if (collision.CompareTag("Player"))
        {
            if (reloadText.activeSelf == true)
            {
                reloadText.SetActive(false);
            }

            if (randomObject == 1) //health
            {
                SoundManagerScript.PlaySound("dropSound");
                if (playerBehaviour._playerLifes < 6)
                {
                    playerBehaviour._playerLifes += 1;
                }
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
                playerBehaviour.activePowerUp = true;
                SoundManagerScript.PlaySound("powerup");
            }
            Destroy(this.gameObject);
        }
    }
}
