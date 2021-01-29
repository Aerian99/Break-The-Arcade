using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redGunReloader : MonoBehaviour
{
    public GameObject[] bulletsHolder;
    
    // Update is called once per frame
    void Update()
    {
        // LOOP REVERSIVO PARA ELIMINAR LAS BALAS
        for (int i = playerBehaviour.MAX_SHOTGUN_SHOOT - 1; i >= playerBehaviour.bulletsShotgun; i--)
        {
            bulletsHolder[i].SetActive(false);
        }
        
        // LOOP PARA RELLENAR LAS BALAS
        for (int j = 0; j < playerBehaviour.bulletsShotgun; j++)
        {
            bulletsHolder[j].SetActive(true);
        }
    }
}
