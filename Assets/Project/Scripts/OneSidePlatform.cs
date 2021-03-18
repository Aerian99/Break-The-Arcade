using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class OneSidePlatform : MonoBehaviour
{
    private PlatformEffector2D pE;
    private float waitTime;
    private bool justPressed;

    void Start()
    {
        waitTime = 0;
        pE = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

   
    private void OnCollisionExit2D(Collision2D collision)
    {
        pE.rotationalOffset = 0f;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                pE.rotationalOffset = 180f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                pE.rotationalOffset = 0f;
            }
        }
    }
}