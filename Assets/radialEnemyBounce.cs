using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class radialEnemyBounce : MonoBehaviour
{
    [Header("Bullet Settings")] public int numBullets;
    public float bulletSpeed;
    public GameObject bulletPrefab, bulletPacman;
    public int burstSpeed;
    private float random, randomBtw;

    [Header("Private Variables")] private Vector3 startPoint;
    private const float radius = 1f;
    float fireRate = 1f;
    private float lastShot = 0.0f;

    GameObject bulletGO;

    

    private void Start()
    {
        random = Random.Range(0,2f);
        randomBtw = Random.Range(2, 4f);
        StartCoroutine(Shooting());
        SoundManagerScript.PlaySound("radialShoot");
    }


    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(random);
        while (true)
        {
            if (!GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().activatedAbsorb)
            {
                startPoint = transform.position;
                float angleStep = 360f / numBullets;
                float angle = 0f;

                for (int y = 0; y < burstSpeed; y++)
                {
                    if (!GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().activatedAbsorb)
                    {
                        for (int i = 0; i <= numBullets - 1; i++)
                        {
                            int random = Random.Range(0, 100);


                            float bulletDirXPosition = startPoint.x + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
                            float bulletDirYPosition = startPoint.y + Mathf.Sin((angle * Mathf.PI) / 180) * radius;

                            Vector3 bulletVector = new Vector3(bulletDirXPosition, bulletDirYPosition, 0);
                            Vector3 bulletMoveDirection = (bulletVector - startPoint).normalized * bulletSpeed;
                            if (random < 15)
                            {
                                bulletGO = Instantiate(bulletPacman, startPoint, Quaternion.identity);
                            }
                            else
                            {
                                bulletGO = Instantiate(bulletPrefab, startPoint, Quaternion.identity);
                            }
                            bulletGO.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletMoveDirection.x, bulletMoveDirection.y, 0);
                            bulletGO.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Rotamos el gameobject en función de su dirección.
                            Destroy(bulletGO, Random.Range(2.5f,3.5f));
                            angle += angleStep;
                        }
                    }
                    yield return new WaitForSeconds(0.4f);
                }

                yield return new WaitForSeconds(randomBtw);
            }
            yield return new WaitForSeconds(0);
        }
    }
}