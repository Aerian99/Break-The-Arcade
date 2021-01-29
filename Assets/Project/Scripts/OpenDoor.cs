using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject door;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            door.GetComponent<Animator>().SetBool("CloseDoor", false);
            door.GetComponent<Animator>().SetBool("OpenDoor", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.GetComponent<Animator>().SetBool("OpenDoor", false);
            door.GetComponent<Animator>().SetBool("CloseDoor", true);
        }
    }
}
