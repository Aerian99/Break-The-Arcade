using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmo : MonoBehaviour
{
    private GameObject player;
    Interpolator _interpolator = new Interpolator(1f, Interpolator.Type.SMOOTH);
    public float interpolationValue;
    Vector3 _position;

    private void Start()
    {
        _interpolator.ToMax();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        _interpolator.Update(Time.deltaTime  / 2 );

        if (_interpolator.IsMaxPrecise)
            _interpolator.ToMin();
        else if (_interpolator.IsMinPrecise)
            _interpolator.ToMax();
        _position.y = 1 * 1 * _interpolator.Value + interpolationValue;
        _position.x = this.transform.position.x;
        this.transform.position = _position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int tempAmmo = 0;
        int tempAmmo2 = 0;

        if (collision.CompareTag("Player"))
        {
            SoundManagerScript.PlaySound("dropSound");
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
            Destroy(this.gameObject);
        }
    }
}
