using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerZone_1 : MonoBehaviour
{
    private int radialEnemy;
    private int robotEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (radialEnemy <= 0 && robotEnemy <= 0)
        {
            Debug.Log("LEVEL CLEAN"); // AQUI HAY QUE ABRIR LAS PUERTAS
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            radialEnemy++;
        }
        
        if (other.gameObject.CompareTag("RobotPatrol"))
        {
            robotEnemy++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            radialEnemy--;
        }
        
        if (other.gameObject.CompareTag("RobotPatrol"))
        {
            robotEnemy--;
        }
    }
}