using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{

    [HideInInspector]public Vector3 newPos;

   public void Move()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = newPos;
    }
}
