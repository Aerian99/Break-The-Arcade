using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrigger : MonoBehaviour
{
    public GameObject actualCamera, lavaCamera, lava;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Player")
        {
            actualCamera.SetActive(false);
            lavaCamera.SetActive(true);
            lava.SetActive(true);
            Destroy(gameObject);
        }
    }
}
