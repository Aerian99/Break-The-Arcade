using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CloseDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject alienController;

    private void Start()
    {
        alienController.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CameraScene.changeCameraAlien = true;
            alienController.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
