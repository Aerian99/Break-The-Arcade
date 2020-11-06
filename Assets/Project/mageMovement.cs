using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageMovement : MonoBehaviour
{
    private const float MOVE_SPEED = 1f;
    public Animator animator;
    public GameObject goPlayer;
    public SpriteRenderer mage;
    public SpriteRenderer actualGun;
    public Sprite mageUp;
    public Sprite mageDown;
    public Sprite mageLeft;
    public Sprite mageRight;
    public Sprite mageUpLeft;
    public Sprite mageUpRight;

    Vector3 v3Pos;
    float fAngle;

    private Rigidbody2D rb;
    private Vector3 moveDir;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        calculateAngle();
        spriteDirection();
    }
    void FixedUpdate()
    {
        rb.velocity = moveDir * MOVE_SPEED;
    }

    void calculateAngle()
    {

        v3Pos = Input.mousePosition;
        v3Pos.z = (goPlayer.transform.position.z - Camera.main.transform.position.z);
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
        v3Pos = v3Pos - goPlayer.transform.position;
        fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;

        if (fAngle < 0.0f)
        {
            fAngle += 360.0f;
        }
    }

    void spriteDirection()
    {
        /*if (fAngle > 0f && fAngle < 60)
        {
            mage.sprite = mageUpRight;
            animator.SetTrigger("mouseUpRight");
        }
        else if (fAngle > 60 && fAngle < 120)
        {
            mage.sprite = mageUp;
            animator.SetTrigger("mouseUp");
        }
        else if (fAngle > 120 && fAngle < 180)
        {
            mage.sprite = mageUpLeft;
            animator.SetTrigger("mouseUpLeft");
        }
        else if (fAngle > 240 && fAngle < 300)
        {
            mage.sprite = mageDown;
            animator.SetTrigger("mouseDown");
        }
        if (fAngle > 180 && fAngle < 240)
        {
            mage.sprite = mageLeft;
            animator.SetTrigger("mouseLeft");
        }
        else if (fAngle > 300 && fAngle < 360)
        {
            mage.sprite = mageRight;
            animator.SetTrigger("mouseRight");
        }*/
    }

    void Move()
    {
        float moveX = 0f;
        float moveY = 0f;

        /*if (Input.GetKey(KeyCode.W)) 
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            moveY = -1f;
        }*/
        if (Input.GetKey(KeyCode.A)) 
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            moveX = +1f;
        }
        moveDir = new Vector3(moveX, moveY).normalized;
    }
}
