using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    bool nextRoom = false;
    public Vector3 position, position2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!nextRoom)
            { 
                CameraManagment.changeCameras(position);
                nextRoom = true;
            }
            else
            { 
                CameraManagment.changeCameras(position2);
                nextRoom = false;
            }
        }

    }
}
