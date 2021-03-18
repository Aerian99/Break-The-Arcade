using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcadeMachine : MonoBehaviour
{
    public GameObject exclamation, dialogCanvas, bocadillo, purple, yellow, red, e_button;
    private bool inTrigger = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E) && player.GetComponent<playerMovement>().enabled)
        {
                this.gameObject.GetComponent<Animator>().SetBool("turnOn", true);

                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                player.GetComponent<Animator>().SetBool("isRunning", false);
                player.GetComponent<playerMovement>().enabled = false;

                if (purple.activeInHierarchy)
                    purple.GetComponent<PurpleShoot>().enabled = false;
                else if (yellow.activeInHierarchy)
                    yellow.GetComponent<LaserShoot>().enabled = false;
                else if (red.activeInHierarchy)
                    red.GetComponent<RedShoot>().enabled = false;

                bocadillo.SetActive(true);
                if (dialogCanvas.activeInHierarchy && !dialogCanvas.GetComponent<DialogManager>().typing)
                {
                    dialogCanvas.GetComponent<DialogManager>().typing = true;
                    dialogCanvas.GetComponent<DialogManager>().index = 0;
                    dialogCanvas.GetComponent<DialogManager>().StartCoroutine(dialogCanvas.GetComponent<DialogManager>().Typing());
                }
                dialogCanvas.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exclamation.SetActive(true);
            e_button.SetActive(true);
            inTrigger = true;
        }
    }

   

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exclamation.SetActive(false);
            e_button.SetActive(false);
            inTrigger = false;
        }
    }


}
