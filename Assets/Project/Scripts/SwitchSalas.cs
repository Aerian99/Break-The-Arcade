using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSalas : MonoBehaviour
{
    public GameObject vCam;
    public Vector3 newPos;
    public GameObject focos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            vCam.SetActive(true);
            focos.transform.position = newPos;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            vCam.SetActive(false);
        }
    }
}
