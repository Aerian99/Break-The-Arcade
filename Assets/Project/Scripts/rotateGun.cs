using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateGun : MonoBehaviour
{
    public GameObject player;
    public GameObject arm;
    private Vector3 v3Pos;
    private float fAngle;

    // Update is called once per frame
    void Update()
    {
        faceMouse();
        calculateAngle();
    }
    void FixedUpdate()
    {
        //calculateAngle();
    }

    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
        mousePosition.x - transform.position.x,
        mousePosition.y - transform.position.y
        );

        transform.right = direction;
    }

    void calculateAngle()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (rotationZ < 0.0f)
        {
            rotationZ += 360.0f;
        }

        if (rotationZ > 90 && rotationZ < 270)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
            transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
        }
        else
        {
            player.GetComponent<SpriteRenderer>().flipX = false; 
        }
    }
}
