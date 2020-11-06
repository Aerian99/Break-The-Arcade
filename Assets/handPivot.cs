using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handPivot : MonoBehaviour
{
    public GameObject myPlayer;
    public SpriteRenderer playerSprite;
    Vector3 v3Pos;
    float fAngle;

    void Update()
    {
        calculateAngle();
    }

    private void FixedUpdate()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (rotationZ < -90 || rotationZ > 90)
        {
            if (myPlayer.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
            }
            else if (myPlayer.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);

            }
        }
    }
    void calculateAngle()
    {

        v3Pos = Input.mousePosition;
        v3Pos.z = (myPlayer.transform.position.z - Camera.main.transform.position.z);
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
        v3Pos = v3Pos - myPlayer.transform.position;
        fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;

        if (fAngle < 0.0f)
        {
            fAngle += 360.0f;
        }

        if (fAngle > 90 && fAngle < 270)
        {
            playerSprite.flipX = false;
            
        }
        else
        {
            playerSprite.flipX = true;
        }
    }

}
