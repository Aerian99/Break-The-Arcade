using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    float cdAbsorb, maxcdAbsorb;
    [HideInInspector]public bool activatedAbsorb;
    GameObject[] robotPatrols;
    // Start is called before the first frame update
    void Start()
    {
        maxcdAbsorb = 6f;
        cdAbsorb = maxcdAbsorb;
        activatedAbsorb = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activatedAbsorb)
        {
            robotPatrols = GameObject.FindGameObjectsWithTag("RobotPatrol");
            for (int i = 0; i < robotPatrols.Length; i++)
            {
                robotPatrols[i].GetComponent<enemyPatrol>().patrolSpeed = 6f;
                robotPatrols[i].GetComponent<Animator>().SetBool("absorbed", true);
            }

            if (cdAbsorb <= 0)
            {
                cdAbsorb = maxcdAbsorb;
                activatedAbsorb = false;
                deactivateEnemyStates();
            }
           
            cdAbsorb -= Time.deltaTime;
        }

      
    }

    void deactivateEnemyStates()
    {
        robotPatrols = GameObject.FindGameObjectsWithTag("RobotPatrol");
        for (int i = 0; i < robotPatrols.Length; i++)
        {
            robotPatrols[i].GetComponent<Animator>().SetBool("absorbed", false);
            robotPatrols[i].GetComponent<enemyPatrol>().patrolSpeed = 2f;
        }

    }

    public void ActivateEnemyStates()
    {
        activatedAbsorb = true;
    }
}
