using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    private float maxCdAmmo, cdAmmo;

     public bool greenPowerUp, bluePowerUp;

    //BULLETS
    public static float bulletDamage;
    private float bulletSpeed = 50f; // Speed
    private float bulletLifeTime = 10f; // Distance
    private float timeBetweenShots = 0.20f; // Cadence
    private float timestamp;

    public GameObject cursor;


    void Start()
    {
        greenPowerUp = bluePowerUp = false;
        particlePoint = this.gameObject.transform.GetChild(0).gameObject;
        shootPoint = this.gameObject.transform.GetChild(1).gameObject.transform;
        muzzle = particlePoint.GetComponent<ParticleSystem>();
        bulletDamage = 2f;
        maxCdAmmo = 1.1f;
        cdAmmo = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PowerUpScene" && Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsPurple > 0 &&
            this.gameObject.activeInHierarchy == true)
        {
            StartCoroutine(ShootPower()); //If the scene is PowerUps starts a coroutine to shoot two times in a period of time 0.05 seconds and waste only one bullet
            playerBehaviour.bulletsPurple--;
        }
        else if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsPurple > 0 &&
            this.gameObject.activeInHierarchy == true && !playerBehaviour.isReloading && !playerBehaviour.weaponMenuUp)
        {
            Shoot();
            SoundManagerScript.PlaySound("purpleGun");
            ScreenShake.shake = 0.5f;
            ScreenShake.canShake = true;
            cursor.GetComponent<Animator>().SetTrigger("click");
        }
        else if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsPurple == 0 &&
           this.gameObject.activeInHierarchy == true && playerBehaviour.reservedAmmoPurple > 0 && !playerBehaviour.isReloading && !playerBehaviour.weaponMenuUp)
        {
            reloadText.SetActive(true);
        }
        else if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsPurple == 0 && playerBehaviour.reservedAmmoPurple == 0 &&
           this.gameObject.activeInHierarchy == true && !playerBehaviour.weaponMenuUp)
        {
            noAmmoText.SetActive(true);
        }

        if(noAmmoText.activeInHierarchy)
        {
            cdAmmo += Time.deltaTime;
            if (cdAmmo >= maxCdAmmo)
            {
                noAmmoText.SetActive(false);
                cdAmmo = 0;
            }

        }

    }
    void Shoot()
    {
        if (Absorb_Gun.firstTimeAbsorb0)
        {
            Absorb_Gun.firstTimeAbsorb0 = false;
            Absorb_Gun.ammoFull0 = true;
        }
        if (greenPowerUp)
        {
            timeBetweenShots = 0.10f;
            muzzle.Play();
            bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            timestamp = Time.time + timeBetweenShots;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootPoint.right * bulletSpeed, ForceMode2D.Impulse);
            playerBehaviour.bulletsPurple--;
            ReloadBullet();

            Destroy(bullet, bulletLifeTime);
        }
        else if (bluePowerUp)
        {
            timeBetweenShots = 0.10f;
            StartCoroutine(shootBluePowerUp());
        }
        else
        {
            muzzle.Play();
            bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            timestamp = Time.time + timeBetweenShots;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootPoint.right * bulletSpeed, ForceMode2D.Impulse);
            playerBehaviour.bulletsPurple--;
            ReloadBullet();

            Destroy(bullet, bulletLifeTime);
        }
    }

    IEnumerator shootBluePowerUp()
    {
        playerBehaviour.bulletsPurple--;

        for (int i = 0; i < 2; i++)
        {
            muzzle.Play();
            bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            timestamp = Time.time + timeBetweenShots;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootPoint.right * bulletSpeed, ForceMode2D.Impulse);
            ReloadBullet();
            yield return new WaitForSeconds(0.02f);
        }


        Destroy(bullet, bulletLifeTime);

        yield return new WaitForSeconds(0);
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

    IEnumerator ShootPower()
    {
        for (int i = 0; i < 4; i++)
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


            ReloadBullet();

            Destroy(bullet, bulletLifeTime);
            SoundManagerScript.PlaySound("purpleGun");
            ScreenShake.shake = 0.2f;
            ScreenShake.canShake = true;
            yield return new WaitForSeconds(0.05f);
        }
    }
}