using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScene : MonoBehaviour
{
    public static bool changeCameraAlien, allEnemiesDefeat;
    public GameObject camaraJugador, camaraAlien;

    // Start is called before the first frame update
    void Start()
    {
        changeCameraAlien = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeCameraAlien)
        {
            camaraJugador.SetActive(false);
            camaraAlien.SetActive(true);
        }

        if(allEnemiesDefeat)
        { 
            camaraJugador.SetActive(true);
            camaraAlien.SetActive(false);
        }
    }
}
