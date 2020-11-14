using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowShoot : MonoBehaviour
{
    private GameObject particlePoint;
    private ParticleSystem muzzle;
    private Transform shootPoint;
    public GameObject bulletPrefab;
    private GameObject bullet;

    // BULLET SETTINGS
    public static float bulletDamage;
    private float bulletForce = 25f;
    private float bulletLifeTime = 0.35f;
    private float timeBetweenShots = 0.20f;
    private float timestamp;
    void Start()
    {
        particlePoint = this.gameObject.transform.GetChild(0).gameObject;
        shootPoint = this.gameObject.transform.GetChild(1).gameObject.transform;
        muzzle = particlePoint.GetComponent<ParticleSystem>();
        bulletDamage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour._bulletCounter > 0 && this.gameObject.activeInHierarchy == true)
        {
            Shoot();
            SoundManagerScript.PlaySound("yellowGun");
            ScreenShake.shake = 1f;
            ScreenShake.canShake = true;
        }
    }
    void Shoot()
    {
        muzzle.Play();
        bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        timestamp = Time.time + timeBetweenShots;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootPoint.right * bulletForce, ForceMode2D.Impulse);
        playerBehaviour._bulletCounter--;

        Destroy(bullet, bulletLifeTime);
    }
}