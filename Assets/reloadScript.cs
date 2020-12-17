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

        if (Input.GetKeyDown(KeyCode.R) && playerBehaviour.isReloading)
        {
            if (playerBehaviour.reservedAmmoPurple != 0 && handController.currentPos == 0)
            {
                fill.enabled = true;
                perTimer.enabled = true;
                timer = 2f;
                fill.fillAmount = 0f;
            }

            if (playerBehaviour.reservedAmmoYellow != 0 && handController.currentPos == 1)
            {
                fill.enabled = true;
                perTimer.enabled = true;
                timer = 2f;
                fill.fillAmount = 0f;
            }

            if (playerBehaviour.reservedAmmoShotgun != 0 && handController.currentPos == 2)
            {
                fill.enabled = true;
                perTimer.enabled = true;
                timer = 2f;
                fill.fillAmount = 0f;
            }
        }
        ReloadCountdown();
    }

    void ReloadCountdown()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            fill.fillAmount += 1.0f / waitTime * Time.deltaTime;
            perTimer.text = (int) (fill.fillAmount * 100) + "%";
        }
    }
}