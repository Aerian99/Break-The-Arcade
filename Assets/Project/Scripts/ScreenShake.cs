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


    // Start is called before the first frame update
    void Start()
    {
        //camara = GameObject.FindWithTag("Camara").GetComponent<CinemachineVirtualCamera>();
        maxCdShake = 0.1f;
        canShake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShake)
        {
            this.gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shake;

            if (cdShake >= maxCdShake)
            {
                canShake = false;
                cdShake = 0;
            }
            cdShake += Time.deltaTime;
        }
        else 
        {
            this.gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
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
