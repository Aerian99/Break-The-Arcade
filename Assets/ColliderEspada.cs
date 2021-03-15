using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEspada : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if(player.GetComponent<playerBehaviour>().canBeDamaged)
                player.GetComponent<playerBehaviour>().activeImmunity = true;
        }
    }
}
