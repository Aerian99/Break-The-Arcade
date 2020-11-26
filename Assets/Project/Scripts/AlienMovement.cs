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

    // Update is called once per frame
    void Update()
    {
        inRange = Physics2D.OverlapCircle(transform.position, range, layer); //Look if collides with a wall
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, range); //Look visually the range
    }
}

