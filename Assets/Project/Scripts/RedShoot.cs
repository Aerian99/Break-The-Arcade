using System;
using System.Collections;
using System.Collections.Generic;
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
    private GameObject reloadBullet;
    public GameObject reloadPrefab;
    private GameObject particlePoint;
    private Rigidbody2D player;
    private ParticleSystem muzzle;

    private Transform shootPoint;
    private Transform shootPoint2;
    private Transform shootPoint3;

    // BULLET
    public static float bulletDamage;
    private float bulletForce = 25f;
    private float bulletLifeTime = 0.40f; // Alcance de la bala
    private float timeBetweenShots = 0.35f;
    private float timestamp;
    private Vector2 vector2r, vector2l;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        particlePoint = this.gameObject.transform.GetChild(0).gameObject;
        shootPoint = this.gameObject.transform.GetChild(1).gameObject.transform;
        shootPoint2 = this.gameObject.transform.GetChild(2).gameObject.transform;
        shootPoint3 = this.gameObject.transform.GetChild(3).gameObject.transform;
        player = GameObject.FindWithTag("Player").gameObject.GetComponent<Rigidbody2D>();
        muzzle = particlePoint.GetComponent<ParticleSystem>();
        vector2r = new Vector2(150f, 10f);
        vector2l = new Vector2(-150f, 10f);

        bulletDamage = 5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour._bulletCounter > 0 &&
            this.gameObject.activeInHierarchy == true)
        {
            Shoot();
            SoundManagerScript.PlaySound("shotgun");
            ScreenShake.shake = 4.5f;
            ScreenShake.canShake = true;
        }
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
        playerBehaviour._bulletCounter--;

        

        knockBack();

        Destroy(bullet, bulletLifeTime);
        Destroy(bullet2, bulletLifeTime);
        Destroy(bullet3, bulletLifeTime);
        Destroy(reloadBullet, 0.3f);
    }

    void knockBack()
    {
        if (playerAimWeapon.isFacingLeft && playerMovement.IsGrounded()) // CUANDO EL JUGADOR MIRA HACIA LA IZQUIERDA
        {
            player.AddForce(vector2r, ForceMode2D.Impulse);
        }
        else if (!playerAimWeapon.isFacingLeft && playerMovement.IsGrounded()
        ) // CUANDO EL JUGADOR MIRA HACIA LA DERECHA
        {
            player.AddForce(vector2l, ForceMode2D.Impulse);
        }

        if (playerAimWeapon.angle > -170 && playerAimWeapon.angle < -90 && playerMovement.IsGrounded())
        {
            player.AddForce(new Vector2(vector2r.x, vector2r.y), ForceMode2D.Impulse);
        }
        else if (playerAimWeapon.angle > -90 && playerAimWeapon.angle < -10 && playerMovement.IsGrounded())
        {
            player.AddForce(new Vector2(vector2l.x, vector2l.y), ForceMode2D.Impulse);
        }

        if (playerMovement.IsGrounded() == false && playerAimWeapon.angle > -130 && playerAimWeapon.angle < -30)
        {
            player.velocity = Vector2.zero;
            player.AddForce(new Vector2(0f, 35f), ForceMode2D.Impulse);
        }
    }

 
}