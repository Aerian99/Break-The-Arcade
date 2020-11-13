using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateHands : MonoBehaviour
{
    public float offset;
    public GameObject gun;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + offset);


        /*if (rotationZ < 89 && rotationZ > -89)
        {
            gun.transform.rotation = Quaternion.Euler(gun.transform.rotation.x, 0, gun.transform.rotation.z);
        }
        else 
        {
           gun.transform.rotation = Quaternion.Euler(gun.transform.rotation.x, 180, gun.transform.rotation.z);
        }*/
    }

}
