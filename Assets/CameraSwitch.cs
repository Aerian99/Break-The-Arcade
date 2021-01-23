using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    bool nextRoom = false;
    public Vector3 newPosition, oldPosition;
    public Vector3 posFocoR, posFocoG, posFocoB;
    public GameObject focoR, focoG, focoB;
    Vector3 firstPosR, firstPosG, firstPosB;
    float t;
    public bool treasureRoom;

    private void Start()
    {
        t = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (treasureRoom)
            {
                LEDController.isTreasureRoom = true;
            }
            if (!nextRoom)
            {
                firstPosR = focoR.transform.position;
                firstPosG = focoG.transform.position;
                firstPosB = focoB.transform.position;
                CameraManagment.changeCameras(newPosition);
                focoR.transform.position = Vector3.Lerp(firstPosR, posFocoR, t);
                focoG.transform.position = Vector3.Lerp(firstPosG, posFocoG, t);
                focoB.transform.position = Vector3.Lerp(firstPosB, posFocoB, t);
                nextRoom = true;
            }
            else
            { 
                CameraManagment.changeCameras(oldPosition);
                focoR.transform.position = Vector3.Lerp(posFocoR, firstPosR, t);
                focoG.transform.position = Vector3.Lerp(posFocoG, firstPosG, t);
                focoB.transform.position = Vector3.Lerp(posFocoB, firstPosB, t);
                nextRoom = false;
                LEDController.isTreasureRoom = false;
            }
        }

    }
}
