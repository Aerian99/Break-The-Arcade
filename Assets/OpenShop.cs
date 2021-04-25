using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    [HideInInspector]public bool playerIn;
    public Camera camera;
    [HideInInspector]public bool inShop;

    // Start is called before the first frame update
    void Start()
    {
        inShop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIn && Input.GetKeyDown(KeyCode.E) && !inShop)
        {
            GameObject.FindGameObjectWithTag("gameController").GetComponentInChildren<Canvas>().worldCamera = camera;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("gameController").GetComponent<Animator>().SetTrigger("initShop");
            inShop = true;
        }

        else if (inShop && Input.GetKeyDown(KeyCode.E) && playerIn)
        {
            inShop = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).gameObject.active = true;
            GameObject.FindGameObjectWithTag("gameController").GetComponent<Animator>().SetTrigger("backToLevel");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerIn = false;
        }
    }
}
