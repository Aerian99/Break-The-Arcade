using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PurpleShoot : MonoBehaviour
{
    private GameObject particlePoint;
    private ParticleSystem muzzle;
    private Transform shootPoint;
    public GameObject bulletPrefab;
    private GameObject bullet;
    public GameObject bulletReloadPrefab;
    private GameObject bulletReload;

    public GameObject reloadText;
    public GameObject noAmmoText;

    //BULLETS
    public static float bulletDamage;
    private float bulletSpeed = 25f; // Speed
    private float bulletLifeTime = 0.40f; // Distance
    private float timeBetweenShots = 0.30f; // Cadence
    private float timestamp;


    void Start()
    {
        particlePoint = this.gameObject.transform.GetChild(0).gameObject;
        shootPoint = this.gameObject.transform.GetChild(1).gameObject.transform;
        muzzle = particlePoint.GetComponent<ParticleSystem>();
        bulletDamage = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsPurple > 0 &&
            this.gameObject.activeInHierarchy == true)
        {
            Shoot();
            SoundManagerScript.PlaySound("purpleGun");
            ScreenShake.shake = 1.5f;
            ScreenShake.canShake = true;
        }

        else if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsPurple == 0 &&
           this.gameObject.activeInHierarchy == true && playerBehaviour.reservedAmmoPurple > 0)
        {
            reloadText.SetActive(true);
        }

        else if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsPurple == 0 && playerBehaviour.reservedAmmoPurple == 0 &&
           this.gameObject.activeInHierarchy == true)
        {
            noAmmoText.SetActive(true);
        }

    }
    void Shoot()
    {
        if (Absorb_Gun.firstTimeAbsorb0)
        {
            Absorb_Gun.firstTimeAbsorb0 = false;
            Absorb_Gun.ammoFull0 = true;
        }
        muzzle.Play();
        bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        timestamp = Time.time + timeBetweenShots;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootPoint.right * bulletSpeed, ForceMode2D.Impulse);
        playerBehaviour.bulletsPurple--;
        
        ReloadBullet();

        Destroy(bullet, bulletLifeTime);
    }
    
    void ReloadBullet()
    {
        bulletReload = Instantiate(bulletReloadPrefab, this.transform.position, Quaternion.identity);
        if (playerAimWeapon.isFacingLeft)
        {
            bulletReload.GetComponent<Rigidbody2D>().AddForce(new Vector2(6f, 7f), ForceMode2D.Impulse);
        }
        else
        {
            bulletReload.GetComponent<Rigidbody2D>().AddForce(new Vector2(-6f, 7f), ForceMode2D.Impulse);
        }
        Destroy(bulletReload, 1f);
    }
}