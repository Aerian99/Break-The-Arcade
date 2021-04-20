using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    bool playerIn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIn && Input.GetKeyDown(KeyCode.E))
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).gameObject.SetActive(false); 
            GameObject.FindGameObjectWithTag("gameController").GetComponent<Animator>().SetTrigger("initShop");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerIn = true;
        }
    }
}
