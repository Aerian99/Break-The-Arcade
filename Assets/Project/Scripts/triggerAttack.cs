using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerAttack : MonoBehaviour
{
    Quaternion rotation;
    // Making child not to rotate as parent does.
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            joystickShoot.isInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            joystickShoot.isInside = false;
        }
    }
}
