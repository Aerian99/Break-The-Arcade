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
    public static bool activeImmunity, canBeDamaged; 

    [HideInInspector]public bool activePostProcessing;
    private float cdAberration, maxcdAberration, cdImmunity, maxCdImmunity;
    private float reloadTime;
    public static bool isReloading;
    public GameObject postProcessingAberration;
    
    public TextMeshProUGUI lifes;
    public TextMeshProUGUI bullets;

    public GameObject deathEffect;
    public GameObject reloadText;
    
    //private float timer = 0.0f;
    private int seconds;

    public static int bulletsPurple, bulletsYellow, bulletsShotgun;
    public static int MAX_PURPLE_SHOOT, MAX_YELLOW_SHOOT, MAX_SHOTGUN_SHOOT; //maximo de balas que puede tener en el cargador
    public static bool purpleCanReload, yellowCanReload, shotgunCanReload;
    public static int reservedAmmoPurple, reservedAmmoYellow, reservedAmmoShotgun; //counter del total que lleva
    public static int MAX_BULLETS_PURPLE, MAX_BULLETS_YELLOW, MAX_BULLETS_SHOTGUN; //máximo que puede tener en TOTAL

    void Start()
    {
        animator = GetComponent<Animator>();
        _playerLifes = 5;
        maxcdAberration = 0.1f;
        cdAberration = 0;
        maxCdImmunity = 2f;
        canBeDamaged = true;
        cdImmunity = maxCdImmunity;
        activePostProcessing = activeImmunity = false;
        purpleCanReload = yellowCanReload = shotgunCanReload = false;
        bulletsPurple = bulletsYellow = bulletsShotgun = 9;
        reservedAmmoPurple = reservedAmmoYellow = reservedAmmoShotgun = 5;
        MAX_PURPLE_SHOOT = 10;
        MAX_YELLOW_SHOOT = 10;
        MAX_SHOTGUN_SHOOT = 3;

        MAX_BULLETS_PURPLE = 30;
        MAX_BULLETS_YELLOW = 20;
        MAX_BULLETS_SHOTGUN = 9;

        reloadTime = 2f;
        isReloading = false;
    }

    void Update()
    {
        if (_playerLifes <= 0)
        {
            Destroy(this.gameObject);
            GameObject deathEffectGO = Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            Destroy(deathEffectGO, 0.5f);
        }

        Immunity();
        chromaticAberration();
        lifes.text = "Lifes:  " + _playerLifes;

        
        if(handController.currentPos == 0) 
            bullets.text = "Bullets:  " + bulletsPurple + "/" + reservedAmmoPurple;
        else if (handController.currentPos == 1) 
            bullets.text = "Bullets:  " + bulletsYellow + "/" + reservedAmmoYellow;
        else if (handController.currentPos == 2) 
            bullets.text = "Bullets:  " + bulletsShotgun + "/" + reservedAmmoShotgun;

        if (isReloading)
            return;

        /*if (bulletsPurple <= 0 && Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (bulletsYellow <= 0 && Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
        }*/

        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }
    void Immunity()
    {
        if (activeImmunity)
        {
            _playerLifes--;
            activePostProcessing = true;
            canBeDamaged = false;
            activeImmunity = false;
            animator.SetBool("isImmune", true);
        }
        if (!canBeDamaged)
        { 
            cdImmunity -= Time.deltaTime;
            
        }

        if (cdImmunity <= 0)
        {
            animator.SetBool("isImmune", false);
            canBeDamaged = true;
            cdImmunity = maxCdImmunity;
        }
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

    IEnumerator Reload()
    {
        int bulletsNeeded;
        reloadText.SetActive(false);
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        if (handController.currentPos == 0)
        {
            bulletsNeeded = MAX_PURPLE_SHOOT - bulletsPurple;

            if (reservedAmmoPurple >= bulletsNeeded)
            {
                bulletsPurple += bulletsNeeded;
                reservedAmmoPurple -= bulletsNeeded;
            }
            else
            {
                bulletsPurple += reservedAmmoPurple;
                reservedAmmoPurple = 0;
            }
        }
        else if (handController.currentPos == 1)
        {
            bulletsNeeded = MAX_YELLOW_SHOOT - bulletsYellow;
            
            if (reservedAmmoYellow >= bulletsNeeded)
            {
                bulletsYellow += bulletsNeeded;
                reservedAmmoYellow -= bulletsNeeded;
            }
            else
            {
                bulletsYellow += reservedAmmoYellow;
                reservedAmmoYellow = 0;
            }
        }
        else if (handController.currentPos == 2)
        {
            bulletsNeeded = MAX_SHOTGUN_SHOOT - bulletsShotgun;
            
            if (reservedAmmoShotgun >= bulletsNeeded)
            {
                bulletsShotgun += bulletsNeeded;
                reservedAmmoShotgun -= bulletsNeeded;
            }
            else
            {
                bulletsShotgun += reservedAmmoShotgun;
                reservedAmmoShotgun = 0;
            }
        }
        isReloading = false;
    }
}