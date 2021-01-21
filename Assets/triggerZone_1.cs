using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerZone_1 : MonoBehaviour
{
    private int radialEnemy;
    private int robotEnemy, tower;

    public GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (radialEnemy <= 0 && robotEnemy <= 0)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<Animator>().SetBool("hasPassed", false);
                doors[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (radialEnemy > 0 || robotEnemy > 0)
            {
                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].GetComponent<Animator>().SetBool("hasPassed", true);
                    doors[i].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            radialEnemy++;
        }
        if (other.gameObject.CompareTag("Tower"))
        {
            tower++;
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

        if (other.gameObject.CompareTag("Tower"))
        {
            tower--;
        }

        if (other.gameObject.CompareTag("RobotPatrol"))
        {
            robotEnemy--;
        }
    }
}