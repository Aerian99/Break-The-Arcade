using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ScreenShake : MonoBehaviour
{
    private float cdShake, maxCdShake;
    public static bool canShake;
    public static float shake;
    private CinemachineBasicMultiChannelPerlin cinemachVR;

    // Start is called before the first frame update
    void Start()
    {
        //camara = GameObject.FindWithTag("Camara").GetComponent<CinemachineVirtualCamera>();
        maxCdShake = 0.1f;
        canShake = false;
        shake = 0.0f;
    }

    private void Awake()
    {
        CinemachineVirtualCamera cVR = gameObject.GetComponent<CinemachineVirtualCamera>();
        if(cVR == null)
        {
            Debug.Log(name);
        }
        cinemachVR = cVR.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canShake)
        {
            cinemachVR.m_AmplitudeGain = shake;
            //cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shake;

            if (cdShake >= maxCdShake)
            {
                canShake = false;
                cdShake = 0;
            }
            cdShake += Time.deltaTime;
        }
        else
        {
            cinemachVR.m_AmplitudeGain = 0f;
        }
    }
}
