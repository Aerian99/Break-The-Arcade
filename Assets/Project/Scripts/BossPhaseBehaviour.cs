using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseBehaviour : MonoBehaviour
{

    public float speed;
    Rigidbody2D EnemyRB;
    public GameObject rightCheck, roofCheck, groundCheck;
    public LayerMask groundLayer;
    private bool facingRight = true, groundTouch, roofTouch, rightTouch;
    public float dirX = 1, DIRy = 0.25f;
    public float circleRadius;
    enum Phases { INITPHASE, PHASE2, PHASE3}
    Phases phase;
    public int health, maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        speed = 750;
        EnemyRB = GetComponent<Rigidbody2D>();
        health = maxHealth = 1000;
        phase = Phases.INITPHASE;
        StartCoroutine(shootBehaviour());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(health < maxHealth / 4)
        {
            phase = Phases.PHASE3;
        }
        else if(health < maxHealth / 2)
        {
            phase = Phases.PHASE2;
        }
        else
        {
            phase = Phases.INITPHASE;
        }
    }

    void Movement()
    {
        EnemyRB.velocity = new Vector2(dirX, DIRy) * speed * Time.deltaTime;
        HitDetection();
    }


    void HitDetection()
    {

        rightTouch = Physics2D.OverlapCircle(rightCheck.transform.position, circleRadius, groundLayer);
        roofTouch = Physics2D.OverlapCircle(roofCheck.transform.position, circleRadius, groundLayer);
        groundTouch = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);
        HitLogic();

    }

    void HitLogic()
    {
        if(rightTouch && facingRight)
        {
            Flip();
        }
        else if(rightTouch && !facingRight)
        {
            Flip();


        }
        if(roofTouch)
        {
            DIRy = -0.25F;
        }else if(groundTouch)
        {
            DIRy = 0.25f;
        }



    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        dirX = -dirX;
    }
    IEnumerator shootBehaviour()
    {
        while (true)
        {
            if(phase == Phases.INITPHASE)
            {
                //Shoot


                yield return new WaitForSeconds(5);
            }else if(phase == Phases.PHASE2)
            {
                speed = 1000;
                //Shoot

                yield return new WaitForSeconds(2.5f);
            }
            else if(phase == Phases.PHASE3)
            {
                //Shoot
                //Shoot
                speed = 1250;

                yield return new WaitForSeconds(1.5f);
            }

        }
        
    }
}
