using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeAI : MonoBehaviour
{
    private float speed;
    private float distance;
    private bool movingRight = true;

    private Transform groundDetection;

    private void Start() 
    {
        speed = 3f;
        distance = 1f;

        groundDetection = this.transform.GetChild(0).transform;

        if (movingRight == true) 
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                this.GetComponent<SpriteRenderer>().flipX = true;
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                this.GetComponent<SpriteRenderer>().flipX = true;
                movingRight = true;
            }
        }
    }
}
