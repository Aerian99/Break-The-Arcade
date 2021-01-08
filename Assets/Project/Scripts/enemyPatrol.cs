using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    private float patrolSpeed;
    private float patrolDistance;
    private Rigidbody2D rb;
    private GameObject canvasGO;

    private bool movingRight = true;
    public Transform groundDetecion;
    private Animator anim;

    private Vector2 vecDir;

    private RaycastHit2D groundInfo;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        patrolSpeed = 2f;
        patrolDistance = 1f;
        anim = GetComponent<Animator>();
        vecDir = new Vector2(180, 0f);
        //canvasGO = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        groundInfo = Physics2D.Raycast(groundDetecion.position, Vector2.down, patrolDistance);
        changeDirection();
        triggerDetection();
    }

    void changeDirection()
    {
        if (movingRight == true)
        {
            rb.velocity = Vector2.right * patrolSpeed;
            anim.SetBool("isRunning", true);
        }
        else
        {
            rb.velocity = Vector2.left * patrolSpeed;
            anim.SetBool("isRunning", true);
        }
    }

    void triggerDetection()
    {
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                //canvasGO.transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                //canvasGO.transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    public bool getMovingRight()
    {
        return movingRight;
    
    }
}