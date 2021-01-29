using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    private float FireRate = 0.5f;
    private float NextTimeToFire = 1f;
    private float shootForce = 15f;

    private GameObject player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }
    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        bulletGO.GetComponent<Rigidbody2D>().AddForce(transform.right * shootForce, ForceMode2D.Impulse);
        NextTimeToFire = Time.time + FireRate;
    }
}