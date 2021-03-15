using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierAlienDead : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AlienEnemy"))
        {
            player.GetComponent<playerBehaviour>()._playerLifes = 0;
        }
    }
}
