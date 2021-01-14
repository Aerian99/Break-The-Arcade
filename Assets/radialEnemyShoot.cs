using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class radialEnemyShoot : MonoBehaviour
{
    [Header("Bullet Settings")] public int numBullets;
    public float bulletSpeed;
    public GameObject bulletPrefab, bulletPacman;
    public int burstSpeed;

    [Header("Private Variables")] private Vector3 startPoint;
    private const float radius = 1f;
    float fireRate = 1f;
    private float lastShot = 0.0f;

    GameObject bulletGO;

    private void Start()
    {
        StartCoroutine(Shooting());
    }


    IEnumerator Shooting()
    {
        while (true)
        {
            startPoint = transform.position;
            float angleStep = 360f / numBullets;
            float angle = 0f;

            for (int y = 0; y < burstSpeed; y++)
            {
                for (int i = 0; i <= numBullets - 1; i++)
                {
                    int random = Random.Range(0,100);

                    
                    float bulletDirXPosition = startPoint.x + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
                    float bulletDirYPosition = startPoint.y + Mathf.Sin((angle * Mathf.PI) / 180) * radius;

                    Vector3 bulletVector = new Vector3(bulletDirXPosition, bulletDirYPosition, 0);
                    Vector3 bulletMoveDirection = (bulletVector - startPoint).normalized * bulletSpeed;
                    if (random < 30)
                    {
                        bulletGO = Instantiate(bulletPacman, startPoint, Quaternion.identity);
                    }
                    else
                    {
                        bulletGO = Instantiate(bulletPrefab, startPoint, Quaternion.identity);
                    }
                    bulletGO.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletMoveDirection.x, bulletMoveDirection.y, 0);
                    bulletGO.transform.rotation = UnityEngine.Quaternion.AngleAxis(angle, Vector3.forward); // Rotamos el gameobject en función de su dirección.
                    Destroy(bulletGO, 2f);
                    angle += angleStep;
                }

                yield return new WaitForSeconds(0.4f);
            }

            yield return new WaitForSeconds(5);
        }
    }
}