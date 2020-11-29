using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    private float cd, maxCd, speed;
    private bool justActive;
    private void Start()
    {
        speed = 2f;
        maxCd = 1f;
        cd = maxCd;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (cd <= 0)
            Movement();
        cd -= Time.fixedDeltaTime;

        if (this.gameObject.transform.childCount == 0)
        {
            CameraScene.allEnemiesDefeat = true;
        }
    }


    void Movement()
    {
        if (AlienMovement.inRange && !justActive)
        {
            speed = -speed;
            this.transform.Translate(new Vector3(0, -2, 0));
            justActive = true;
        }
        else
        {
            this.transform.Translate(new Vector3(speed, 0, 0));
            justActive = false;
            AlienMovement.inRange = false;
        }
        cd = maxCd;
    
    }
}
