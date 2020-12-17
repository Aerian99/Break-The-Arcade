using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagment : MonoBehaviour
{
    public static GameObject cam1, cam2;
    private static bool isActivecam1, isActivecam2;

    private void Start()
    {
        cam1 = GameObject.FindGameObjectWithTag("Camera1");
        cam2 = GameObject.FindGameObjectWithTag("Camera2");
        cam1.SetActive(true);
        cam2.SetActive(false);
        isActivecam1 = true;
        isActivecam2 = false;
    }



    public static void changeCameras(Vector3 position)
    {
        if (isActivecam1)
        {
            cam1.SetActive(false);
            cam1.transform.position = position;
        }
        else if (!isActivecam1)
        {
            cam1.SetActive(true);
            cam1.transform.position = position;
        }

        if (isActivecam2)
        {
            cam2.SetActive(false);
            cam2.transform.position = position;

        }
        else if (!isActivecam2)
        {
            cam2.SetActive(true);
            cam2.transform.position = position;
        }
    
    
    }
}
