using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class purpleGunReloader : MonoBehaviour
{
    public GameObject[] bulletsHolder;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            // LOOP REVERSIVO PARA ELIMINAR LAS BALAS
            for (int i = player.GetComponent<playerBehaviour>().MAX_PURPLE_SHOOT - 1; i >= player.GetComponent<playerBehaviour>().bulletsPurple; i--)
            {
                bulletsHolder[i].SetActive(false);
            }
        
            // LOOP PARA RELLENAR LAS BALAS
            for (int j = 0; j < player.GetComponent<playerBehaviour>().bulletsPurple; j++)
            {
                bulletsHolder[j].SetActive(true);
            }
        }
    }
}
