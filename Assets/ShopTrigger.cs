using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject e_button;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        e_button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        e_button.SetActive(false);
    }
}
