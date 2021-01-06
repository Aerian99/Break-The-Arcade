using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class purpleGunReloader : MonoBehaviour
{
    public GameObject[] bulletsHolder;

    // Update is called once per frame
    void Update()
    {
        // LOOP REVERSIVO PARA ELIMINAR LAS BALAS
        for (int i = playerBehaviour.MAX_PURPLE_SHOOT - 1; i >= playerBehaviour.bulletsPurple; i--)
        {
            bulletsHolder[i].SetActive(false);
        }
        
        // LOOP PARA RELLENAR LAS BALAS
        for (int j = 0; j < playerBehaviour.bulletsPurple; j++)
        {
            bulletsHolder[j].SetActive(true);
        }
    }
}
