using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class ActivateDash : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hey");
            player.GetComponent<playerMovement>().dashImage.fillAmount = 1; 
            player.GetComponent<playerMovement>().dashCooldown = 3;
            player.GetComponent<playerMovement>().canDash = true;
            player.GetComponent<playerMovement>().dashDiagonal = true;
            player.GetComponent<playerMovement>().dashUp = true;
        }
    }
}
