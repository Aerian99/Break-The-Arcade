using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class OneSidePlatform : MonoBehaviour
{
    private PlatformEffector2D pE;
    private float waitTime;

    void Start()
    {
        pE = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTime = 0.05f;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (waitTime <= 0)
            {
                pE.rotationalOffset = 180f;
                waitTime = 0.05f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pE.rotationalOffset = 0f;
        }
    }
}