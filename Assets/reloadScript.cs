using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class reloadScript : MonoBehaviour
{
    public float timer;
    private float waitTime;
    public Image fill;
    public TextMeshProUGUI perTimer;
    private playerBehaviour p_behaviourScript;
    public static bool hitReload;

    void Start()
    {
        timer = 0f;
        waitTime = 2f;
        fill.fillAmount = 0f;
        fill.enabled = false;
        perTimer.enabled = false;
        p_behaviourScript = GetComponent<playerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fill.fillAmount >= 1f)
        {
            fill.enabled = false;
            perTimer.enabled = false;
        }

        if ((Input.GetKeyDown(KeyCode.R) &&
             (handController.currentPos == 0 && playerBehaviour.bulletsPurple < playerBehaviour.MAX_PURPLE_SHOOT ||
              handController.currentPos == 1 && playerBehaviour.bulletsYellow < playerBehaviour.MAX_YELLOW_SHOOT ||
              handController.currentPos == 2 && playerBehaviour.bulletsShotgun < playerBehaviour.MAX_SHOTGUN_SHOOT)) ||
            (handController.currentPos == 0 && playerBehaviour.bulletsPurple <= 0|| 
             handController.currentPos == 1 && playerBehaviour.bulletsYellow <= 0||
             handController.currentPos == 2 && playerBehaviour.bulletsShotgun <= 0))
        {
            if (playerBehaviour.reservedAmmoPurple != 0 && handController.currentPos == 0)
            {
                fill.enabled = true;
                perTimer.enabled = true;
                timer = this.GetComponent<playerBehaviour>().reloadTime;
                fill.fillAmount = 0f;
            }

            if (playerBehaviour.reservedAmmoYellow != 0 && handController.currentPos == 1)
            {
                fill.enabled = true;
                perTimer.enabled = true;
                timer = this.GetComponent<playerBehaviour>().reloadTime;
                fill.fillAmount = 0f;
            }

            if (playerBehaviour.reservedAmmoShotgun != 0 && handController.currentPos == 2)
            {
                fill.enabled = true;
                perTimer.enabled = true;
                timer = this.GetComponent<playerBehaviour>().reloadTime;
                fill.fillAmount = 0f;
            }
        }
        ReloadCountdown();
    }

    void ReloadCountdown() // Llena la imagen circular de reload
    {
        if (timer > 0)
        {
            playerBehaviour.isReloading = false;
            timer -= Time.deltaTime;
            fill.fillAmount += 1.0f / this.GetComponent<playerBehaviour>().reloadTime * Time.deltaTime;
            perTimer.text = (int) (fill.fillAmount * 100) + "%";
        }
    }
    
}