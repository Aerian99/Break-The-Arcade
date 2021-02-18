using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RedShoot : MonoBehaviour
{
    private GameObject effect;
    private Animator anim;
    public ParticleSystem hitEffectPrefab;
    private ParticleSystem shootParticles;

    public GameObject bulletPrefab;
    private GameObject bullet;
    private GameObject bullet2;
    private GameObject bullet3;
    private GameObject bullet4;
    private GameObject bullet5;
    private GameObject bullet6;

    private GameObject particlePoint;
    private Rigidbody2D player;
    private ParticleSystem muzzle;

    private Transform shootPoint;
    private Transform shootPoint2;
    private Transform shootPoint3;
    private Transform shootPoint4;
    private Transform shootPoint5;
    private Transform shootPoint6;

    public GameObject bulletReloadPrefab;
    private GameObject bulletReload;

    public bool powerUpGreen;
    public bool powerUpBlue;

    public GameObject reloadText;
    public GameObject noAmmoText;

    // BULLET
    public static float bulletDamage;
    private float bulletForce = 40f;
    private float bulletLifeTime = 1.5f; // Alcance de la bala
    private float timeBetweenShots = 0.35f;
    private float timestamp;
    public Sprite reloadGun1, reloadGun2, reloadGun3;

    public GameObject cursor;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        particlePoint = this.gameObject.transform.GetChild(0).gameObject;
        shootPoint = this.gameObject.transform.GetChild(1).gameObject.transform;
        shootPoint2 = this.gameObject.transform.GetChild(2).gameObject.transform;
        shootPoint3 = this.gameObject.transform.GetChild(3).gameObject.transform;
        shootPoint4 = this.gameObject.transform.GetChild(4).gameObject.transform;
        shootPoint5 = this.gameObject.transform.GetChild(5).gameObject.transform;
        player = GameObject.FindWithTag("Player").gameObject.GetComponent<Rigidbody2D>();
        muzzle = particlePoint.GetComponent<ParticleSystem>();

        bulletDamage = 5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "PowerUpScene")
            powerUps();
        else
        {
            if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsShotgun > 0 &&
                this.gameObject.activeInHierarchy == true && !playerBehaviour.isReloading && !playerBehaviour.weaponMenuUp)
            {
                Shoot();
                SoundManagerScript.PlaySound("shotgun");
                ScreenShake.shake = 4.5f;
                ScreenShake.canShake = true;
                cursor.GetComponent<Animator>().SetTrigger("click");
            }

            else if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsShotgun == 0 &&
                     this.gameObject.activeInHierarchy == true && playerBehaviour.reservedAmmoShotgun > 0 && !playerBehaviour.weaponMenuUp)
            {
                reloadText.SetActive(true);
            }

            else if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsShotgun == 0 &&
                     this.gameObject.activeInHierarchy == true && playerBehaviour.reservedAmmoShotgun == 0 && !playerBehaviour.weaponMenuUp)
            {
                noAmmoText.SetActive(true);
            }

            RotateReloadBullet();
        }
    }

    void Shoot()
    {
        if (Absorb_Gun.firstTimeAbsorb2)
        {
            Absorb_Gun.firstTimeAbsorb2 = false;
            Absorb_Gun.ammoFull2 = true;
        }

        muzzle.Play();
        if (powerUpGreen)
        {
            bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootPoint.right * bulletForce, ForceMode2D.Impulse);

            bullet2 = Instantiate(bulletPrefab, shootPoint2.position, shootPoint2.rotation);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce(shootPoint2.right * bulletForce, ForceMode2D.Impulse);

            bullet3 = Instantiate(bulletPrefab, shootPoint3.position, shootPoint3.rotation);
            Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
            rb3.AddForce(shootPoint3.right * bulletForce, ForceMode2D.Impulse);

            bullet4 = Instantiate(bulletPrefab, shootPoint4.position, shootPoint.rotation);
            Rigidbody2D rb4 = bullet4.GetComponent<Rigidbody2D>();
            rb4.AddForce(shootPoint4.right * bulletForce, ForceMode2D.Impulse);

            bullet5 = Instantiate(bulletPrefab, shootPoint5.position, shootPoint.rotation);
            Rigidbody2D rb5 = bullet5.GetComponent<Rigidbody2D>();
            rb5.AddForce(shootPoint5.right * bulletForce, ForceMode2D.Impulse);
            timestamp = Time.time + timeBetweenShots;
            playerBehaviour.bulletsShotgun--;
            shotgunJump();

            ReloadBullet();

            Destroy(bullet, bulletLifeTime);
            Destroy(bullet2, bulletLifeTime);
            Destroy(bullet3, bulletLifeTime);
            Destroy(bullet4, bulletLifeTime);
            Destroy(bullet5, bulletLifeTime);
        }
        else if (powerUpBlue)
        {
            StartCoroutine(ShootBlue());
        }
        else
        {
            bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootPoint.right * bulletForce, ForceMode2D.Impulse);

            bullet2 = Instantiate(bulletPrefab, shootPoint2.position, shootPoint2.rotation);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce(shootPoint2.right * bulletForce, ForceMode2D.Impulse);

            bullet3 = Instantiate(bulletPrefab, shootPoint3.position, shootPoint3.rotation);
            Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
            rb3.AddForce(shootPoint3.right * bulletForce, ForceMode2D.Impulse);
            timestamp = Time.time + timeBetweenShots;
            playerBehaviour.bulletsShotgun--;
            shotgunJump();

            ReloadBullet();
            Destroy(bullet, bulletLifeTime);
            Destroy(bullet2, bulletLifeTime);
            Destroy(bullet3, bulletLifeTime);
        }
    }

    IEnumerator ShootBlue()
    {
        playerBehaviour.bulletsShotgun--;

        for (int i = 0; i < 2; i++)
        {
            bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootPoint.right * bulletForce, ForceMode2D.Impulse);

            bullet2 = Instantiate(bulletPrefab, shootPoint2.position, shootPoint2.rotation);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce(shootPoint2.right * bulletForce, ForceMode2D.Impulse);

            bullet3 = Instantiate(bulletPrefab, shootPoint3.position, shootPoint3.rotation);
            Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
            rb3.AddForce(shootPoint3.right * bulletForce, ForceMode2D.Impulse);

            bullet4 = Instantiate(bulletPrefab, shootPoint4.position, shootPoint.rotation);
            Rigidbody2D rb4 = bullet4.GetComponent<Rigidbody2D>();
            rb4.AddForce(shootPoint4.right * bulletForce, ForceMode2D.Impulse);

            bullet5 = Instantiate(bulletPrefab, shootPoint5.position, shootPoint.rotation);
            Rigidbody2D rb5 = bullet5.GetComponent<Rigidbody2D>();
            rb5.AddForce(shootPoint5.right * bulletForce, ForceMode2D.Impulse);
            timestamp = Time.time + timeBetweenShots;

            ReloadBullet();
            shotgunJump();
            Destroy(bullet, bulletLifeTime);
            Destroy(bullet2, bulletLifeTime);
            Destroy(bullet3, bulletLifeTime);
            Destroy(bullet4, bulletLifeTime);
            Destroy(bullet5, bulletLifeTime);
            yield return new WaitForSeconds(0.3f);
        }


        yield return new WaitForSeconds(0);
    }

    void shotgunJump()
    {
        if (playerMovement.IsGrounded() && playerAimWeapon.angle > -160 && playerAimWeapon.angle < -90)
        {
            player.AddForce(new Vector2(25f, 7f), ForceMode2D.Impulse);
        }
        else if (playerMovement.IsGrounded() && playerAimWeapon.angle > -90 && playerAimWeapon.angle < -20)
        {
            player.AddForce(new Vector2(-25f, 7f), ForceMode2D.Impulse);
        }

        if (playerMovement.IsGrounded() == false && playerAimWeapon.angle > -160 && playerAimWeapon.angle < -20
        ) // Si el jugador dispara hacia abajo con un angulo determinado, se realizara un salto con la potencia de la escopeta;
        {
            player.velocity = Vector2.zero;
            player.AddForce(new Vector2(0f, 35f), ForceMode2D.Impulse);
        }
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

        Destroy(bulletReload, 0.6f);
    }

    void RotateReloadBullet()
    {
        if (bulletReload != null)
        {
            if (bulletReload.GetComponent<Rigidbody2D>().velocity.y > 0f)
            {
                bulletReload.GetComponent<SpriteRenderer>().sprite = reloadGun1;
            }
            else if (bulletReload.GetComponent<Rigidbody2D>().velocity.y < 0f)
            {
                bulletReload.GetComponent<SpriteRenderer>().sprite = reloadGun2;
            }
            else if (bulletReload.GetComponent<Rigidbody2D>().velocity.y == 0f)
            {
                bulletReload.GetComponent<SpriteRenderer>().sprite = reloadGun3;
            }
        }
    }


    void powerUps()
    {
        if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsShotgun > 0 &&
            this.gameObject.activeInHierarchy == true)
        {
            ShootPowerUp();
            SoundManagerScript.PlaySound("shotgun");
            ScreenShake.shake = 4.5f;
            ScreenShake.canShake = true;
        }

        else if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsShotgun == 0 &&
                 this.gameObject.activeInHierarchy == true && playerBehaviour.reservedAmmoShotgun > 0 &&
                 !playerBehaviour.isReloading)
        {
            reloadText.SetActive(true);
        }

        else if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsShotgun == 0 &&
                 this.gameObject.activeInHierarchy == true && playerBehaviour.reservedAmmoShotgun == 0)
        {
            noAmmoText.SetActive(true);
        }

        RotateReloadBullet();
    }

    void ShootPowerUp()
    {
        if (Absorb_Gun.firstTimeAbsorb2)
        {
            Absorb_Gun.firstTimeAbsorb2 = false;
            Absorb_Gun.ammoFull2 = true;
        }

        muzzle.Play();

        bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootPoint.right * bulletForce, ForceMode2D.Impulse);

        bullet2 = Instantiate(bulletPrefab, shootPoint2.position, shootPoint2.rotation);
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(shootPoint2.right * bulletForce, ForceMode2D.Impulse);

        bullet3 = Instantiate(bulletPrefab, shootPoint3.position, shootPoint3.rotation);
        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
        rb3.AddForce(shootPoint3.right * bulletForce, ForceMode2D.Impulse);

        bullet4 = Instantiate(bulletPrefab, shootPoint4.position, shootPoint4.rotation);
        Rigidbody2D rb4 = bullet4.GetComponent<Rigidbody2D>();
        rb4.AddForce(shootPoint4.right * bulletForce, ForceMode2D.Impulse);

        bullet5 = Instantiate(bulletPrefab, shootPoint5.position, shootPoint5.rotation);
        Rigidbody2D rb5 = bullet5.GetComponent<Rigidbody2D>();
        rb5.AddForce(shootPoint5.right * bulletForce, ForceMode2D.Impulse);

        bullet6 = Instantiate(bulletPrefab, shootPoint6.position, shootPoint6.rotation);
        Rigidbody2D rb6 = bullet6.GetComponent<Rigidbody2D>();
        rb6.AddForce(shootPoint6.right * bulletForce, ForceMode2D.Impulse);

        timestamp = Time.time + timeBetweenShots;
        playerBehaviour.bulletsShotgun--;

        shotgunJump();

        ReloadBullet();

        Destroy(bullet, bulletLifeTime);
        Destroy(bullet2, bulletLifeTime);
        Destroy(bullet3, bulletLifeTime);
        Destroy(bullet4, bulletLifeTime);
        Destroy(bullet5, bulletLifeTime);
        Destroy(bullet6, bulletLifeTime);
    }
}