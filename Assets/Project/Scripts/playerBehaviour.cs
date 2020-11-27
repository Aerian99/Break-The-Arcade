using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class playerBehaviour : MonoBehaviour
{

    private Animator animator;
    public static int _playerLifes;
    public static int _bulletCounterPurple;
    public static int _bulletCounterYellow;
    public static int _bulletCounterShotgun;

    public static bool activePostProcessing;
    private float cdAberration, maxcdAberration;
    public GameObject postProcessingAberration;
    
    public TextMeshProUGUI lifes;
    public TextMeshProUGUI bullets;

    public GameObject deathEffect;
    
    private float timer = 0.0f;
    private int seconds;

    public static int MAX_BULLETS_PURPLE;
    public static int MAX_BULLETS_YELLOW;
    public static int MAX_BULLETS_SHOTGUN;

    void Start()
    {
        animator = GetComponent<Animator>();
        _playerLifes = 5;
        _bulletCounterPurple = 0;
        _bulletCounterYellow = 0;
        _bulletCounterShotgun = 0;
        maxcdAberration = 0.1f;
        cdAberration = 0;
        activePostProcessing = false;
        MAX_BULLETS_PURPLE = 30;
        MAX_BULLETS_YELLOW = 20;
        MAX_BULLETS_SHOTGUN = 9;
    }

    void Update()
    {
        if (_playerLifes <= 0)
        {
            Destroy(this.gameObject);
            GameObject deathEffectGO = Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            Destroy(deathEffectGO, 0.5f);
        }

        chromaticAberration();
        lifes.text = "Lifes:  " + _playerLifes;

        
        if(handController.currentPos == 0) bullets.text = "Bullets:  " + _bulletCounterPurple + "/" + MAX_BULLETS_PURPLE;
        else if (handController.currentPos == 1) bullets.text = "Bullets:  " + _bulletCounterYellow + "/" + MAX_BULLETS_YELLOW;
        else if (handController.currentPos == 2) bullets.text = "Bullets:  " + _bulletCounterShotgun + "/" + MAX_BULLETS_SHOTGUN;
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