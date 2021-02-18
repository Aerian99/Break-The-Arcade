using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSelector : MonoBehaviour
{
    public GameObject cursor;
    public GameObject weaponMenu;
    void Start()
    {
        weaponMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            weaponMenu.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            weaponMenu.SetActive(false);
        }
    }
}
