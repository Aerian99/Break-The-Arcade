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
    public GameObject activeCamera;
    private Animator animator;
    [HideInInspector] public int _playerLifes;
    private float _maxLifes;
    [HideInInspector]  public bool activeImmunity, canBeDamaged, canBeDamagedPowerup, activePowerUp;

    private reloadScript reloadScript;

    [HideInInspector] public bool activePostProcessing;
    [HideInInspector] public float cdAberration, maxcdAberration, cdImmunity, maxCdImmunity, cdPowerup, maxCdPowerup;
    public float reloadTime;
    [HideInInspector] public bool isReloading;

    public TextMeshProUGUI purpleBulletsCounter;
    public TextMeshProUGUI yellowBulletsCounter;
    public TextMeshProUGUI redBulletsCounter;

    public Image healthBarFill;
    public Image healthBarWhiteFill;
    private float hurtSpeed;

    // HEALTHBAR SETTINGS
    public GameObject defaultBar;
    public Sprite[] healthbarimages;

    public GameObject deathEffect, noAmmoText;
    private GameObject reloadText;

    private int seconds;

    [HideInInspector] public int bulletsPurple, bulletsYellow, bulletsShotgun;

    [HideInInspector]
    public int MAX_PURPLE_SHOOT, MAX_YELLOW_SHOOT, MAX_SHOTGUN_SHOOT; //maximo de balas que puede tener en el cargador

    [HideInInspector] public bool purpleCanReload, yellowCanReload, shotgunCanReload;
    [HideInInspector] public int reservedAmmoPurple, reservedAmmoYellow, reservedAmmoShotgun; //counter del total que lleva
    [HideInInspector] public int MAX_BULLETS_PURPLE, MAX_BULLETS_YELLOW, MAX_BULLETS_SHOTGUN; //máximo que puede tener en TOTAL

    public GameObject weaponMenu;
    [HideInInspector] public bool weaponMenuUp;
    public GameObject aimController1, aimController2, aimController3, aimController4;

    //SHIELD

    public GameObject shield;
    public float cdShield, maxCdShield;
    public bool shieldActivated;

    void Start()
    {
        reloadText = GameObject.Find("ReloadText");
        animator = GetComponent<Animator>();
        _maxLifes = 100f;
        _playerLifes = 5;
        //_playerLifes = 3 + GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.playerUpLifes;
        hurtSpeed = 0.0005f;
        maxcdAberration = 0.1f;
        cdAberration = 0;
        maxCdImmunity = 2f;
        maxCdPowerup = 4f;
        canBeDamaged = canBeDamagedPowerup = true;
        cdImmunity = maxCdImmunity;
        cdPowerup = maxCdPowerup;
        activePostProcessing = activeImmunity = activePowerUp = false;
        purpleCanReload = yellowCanReload = shotgunCanReload = false;
        bulletsPurple = bulletsYellow = bulletsShotgun = 0;
        reservedAmmoPurple = reservedAmmoYellow = reservedAmmoShotgun = 0;
        MAX_PURPLE_SHOOT = 15;
        MAX_YELLOW_SHOOT = 10;
        MAX_SHOTGUN_SHOOT = 3;

        MAX_BULLETS_PURPLE = 45;
        MAX_BULLETS_YELLOW = 40;
        MAX_BULLETS_SHOTGUN = 15;

        reloadTime = 1f;
        isReloading = false;
        reloadText.SetActive(false);
        reloadScript = GetComponent<reloadScript>();
        weaponMenu.SetActive(false);
        weaponMenuUp = false;

        cdShield = 0f;
        maxCdShield = 5f;
    }

    void Update()
    {
        WeaponMenu();
        if (!weaponMenuUp)
        {
            ActiveMiniMap();
        }
        resetReload();
        //ActiveMiniMap();
        //healthBarEffect();
        healthBarPixel();

        if (_playerLifes <= 0)
        {
            SoundManagerScript.PlaySound("gameOver");
            GameObject deathEffectGO = Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            Destroy(deathEffectGO, 0.5f);
            GameObject.Find("-----SCENEMANAGEMENT").GetComponent<PlaySceneManager>().isDead = true;
            Destroy(this.gameObject);
        }

        Immunity();
        PowerUp();
        chromaticAberration();

        if (handController.currentPos == 0)
            purpleBulletsCounter.text = "" + reservedAmmoPurple;
        else if (handController.currentPos == 1)
            yellowBulletsCounter.text = "" + reservedAmmoYellow;
        else if (handController.currentPos == 2)
            redBulletsCounter.text = "" + reservedAmmoShotgun;

        if (shieldActivated)
            ActivateShield();

        if (((Input.GetKeyDown(KeyCode.R) &&
             (handController.currentPos == 0 && bulletsPurple < MAX_PURPLE_SHOOT && reservedAmmoPurple > 0 ||
              handController.currentPos == 1 && bulletsYellow < MAX_YELLOW_SHOOT && reservedAmmoYellow > 0 ||
              handController.currentPos == 2 && bulletsShotgun < MAX_SHOTGUN_SHOOT && reservedAmmoShotgun > 0)) ||
             (handController.currentPos == 0 && bulletsPurple <= 0 && reservedAmmoPurple > 0 ||
              handController.currentPos == 1 && bulletsYellow <= 0 && reservedAmmoYellow > 0 ||
              handController.currentPos == 2 && bulletsShotgun <= 0 && reservedAmmoYellow > 0)) && !isReloading)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    void ActiveMiniMap()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            activeCamera.SetActive(true);
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        else
        {
            activeCamera.SetActive(false);
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02F;
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

    void PowerUp()
    {
        if (activePowerUp)
        {
            activePostProcessing = true;
            canBeDamagedPowerup = false;
            activePowerUp = false;
            animator.SetBool("hasPowerup", true);
        }

        if (!canBeDamagedPowerup)
        {
            cdPowerup -= Time.deltaTime;
        }

        if (cdPowerup <= 0)
        {
            animator.SetBool("hasPowerup", false);
            canBeDamagedPowerup = true;
            cdPowerup = maxCdPowerup;
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
        Debug.Log("Waiting");
        yield return new WaitUntil(() => gameObject.GetComponent<reloadScript>().timer <= 0);


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
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && isReloading
        ) // Comprobamos si cambia de arma cuando esta recargando, esto reseteará la recarga.
        {
            reloadScript.fill.fillAmount = 0f;
            reloadScript.fill.enabled = false;
            reloadScript.perTimer.enabled = false;
            reloadScript.timer = 0f;
            StopCoroutine("Reload");
            gameObject.GetComponent<reloadScript>().hasReloaded = false;
            isReloading = false;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && isReloading
        ) // Comprobamos si cambia de arma cuando esta recargando, esto reseteará la recarga.
        {
            reloadScript.fill.fillAmount = 0f;
            reloadScript.fill.enabled = false;
            reloadScript.perTimer.enabled = false;
            reloadScript.timer = 0f;
            StopCoroutine("Reload");
            gameObject.GetComponent<reloadScript>().hasReloaded = false;
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
        for (int i = 0; i <= 6; i++)
        {
            if (_playerLifes == i)
            {
                defaultBar.GetComponent<Image>().sprite = healthbarimages[i];
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("RobotPatrol") || collision.gameObject.CompareTag("Enemy")) &&
            !activePowerUp)
        {
            activeImmunity = true;
        }

        if (collision.gameObject.CompareTag("Ammo"))
        {
            if (noAmmoText.activeInHierarchy)
                noAmmoText.SetActive(false);
            if (reloadText.activeInHierarchy)
                reloadText.SetActive(false);
        }
    }

    void WeaponMenu()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // DESACTIVAMOS EL MOVIMIENTO Y EL APUNTADO
            this.gameObject.GetComponent<playerMovement>().enabled = false;
            aimController1.GetComponent<playerAimWeapon>().enabled = false;
            aimController2.GetComponent<playerAimWeapon>().enabled = false;
            aimController3.GetComponent<playerAimWeapon>().enabled = false;
            aimController4.GetComponent<playerAimWeapon>().enabled = false;

            weaponMenu.SetActive(true);
            weaponMenuUp = true;
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            this.gameObject.GetComponent<playerMovement>().enabled = true;
            aimController1.GetComponent<playerAimWeapon>().enabled = true;
            aimController2.GetComponent<playerAimWeapon>().enabled = true;
            aimController3.GetComponent<playerAimWeapon>().enabled = true;
            aimController4.GetComponent<playerAimWeapon>().enabled = true;

            weaponMenu.SetActive(false);
            weaponMenuUp = false;

            // SELECIONAR EL ARMA DONDE TENIAMOS EL RATÓN
            if (RadialMenu.selection == 0)
            {
                handController.currentPos = 0;
            }
            else if (RadialMenu.selection == 2)
            {
                handController.currentPos = 1;
            }
            else if (RadialMenu.selection == 1)
            {
                handController.currentPos = 2;
            }

            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02F;
        }
    }

    public void ActivateShield()
    {
        if (!shield.activeInHierarchy)
        { 
            shield.SetActive(true);        
        }

        cdShield += Time.deltaTime;
        if (cdShield >= maxCdShield)
        {
            shield.SetActive(false);
            cdShield = 0f;
            shieldActivated = false;
        }
    }
}