using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform firePoint;
    public ParticleSystem muzzle;
    public GameObject bulletPrefab;
    private GameObject bullet;

    // SOUNDS
    public AudioSource shot1;

    //private int actualWeapon;
    // BULLET SETTINGS
    private float bulletForce = 4f;

    void Start()
    {   
        shot1 = GetComponent<AudioSource>();
        //actualWeapon = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        shot1.Play(); // Shot sound
        muzzle.Play(); // Particle effect
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
