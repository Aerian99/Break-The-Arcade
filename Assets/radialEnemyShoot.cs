using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class radialEnemyShoot : MonoBehaviour
{
    [Header("Bullet Settings")] 
    public int numBullets;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    
    [Header("Private Variables")] 
    private Vector3 startPoint;
    private const float radius = 1f;
    float fireRate = 1.5f;
    private float lastShot = 0.0f;
    void Update()
    {
        if (Time.time > fireRate + lastShot) // Simple fire rate
        {
            startPoint = transform.position;
            spawnBullets(numBullets);
            lastShot = Time.time;
        }
    }

    private void spawnBullets(int _numBullets)
    {
        float angleStep = 360f / _numBullets;
        float angle = 0f;

        for (int i = 0; i <= _numBullets - 1; i++)
        {
            float bulletDirXPosition = startPoint.x + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
            float bulletDirYPosition = startPoint.y + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            
            Vector3 bulletVector = new Vector3(bulletDirXPosition,bulletDirYPosition, 0);
            Vector3 bulletMoveDirection = (bulletVector - startPoint).normalized * bulletSpeed;

            GameObject bulletGO = Instantiate(bulletPrefab, startPoint, quaternion.identity);
            bulletGO.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletMoveDirection.x, bulletMoveDirection.y, 0);

            angle += angleStep;
        }
    }
}
