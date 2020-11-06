using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class playerAimWeapon : MonoBehaviour
{
    private Transform aimPos;
    private SpriteRenderer player;
    private Vector3 worldPosition;

    void Start()
    {
        aimPos = GameObject.Find("_aimPos").transform;
        player = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleAiming();
        if (Input.GetMouseButtonDown(0) && playerBehaviour._bulletCounter > 0)
        {
            ShakeCamera(.03f, .1f);
        }
    }
    void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimPos.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimlocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimlocalScale.y = -1f;
            player.flipX = true;
        }
        else
        {
            aimlocalScale.y = +1f;
            player.flipX = false;
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

    // CAMERA SHAKE FUNCTION
    public static void ShakeCamera(float intensidad, float duracion)
    {
        Vector3 lastCameraMovement = Vector3.zero;
        FunctionUpdater.Create(delegate ()
        {
            duracion -= Time.unscaledDeltaTime;
            Vector3 randomMovement = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * intensidad;
            Camera.main.transform.position = Camera.main.transform.position - lastCameraMovement + randomMovement;
            lastCameraMovement = randomMovement;
            return duracion <= 0f;
        }, "CAMERA_SHAKE");
    }
}
