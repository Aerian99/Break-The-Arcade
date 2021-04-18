using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoEnemyShoot2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bulletGO;
    public Transform firePoint1, firePoint2;
    private float _bulletSpeed;

    private float cd, maxCd;
    private float cdRate, maxCdRate;
    
    // INITIAL FIRE POINTS POSITIONS
    // Vector3(-0.189, -0.0053, 0); FIREPOINT 1
    // Vector3(0.189, -0.0053, 0); FIREPOINT 2  0.328

    void Start()
    {
        _bulletSpeed = 12f; // Bullet speed
        maxCd = 0.03f; // Time between bullets
        cd = maxCd;
        cdRate = maxCdRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (cd <= 0)
        {
            Shooting();
            cd = maxCd;
        }
        cd -= Time.deltaTime;
    }

    private void Shooting()
    {

        // FIREPOINT 1 (LEFT)
        bulletGO = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        bulletGO.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * _bulletSpeed, ForceMode2D.Impulse);
        
        // FIREPOINT 2 (RIGHT)
        bulletGO = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        bulletGO.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * _bulletSpeed, ForceMode2D.Impulse);
    }
}