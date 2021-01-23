using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialTrigger : MonoBehaviour
{
    private int robotEnemy;

    private bool hasPassedLevel = false;
    public GameObject[] enemies;
    public Vector3[] positions;

    public bool isAlien;

    // Update is called once per frame
    void Update()
    {
        if (robotEnemy <= 0)
        {
            hasPassedLevel = true;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasPassedLevel)
            {
                if (!isAlien)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        Instantiate(enemies[i], positions[i], Quaternion.identity);
                    }
                }
            }
        }
    }
}
