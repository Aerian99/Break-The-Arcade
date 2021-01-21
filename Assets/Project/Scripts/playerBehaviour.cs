using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerBehaviour : MonoBehaviour
{

    private Animator animator;
    public static int _playerLifes;
    private float _maxLifes;
    public static bool activeImmunity, canBeDamaged;

    private reloadScript reloadScript;

    [HideInInspector]public bool activePostProcessing;
    private float cdAberration, maxcdAberration, cdImmunity, maxCdImmunity;
    public float reloadTime;
    public static bool isReloading;

    public TextMeshProUGUI purpleBulletsCounter;
    public TextMeshProUGUI yellowBulletsCounter;
    public TextMeshProUGUI redBulletsCounter;

    public Image healthBarFill;
    public Image healthBarWhiteFill;
    private float hurtSpeed;
    
    // HEALTHBAR SETTINGS
    public GameObject defaultBar;
    public Sprite[] healthbarimages;

    public GameObject deathEffect;
    private GameObject reloadText;
    
    private int seconds;

    public static int bulletsPurple, bulletsYellow, bulletsShotgun;
    public static int MAX_PURPLE_SHOOT, MAX_YELLOW_SHOOT, MAX_SHOTGUN_SHOOT; //maximo de balas que puede tener en el cargador
    public static bool purpleCanReload, yellowCanReload, shotgunCanReload;
    public static int reservedAmmoPurple, reservedAmmoYellow, reservedAmmoShotgun; //counter del total que lleva
    public static int MAX_BULLETS_PURPLE, MAX_BULLETS_YELLOW, MAX_BULLETS_SHOTGUN; //máximo que puede tener en TOTAL

    void Start()
    {
        reloadText = GameObject.Find("ReloadText");
        animator = GetComponent<Animator>();
        _maxLifes = 100f;
        //_playerLifes = _maxLifes;
        _playerLifes = 3;
        hurtSpeed = 0.0005f;
        maxcdAberration = 0.1f;
        cdAberration = 0;
        maxCdImmunity = 2f;
        canBeDamaged = true;
        cdImmunity = maxCdImmunity;
        activePostProcessing = activeImmunity = false;
        purpleCanReload = yellowCanReload = shotgunCanReload = false;
        bulletsPurple = bulletsYellow = bulletsShotgun = 0;
        reservedAmmoPurple = reservedAmmoYellow = reservedAmmoShotgun = 200;
        MAX_PURPLE_SHOOT = 15;
        MAX_YELLOW_SHOOT = 10;
        MAX_SHOTGUN_SHOOT = 3;

        MAX_BULLETS_PURPLE = 15;
        MAX_BULLETS_YELLOW = 20;
        MAX_BULLETS_SHOTGUN = 9;

        reloadTime = 1f;
        isReloading = false;
        reloadText.SetActive(false);
        reloadScript = GetComponent<reloadScript>();
    }

    void Update()
    {
        resetReload();
        //healthBarEffect();
        healthBarPixel();
        
        if (_playerLifes <= 0)
        {
            Destroy(this.gameObject);
            GameObject deathEffectGO = Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            Destroy(deathEffectGO, 0.5f);
        }
        
        Immunity();
        chromaticAberration();

        if(handController.currentPos == 0) 
            purpleBulletsCounter.text = "" + reservedAmmoPurple;
        else if (handController.currentPos == 1) 
            yellowBulletsCounter.text = "" + reservedAmmoYellow;
        else if (handController.currentPos == 2) 
            redBulletsCounter.text = "" + reservedAmmoShotgun;

        if (isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.R) && 
            (handController.currentPos == 0 && bulletsPurple < MAX_PURPLE_SHOOT ||
            handController.currentPos == 1 && bulletsYellow < MAX_YELLOW_SHOOT ||
            handController.currentPos == 2 && bulletsShotgun < MAX_SHOTGUN_SHOOT))
        {
            StartCoroutine(Reload());
        }
    }
    void Immunity()
    {
        if (activeImmunity)
        {
            SoundManagerScript.PlaySound("hurt");
            _playerLifes -= 1;
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
            if (cdAberration > maxcdAberration)
            {
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

    void resetReload() // Esta función previene que no acabe la recarga si cambias de arma si estás recargando.
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && isReloading) // Comprobamos si cambia de arma cuando esta recargando, esto reseteará la recarga.
        {
            reloadScript.fill.fillAmount = 0f;
            reloadScript.fill.enabled = false;
            reloadScript.perTimer.enabled = false;
            reloadScript.timer = 0f;
            StopAllCoroutines();
            isReloading = false;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && isReloading) // Comprobamos si cambia de arma cuando esta recargando, esto reseteará la recarga.
        {
            reloadScript.fill.fillAmount = 0f;
            reloadScript.fill.enabled = false;
            reloadScript.perTimer.enabled = false;
            reloadScript.timer = 0f;
            StopAllCoroutines();
            isReloading = false;
        }
    }

    void healthBarEffect()
    {
        healthBarFill.fillAmount = _playerLifes / _maxLifes;

        if (healthBarWhiteFill.fillAmount > healthBarFill.fillAmount)
        {
            healthBarWhiteFill.fillAmount -= hurtSpeed;
        }
        else
        {
            healthBarWhiteFill.fillAmount = healthBarFill.fillAmount;
        }
    }

    void healthBarPixel()
    {
        for (int i = 0; i <= 3; i++)
        {
            if (_playerLifes == i)
            {
                defaultBar.GetComponent<Image>().sprite = healthbarimages[i];
            }
        }
    }
}