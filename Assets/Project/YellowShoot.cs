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
    public static int bulletCounter;

    // BULLET SETTINGS
    private float bulletForce = 25f;
    private float bulletLifeTime = 0.35f;
    void Start()
    {
        particlePoint = this.gameObject.transform.GetChild(0).gameObject;
        shootPoint = this.gameObject.transform.GetChild(1).gameObject.transform;
        muzzle = particlePoint.GetComponent<ParticleSystem>();
        bulletCounter = 99999;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && bulletCounter > 0 && this.gameObject.activeInHierarchy == true)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        muzzle.Play();
        bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootPoint.right * bulletForce, ForceMode2D.Impulse);
        bulletCounter--;

        Destroy(bullet, bulletLifeTime);
    }
}