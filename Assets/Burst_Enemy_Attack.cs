﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Burst_Enemy_Attack : MonoBehaviour
{
    // Targets and gameobjects
    private Transform player;
    private GameObject bulletGO;
    public GameObject bulletPrefab;

    // Stats
    private float _moveSpeed;
    private float _bulletSpeed;
    
    // Dash timer
    private float _dashMaxTime;
    private float _dashRateTime;
    private float _shootingCD;
    private float _shootingMaxCD;
    private bool _isDashing;

    private Vector2 velocity;
    
    void Start()
    {
        player = GameObject.Find("Player").transform;
        
        _moveSpeed = 75f;
        _bulletSpeed = 15f;
        
        _dashMaxTime = 1f;
        _dashRateTime = 3f;

        _shootingMaxCD = 0.5f;
        _shootingCD = _shootingMaxCD;
    }

    // Update is called once per frame
    void Update()
    {
        // Following the player for 10 seconds every 3 seconds.
        if (Vector2.Distance(this.gameObject.transform.position, player.transform.position) < 15f)
        {
            _dashMaxTime -= Time.deltaTime;
            if (_dashMaxTime > 0)
            {
                Dashing();
                _dashRateTime = 3f;
            }
            else if (_dashMaxTime <= 0f)
            {
                _dashRateTime -= Time.deltaTime;

                if (_shootingCD <= 0)
                {
                    Shooting();
                    _shootingCD = _shootingMaxCD;
                }

                _shootingCD -= Time.deltaTime;
            }

            if (_dashRateTime <= 0f)
            {
                _dashMaxTime = 1f;
            }
        }
    }
    

    void Dashing()
    {
        // THIS NEEDS A REWORK FOR COLLISION KNOCKBACK
        Vector2 newPosition = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * _moveSpeed);
        this.GetComponent<Rigidbody2D>().MovePosition(newPosition);
    }

    void Shooting()
    {
        bulletGO = Instantiate(bulletPrefab, this.transform.GetChild(0).position, Quaternion.identity);
        bulletGO.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * _bulletSpeed;
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        float magnitude = 500f;
        
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
