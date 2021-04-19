using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSalas : MonoBehaviour
{
    
    public GameObject vCam/*, cameraMiniMap*/;
    public Vector3 newPos;
    public GameObject focos/*, minimapSquare*/;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            vCam.SetActive(true);
            focos.transform.position = newPos;
            //cameraMiniMap.transform.position = new Vector3(minimapSquare.transform.position.x, minimapSquare.transform.position.y, cameraMiniMap.transform.position.z);
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
