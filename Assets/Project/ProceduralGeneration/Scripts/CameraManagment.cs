using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagment : MonoBehaviour
{
    public static GameObject cam1, cam2;
    private static bool isActivecam1, isActivecam2;
    private static float cd, maxCd;
    private static bool justActivated;
    private static Vector3 savePos;

    private void Start()
    {
        justActivated = false;
        maxCd = 0.5f;
        cd = maxCd;
        cam1 = GameObject.FindGameObjectWithTag("Camera1");
        cam2 = GameObject.FindGameObjectWithTag("Camera2");
        cam1.SetActive(true);
        cam2.SetActive(false);
        isActivecam1 = true;
        isActivecam2 = false;
    }
    private void Update()
    {
        if (cam1.activeInHierarchy)
        {
            isActivecam1 = true;
        }
        else
        {
            isActivecam1 = false;
        }

        if (cam2.activeInHierarchy)
        {
            isActivecam2 = true;

        }
        else
        {
            isActivecam2 = false;
        }

        if (isActivecam1 && cd <= 0.0f)
        {
            cam1.transform.position = savePos;
            cd = maxCd;
            justActivated = false;
        }
        if (isActivecam2 && cd <= 0.0f)
        {
            cam2.transform.position = savePos;
            cd = maxCd;
            justActivated = false;
        }

        if(justActivated)
            cd -= Time.deltaTime;
    }


    public static void changeCameras(Vector3 position)
    {
        savePos = position;
        justActivated = true;
        if (isActivecam1 && cd == maxCd)
        {
            cam2.transform.position = savePos;
            cam2.SetActive(true);
            cam1.SetActive(false);
        }

        if (isActivecam2 && cd == maxCd)
        {
            cam1.transform.position = savePos;
            cam1.SetActive(true);
            cam2.SetActive(false);
        }

    }
}
