using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerZone_1 : MonoBehaviour
{
    private int radialEnemy;
    private int robotEnemy, tower, alien;

    public GameObject[] doors;
    private bool hasPassedLevel = false;
    public GameObject[] enemies;
    public Vector3[] positions;

    // Start is called before the first frame update
    void Start()
    {
        radialEnemy = enemies.Length;
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (radialEnemy <= 0 && robotEnemy <= 0 && tower <= 0 && alien <= 0)
        {
            hasPassedLevel = true;
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
            if(!hasPassedLevel)
            { 
                for (int i = 0; i < enemies.Length; i++)
                {
                    Instantiate(enemies[i], positions[i], Quaternion.identity);
                }
                if (radialEnemy > 0)
                {
                    for (int i = 0; i < doors.Length; i++)
                    {
                        doors[i].GetComponent<Animator>().SetBool("hasPassed", true);
                        doors[i].GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Tower") || other.gameObject.CompareTag("RobotPatrol") || other.gameObject.CompareTag("AlienEnemy"))
        {
            radialEnemy--;
        }

    }
}