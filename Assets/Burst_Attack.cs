using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Burst_Attack : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bulletGO;
    public Transform firePoint1;
    private Transform player;
    private float _bulletSpeed, _moveSpeed;

    private float cd, maxCd;
    private float cd2, maxCd2;
    private bool startFollowing;

    void Start()
    {
        _bulletSpeed = 15f;
        maxCd = 0.2f;
        cd = maxCd;
        player = GameObject.Find("Player").transform;
        _moveSpeed = 15f;
        cd2 = 0f;
        startFollowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (cd <= 0 && cd2 < 6)
        {
            //Shooting();
            cd = maxCd;
        }

        cd -= Time.deltaTime;

        cd2 += Time.deltaTime;

        if (cd2 >= 6f)
        {
            this.GetComponent<Animator>().SetBool("attack", true);
            //cd2 = 0f;
        }
        else if (cd2 >= 10f)
        {
            this.GetComponent<Animator>().SetBool("attack", false);
            cd2 = 0f;
        }


        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, _moveSpeed * Time.deltaTime);
    }

    private void Shooting()
    {
        bulletGO = Instantiate(bulletPrefab, firePoint1.position, Quaternion.identity);
        bulletGO.GetComponent<Rigidbody2D>().velocity =
            (player.transform.position - transform.position).normalized * _bulletSpeed;
    }
}