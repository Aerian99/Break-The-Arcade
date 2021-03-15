using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class GrenadeShoot : MonoBehaviour
{
    public int numBullets;
    private Vector3 startPoint;
    private const float radius = 1f;
    public float bulletSpeed;
    public GameObject bulletPrefab, lightEffect;
    private bool absorbed;
    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(this.gameObject, 1.5f);
        absorbed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.tag != "AbsorbGun" && other.gameObject.tag != "Range" && other.gameObject.tag != "absorbZone" && !absorbed)
        {
            if (player.GetComponent<playerBehaviour>().canBeDamaged && player.GetComponent<playerBehaviour>().canBeDamagedPowerup)
            {
                player.GetComponent<playerBehaviour>().activeImmunity = true;
                other.GetComponent<Animator>().SetTrigger("hit");
            }
            Destroy(this.gameObject);
        }
        else if
        (other.gameObject.tag != "Enemy"
         && other.gameObject.tag != "PurpleBullet"
         && other.gameObject.tag != "YellowBullet"
         && other.gameObject.tag != "RedBullet"
         && other.gameObject.tag != "EnemyBullet"
         && other.gameObject.tag != "Bullet Pacman"
         && other.gameObject.tag != "Range"
         && other.gameObject.tag != "Tower"
         && other.gameObject.tag != "CyanEnemy"
         && other.gameObject.tag != "OrangeEnemy"
         && other.gameObject.tag != "RedEnemy"
         && other.gameObject.tag != "NPC"
         && other.gameObject.tag != "greyPlatform"
         && other.gameObject.tag != "Triggers"
         && other.gameObject.tag != "RobotPatrol"
         && other.gameObject.tag != "Bullet")
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "AlienWall")
        {
            other.GetComponent<ProtectionBarrierAliens>().hitted = true;
        }
        if (other.gameObject.CompareTag("absorbZone")) // Si la bala a entrado en la zona de absorción no puede hacer daño y ponemos "absorbed" a true.
        {
            absorbed = true;
            Destroy(this.gameObject, 0.1f); // Destruimos la bala en función del tiempo que tarda en absorber, para ahorrar problemas.
        }

        if (other.gameObject.CompareTag("AbsorbGun"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(lightEffect, this.transform.position, quaternion.identity);
        startPoint = transform.position;
        startPoint = new Vector3(startPoint.x, startPoint.y + .5f, startPoint.z);
        float angleStep = 360f / numBullets;
        float angle = 0f;


        for (int i = 0; i <= numBullets - 1; i++)
        {
            float bulletDirXPosition = startPoint.x + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
            float bulletDirYPosition = startPoint.y + Mathf.Sin((angle * Mathf.PI) / 180) * radius;

            Vector3 bulletVector = new Vector3(bulletDirXPosition, bulletDirYPosition, 0);
            Vector3 bulletMoveDirection = (bulletVector - startPoint).normalized * bulletSpeed;

            GameObject bulletGO = Instantiate(bulletPrefab, startPoint, quaternion.identity);
            bulletGO.transform.rotation = UnityEngine.Quaternion.AngleAxis(angle, Vector3.forward);
            bulletGO.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletMoveDirection.x, bulletMoveDirection.y, 0);
            angle += angleStep;
        }
    }
}

