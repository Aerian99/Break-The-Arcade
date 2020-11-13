using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedShoot : MonoBehaviour
{
    private GameObject effect;
    public ParticleSystem hitEffectPrefab;
    private ParticleSystem shootParticles;
    public GameObject bulletPrefab;
    private GameObject bullet;
    private GameObject bullet2;
    private GameObject bullet3;
    private GameObject particlePoint;
    private ParticleSystem muzzle;
    private Transform shootPoint;
    private Transform shootPoint2;
    private Transform shootPoint3;
    private float bulletForce = 25f;
    private int bulletCount = 5;
    private float bulletLifeTime = 0.20f; // Alcance de la bala
    private float timeBetweenShots = 0.35f;  
    private float timestamp;

    void Start()
    {
        particlePoint = this.gameObject.transform.GetChild(0).gameObject;
        shootPoint = this.gameObject.transform.GetChild(1).gameObject.transform;
        shootPoint2 = this.gameObject.transform.GetChild(2).gameObject.transform;
        shootPoint3 = this.gameObject.transform.GetChild(3).gameObject.transform;
        muzzle = particlePoint.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour._bulletCounter > 0 && this.gameObject.activeInHierarchy == true)
        {
            Shoot();
            SoundManagerScript.PlaySound("shotgun");
            ScreenShake.shake = 8.5f;
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

        Destroy(bullet, bulletLifeTime);
        Destroy(bullet2, bulletLifeTime);
        Destroy(bullet3, bulletLifeTime);
    }
}
