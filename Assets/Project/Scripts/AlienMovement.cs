using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovement: MonoBehaviour
{
    private float range;
    public static bool inRange;
    public LayerMask layer;
    

    // Start is called before the first frame update
    void Start()
    {
        range = 4;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        { 
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            inRange = false;
        }
    }

}

