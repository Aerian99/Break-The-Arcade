using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class OneSidePlatform : MonoBehaviour
{
    private PlatformEffector2D pE;

    void Start()
    {
        pE = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            pE.rotationalOffset = 180f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            pE.rotationalOffset = 0f;
        }
    }
}