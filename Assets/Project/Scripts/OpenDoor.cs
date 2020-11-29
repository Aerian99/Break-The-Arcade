using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject door;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        door.GetComponent<Animator>().SetBool("OpenDoor", true);
    }
}
