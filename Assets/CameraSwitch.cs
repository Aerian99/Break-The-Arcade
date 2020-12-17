using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            CameraManagment.changeCameras(new Vector3(transform.position.x + 5, transform.position.y + 5, transform.position.z - 1));
        }

    }
}
