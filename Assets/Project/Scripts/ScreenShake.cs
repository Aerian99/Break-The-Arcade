using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ScreenShake : MonoBehaviour
{
    private float cdShake, maxCdShake;
    public static bool canShake;
    //public CinemachineVirtualCamera camara, camara2/*, cameraAlien*/;
    public static float shake;
    CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        //cam = gameObject.GetComponent<CinemachineVirtualCamera>();
        //camara = GameObject.FindWithTag("Camara").GetComponent<CinemachineVirtualCamera>();
        maxCdShake = 0.1f;
        canShake = false;
        shake = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShake)
        {

            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shake;

            if (cdShake >= maxCdShake)
            {
                canShake = false;
                cdShake = 0;
            }
            cdShake += Time.deltaTime;
        }
        else 
        {
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
        }

        /*
        if (canShake)
        {
            if(camara.isActiveAndEnabled)
            { 
                camara.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shake;
            }
            else if (camara2.isActiveAndEnabled)
            { 
                camara2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shake;
            }
            else
            {
                cameraAlien.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shake;
            }
            if (cdShake >= maxCdShake)
            {
                canShake = false;
                cdShake = 0;
            }


            cdShake += Time.deltaTime;
        }
        else
        {
            if (camara.isActiveAndEnabled)
                camara.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;

            if (camara2.isActiveAndEnabled)
                camara2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
            else
            {
                cameraAlien.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
            }
        }*/

    }
}
