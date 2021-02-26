using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcadeMachine : MonoBehaviour
{
    public GameObject exclamation, dialogCanvas, bocadillo;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exclamation.SetActive(true);
            if(Input.GetKey(KeyCode.E))
            {
                this.gameObject.GetComponent<Animator>().SetBool("turnOn", true);
                bocadillo.SetActive(true);
                dialogCanvas.SetActive(true);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exclamation.SetActive(false);
        }
    }


}
