using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

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
    private GameObject particlePoint;
    private Rigidbody2D player;
    private ParticleSystem muzzle;

    private Transform shootPoint;
    private Transform shootPoint2;
    private Transform shootPoint3;

    public GameObject bulletReloadPrefab;
    private GameObject bulletReload;

    // BULLET
    public static float bulletDamage;
    private float bulletForce = 25f;
    private float bulletLifeTime = 1.5f; // Alcance de la bala
    private float timeBetweenShots = 0.35f;
    private float timestamp;
    public Sprite reloadGun1, reloadGun2, reloadGun3;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        particlePoint = this.gameObject.transform.GetChild(0).gameObject;
        shootPoint = this.gameObject.transform.GetChild(1).gameObject.transform;
        shootPoint2 = this.gameObject.transform.GetChild(2).gameObject.transform;
        shootPoint3 = this.gameObject.transform.GetChild(3).gameObject.transform;
        player = GameObject.FindWithTag("Player").gameObject.GetComponent<Rigidbody2D>();
        muzzle = particlePoint.GetComponent<ParticleSystem>();

        bulletDamage = 5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
   

        if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour.bulletsShotgun > 0 &&
            this.gameObject.activeInHierarchy == true)
        {
            Shoot();
            SoundManagerScript.PlaySound("shotgun");
            ScreenShake.shake = 4.5f;
            ScreenShake.canShake = true;
        }

        RotateReloadBullet();

        //Debug.Log(playerAimWeapon.angle);
    }

    void Shoot()
    {
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
        timestamp = Time.time + timeBetweenShots;
        playerBehaviour.bulletsShotgun--;

        shotgunJump();

        ReloadBullet();
        
        Destroy(bullet, bulletLifeTime);
        Destroy(bullet2, bulletLifeTime);
        Destroy(bullet3, bulletLifeTime);
        
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

        if (playerMovement.IsGrounded() == false && playerAimWeapon.angle > -160 && playerAimWeapon.angle < -20) // Si el jugador dispara hacia abajo con un angulo determinado, se realizara un salto con la potencia de la escopeta;
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
}