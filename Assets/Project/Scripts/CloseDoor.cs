﻿using System.Collections;
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
        CameraScene.changeCameraAlien = true;
        door.GetComponent<Animator>().SetBool("OpenDoor", false);
        door.GetComponent<Animator>().SetBool("CloseDoor", true);
        alienController.SetActive(true);
        Destroy(this.gameObject);
    }
}
