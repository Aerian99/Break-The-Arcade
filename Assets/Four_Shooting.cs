using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Four_Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bulletGO;
    [HideInInspector] public Transform firePoint1, firePoint2, firePoint3, firePoint4;
    private float _rotationSpeed;
    private float _bulletSpeed;

    private float cd, maxCd;

    void Start()
    {
        _rotationSpeed = 90f;
        _bulletSpeed = 12f;
        maxCd = 0.2f;
        cd = maxCd;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
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
        
        // FIREPOINT 2 (UP)
        bulletGO = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        bulletGO.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * _bulletSpeed, ForceMode2D.Impulse);
        
        // FIREPOINT 3 (DOWN)
        bulletGO = Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
        bulletGO.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * _bulletSpeed, ForceMode2D.Impulse);
        
        // FIREPOINT 4 (RIGHT)
        bulletGO = Instantiate(bulletPrefab, firePoint4.position, firePoint4.rotation);
        bulletGO.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * _bulletSpeed, ForceMode2D.Impulse);
    }

    void Rotation()
    {
        Vector3 rotation1;
        // ROTATING FIREPOINTS
        this.transform.Rotate(new Vector3(0,0, _rotationSpeed) * Time.deltaTime);
        firePoint1.transform.Rotate(new Vector3(0,0, -_rotationSpeed) * Time.deltaTime);
        firePoint2.transform.Rotate(new Vector3(0,0, -_rotationSpeed) * Time.deltaTime);
        firePoint3.transform.Rotate(new Vector3(0,0, -_rotationSpeed) * Time.deltaTime);
        firePoint4.transform.Rotate(new Vector3(0,0, -_rotationSpeed) * Time.deltaTime);
    }
}