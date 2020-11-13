﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerBehaviour : MonoBehaviour
{
    private Animator animator;
    public static int _playerLifes;
    public static int _bulletCounter;
    public static bool activePostProcessing;
    private float cdAberration, maxcdAberration;
    public GameObject postProcessingAberration;
    public TextMeshProUGUI lifes;
    public TextMeshProUGUI bullets;
    private float timer = 0.0f;
    private int seconds;

    void Start()
    {
        animator = GetComponent<Animator>();
        _playerLifes = 5;
        _bulletCounter = 1000;
        maxcdAberration = 2.0f;
        cdAberration = 0;
        activePostProcessing = false;
    }

    void Update()
    {
        if (_playerLifes <= 0)
        {
            Destroy(this.gameObject);
        }

        chromaticAberration();
        lifes.text = "Lifes:  " + _playerLifes;
        bullets.text = "Bullets:  " + playerBehaviour._bulletCounter;
    }

    void chromaticAberration()
    {
        if (activePostProcessing)
        {
            postProcessingAberration.SetActive(true);
            if (cdAberration > maxcdAberration)
            {
                postProcessingAberration.SetActive(false);
                cdAberration = 0;
                activePostProcessing = false;
            }
            cdAberration += Time.deltaTime;
        }
    }
}