using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private float cdMove, maxCdMove, range;
    public static bool inRange, moveRight, moveLeft, justActive;
    public LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        range = 4;
        rb = GetComponent<Rigidbody2D>();
        maxCdMove = 2;
        cdMove = maxCdMove;
        moveRight = true;
        moveLeft = justActive = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        cdMove -= Time.fixedDeltaTime;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, range);
    }

    private void Movement()
    {

        if (!justActive)
        {
            inRange = Physics2D.OverlapCircle(transform.position, range, layer);
        }
        if (cdMove <= 0)
        {
            if (inRange && !justActive)
            {
                rb.transform.position = new Vector3(transform.position.x, transform.position.y - 3, 0);
                if (moveRight)
                {
                    moveLeft = true;
                    moveRight = false;
                    justActive = true;
                    inRange = false;
                }
                else if (moveLeft)
                {
                    moveRight = true;
                    moveLeft = false;
                    justActive = true;
                    inRange = false;
                }
            }
            else if (moveRight)
            {
                rb.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
                justActive = false;
            }
            else if (moveLeft)
            {
                rb.transform.position = new Vector3(transform.position.x - 2, transform.position.y, 0);
                justActive = false;
            }
            
           
            cdMove = maxCdMove;
        }
    }
}

