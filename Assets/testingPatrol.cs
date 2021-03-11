using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class testingPatrol : MonoBehaviour
{
    [HideInInspector] public float patrolSpeed;
    private float patrolDistance;
    private Rigidbody2D rb;
    private GameObject canvasGO;

    private bool movingRight = true;
    public Transform groundDetecion;
    private Animator anim;
    public LayerMask platformLayer;

    private Vector2 vecDir;

    private RaycastHit2D groundInfo;
    private RaycastHit2D groundInfo2;
    private RaycastHit2D groundInfo3;

    private GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        patrolSpeed = 2f;
        patrolDistance = 1f;
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        groundInfo = Physics2D.Raycast(groundDetecion.position, Vector2.down, patrolDistance, platformLayer);
        changeDirection();
        triggerDetection();
    }

    void changeDirection()
    {
        rb.velocity = Vector2.right * patrolSpeed;
        anim.SetBool("isRunning", true);
    }

    void triggerDetection()
    {
        if (groundInfo.collider == false)
        {
            transform.eulerAngles -= new Vector3(0, 0, -90);
        }
    }
}