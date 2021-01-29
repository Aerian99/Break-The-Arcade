using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yellowGunReloader : MonoBehaviour
{
    public Image imgFill;
    private void Start()
    {
        imgFill.fillAmount = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        // ME DA PEREZA ARREGLARLO CON UN FOR 
        if (playerBehaviour.bulletsYellow == 10)
        {
            imgFill.fillAmount = 1f;
        }
        else if (playerBehaviour.bulletsYellow == 9)
        {
            imgFill.fillAmount = 0.9f;
        }
        else if (playerBehaviour.bulletsYellow == 8)
        {
            imgFill.fillAmount = 0.8f;
        }
        else if (playerBehaviour.bulletsYellow == 7)
        {
            imgFill.fillAmount = 0.7f;
        }else if (playerBehaviour.bulletsYellow == 6)
        {
            imgFill.fillAmount = 0.6f;
        }
        else if (playerBehaviour.bulletsYellow == 5)
        {
            imgFill.fillAmount = 0.5f;
        }else if (playerBehaviour.bulletsYellow == 4)
        {
            imgFill.fillAmount = 0.4f;
        }
        else if (playerBehaviour.bulletsYellow == 3)
        {
            imgFill.fillAmount = 0.3f;
        }
        else if (playerBehaviour.bulletsYellow == 2)
        {
            imgFill.fillAmount = 0.2f;
        }
        else if (playerBehaviour.bulletsYellow == 1)
        {
            imgFill.fillAmount = 0.1f;
        }
        else if (playerBehaviour.bulletsYellow == 0)
        {
            imgFill.fillAmount = 0.0f;
        }
    }
}
