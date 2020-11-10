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

    // BULLET SETTINGS
    private float bulletSpeed = 25f; // Speed
    private float bulletLifeTime = 0.40f; // Distance
    private float timeBetweenShots = 0.30f; // Cadence
    private float timestamp;

    void Start()
    {
        particlePoint = this.gameObject.transform.GetChild(0).gameObject;
        shootPoint = this.gameObject.transform.GetChild(1).gameObject.transform;
        muzzle = particlePoint.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timestamp && Input.GetButton("Fire1") && playerBehaviour._bulletCounter > 0 &&
            this.gameObject.activeInHierarchy == true)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzle.Play();
        bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        timestamp = Time.time + timeBetweenShots;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootPoint.right * bulletSpeed, ForceMode2D.Impulse);
        playerBehaviour._bulletCounter--;

        Destroy(bullet, bulletLifeTime);
    }
}