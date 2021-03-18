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
    public static bool hitReload;
    private GameObject player;
    public bool hasReloaded;

    void Start()
    {
        timer = 0f;
        waitTime = 2f;
        fill.fillAmount = 0f;
        fill.enabled = false;
        perTimer.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (fill.fillAmount >= 1f)
        {
            fill.enabled = false;
            perTimer.enabled = false;
        }

        if (((Input.GetKeyDown(KeyCode.R) &&
             (handController.currentPos == 0 && player.GetComponent<playerBehaviour>().bulletsPurple < player.GetComponent<playerBehaviour>().MAX_PURPLE_SHOOT ||
              handController.currentPos == 1 && player.GetComponent<playerBehaviour>().bulletsYellow < player.GetComponent<playerBehaviour>().MAX_YELLOW_SHOOT ||
              handController.currentPos == 2 && player.GetComponent<playerBehaviour>().bulletsShotgun < player.GetComponent<playerBehaviour>().MAX_SHOTGUN_SHOOT)) ||
            (handController.currentPos == 0 && player.GetComponent<playerBehaviour>().bulletsPurple <= 0 ||
             handController.currentPos == 1 && player.GetComponent<playerBehaviour>().bulletsYellow <= 0 ||
             handController.currentPos == 2 && player.GetComponent<playerBehaviour>().bulletsShotgun <= 0)) && !hasReloaded)
        {
            if (player.GetComponent<playerBehaviour>().reservedAmmoPurple != 0 && handController.currentPos == 0)
            {
                fill.enabled = true;
                perTimer.enabled = true;
                timer = this.GetComponent<playerBehaviour>().reloadTime;
                fill.fillAmount = 0f;
            }
            else if (player.GetComponent<playerBehaviour>().reservedAmmoYellow != 0 && handController.currentPos == 1)
            {
                fill.enabled = true;
                perTimer.enabled = true;
                timer = this.GetComponent<playerBehaviour>().reloadTime;
                fill.fillAmount = 0f;
            }
            else if (player.GetComponent<playerBehaviour>().reservedAmmoShotgun != 0 && handController.currentPos == 2)
            {
                fill.enabled = true;
                perTimer.enabled = true;
                timer = this.GetComponent<playerBehaviour>().reloadTime;
                fill.fillAmount = 0f;
            }
            hasReloaded = true;

        }
        if (hasReloaded)
            ReloadCountdown();
    }

    void ReloadCountdown() // Llena la imagen circular de reload
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            fill.fillAmount += 1.0f / this.GetComponent<playerBehaviour>().reloadTime * Time.deltaTime;
            perTimer.text = (int)(fill.fillAmount * 100) + "%";
        }
        else
        {
            timer = -1f;
            hasReloaded = false;
        }
    }
    
}