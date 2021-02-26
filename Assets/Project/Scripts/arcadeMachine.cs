using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcadeMachine : MonoBehaviour
{
    public GameObject exclamation, dialogCanvas, bocadillo, purple, yellow, red;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exclamation.SetActive(true);
            if(Input.GetKey(KeyCode.E))
            {
                this.gameObject.GetComponent<Animator>().SetBool("turnOn", true);

                if(purple.activeInHierarchy)
                    purple.GetComponent<PurpleShoot>().enabled = false;
                else if(yellow.activeInHierarchy)
                    yellow.GetComponent<LaserShoot>().enabled = false;
                else if(red.activeInHierarchy)
                    red.GetComponent<RedShoot>().enabled = false;

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
