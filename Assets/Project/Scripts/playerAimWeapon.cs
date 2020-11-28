using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAimWeapon : MonoBehaviour
{
    private Transform aimPos;
    private SpriteRenderer player;
    private Vector3 worldPosition;
    public static bool isFacingLeft;
    public static float angle;
    

    void Start()
    {
        aimPos = GameObject.Find("_aimPos").transform;
        player = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        HandleAiming();
    }
    void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimPos.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimlocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimlocalScale.y = -1f;
            player.flipX = true;
            isFacingLeft = true;
        }
        else
        {
            aimlocalScale.y = +1f;
            player.flipX = false;
            isFacingLeft = false;
        }
        aimPos.localScale = aimlocalScale;
    }

    // MOUSE WORLD POSITION FUNCTIONS
    private static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    private static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    private static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
