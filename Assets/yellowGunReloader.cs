using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yellowGunReloader : MonoBehaviour
{
    public Image imgFill;
    private GameObject player;
    private void Start()
    {
        imgFill.fillAmount = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log((float)player.GetComponent<playerBehaviour>().MAX_BULLETS_YELLOW);
        float imgFillFloat = (float)player.GetComponent<playerBehaviour>().bulletsYellow / (float)player.GetComponent<playerBehaviour>().MAX_YELLOW_SHOOT;
        imgFill.fillAmount = imgFillFloat;

        /* // ME DA PEREZA ARREGLARLO CON UN FOR 
         if (player.GetComponent<playerBehaviour>().bulletsYellow == 10)
         {
             imgFill.fillAmount = 1f;
         }
         else if (player.GetComponent<playerBehaviour>().bulletsYellow == 9)
         {
             imgFill.fillAmount = 0.9f;
         }
         else if (player.GetComponent<playerBehaviour>().bulletsYellow == 8)
         {
             imgFill.fillAmount = 0.8f;
         }
         else if (player.GetComponent<playerBehaviour>().bulletsYellow == 7)
         {
             imgFill.fillAmount = 0.7f;
         }else if (player.GetComponent<playerBehaviour>().bulletsYellow == 6)
         {
             imgFill.fillAmount = 0.6f;
         }
         else if (player.GetComponent<playerBehaviour>().bulletsYellow == 5)
         {
             imgFill.fillAmount = 0.5f;
         }else if (player.GetComponent<playerBehaviour>().bulletsYellow == 4)
         {
             imgFill.fillAmount = 0.4f;
         }
         else if (player.GetComponent<playerBehaviour>().bulletsYellow == 3)
         {
             imgFill.fillAmount = 0.3f;
         }
         else if (player.GetComponent<playerBehaviour>().bulletsYellow == 2)
         {
             imgFill.fillAmount = 0.2f;
         }
         else if (player.GetComponent<playerBehaviour>().bulletsYellow == 1)
         {
             imgFill.fillAmount = 0.1f;
         }
         else if (player.GetComponent<playerBehaviour>().bulletsYellow == 0)
         {
             imgFill.fillAmount = 0.0f;
         }*/
    }
}
