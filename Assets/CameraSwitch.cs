using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    bool nextRoom = false;
    public Vector3 posFocoR, posFocoG, posFocoB;
    public Vector3 newPos;
    public GameObject focoR, focoG, focoB;
    public GameObject fadeImage;
    Vector3 firstPosR, firstPosG, firstPosB;
    float t;
    public bool treasureRoom, exitTresure;

    bool startCountingFade;
    float cd, maxCd;

    private void Start()
    {
        t = 1f;
        cd = 0f;
        maxCd = 1.1f;
        startCountingFade = false;
    }

    private void Update()
    {
        if (startCountingFade)
        {
            cd += Time.deltaTime;
            if (cd >= maxCd)
            { 
                fadeImage.GetComponent<Animator>().SetBool("fadeIn", false);
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.gameObject.GetComponent<playerMovement>().enabled = true;
                cd = 0;
                startCountingFade = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fadeImage.GetComponent<movePlayer>().newPos = newPos;
            fadeImage.GetComponent<Animator>().SetBool("fadeIn", true);
            startCountingFade = true;
            collision.gameObject.GetComponent<playerMovement>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            if (treasureRoom)
            {
                LEDController.isTreasureRoom = true;
            }
            if(exitTresure)
                LEDController.isTreasureRoom = false;

            firstPosR = focoR.transform.position;
            firstPosG = focoG.transform.position;
            firstPosB = focoB.transform.position;
            focoR.transform.position = Vector3.Lerp(firstPosR, posFocoR, t);
            focoG.transform.position = Vector3.Lerp(firstPosG, posFocoG, t);
            focoB.transform.position = Vector3.Lerp(firstPosB, posFocoB, t);



            /*if (!nextRoom)
            {
                firstPosR = focoR.transform.position;
                firstPosG = focoG.transform.position;
                firstPosB = focoB.transform.position;           
                focoR.transform.position = Vector3.Lerp(firstPosR, posFocoR, t);
                focoG.transform.position = Vector3.Lerp(firstPosG, posFocoG, t);
                focoB.transform.position = Vector3.Lerp(firstPosB, posFocoB, t);
                nextRoom = true;
            }
            else
            { 
                focoR.transform.position = Vector3.Lerp(posFocoR, firstPosR, t);
                focoG.transform.position = Vector3.Lerp(posFocoG, firstPosG, t);
                focoB.transform.position = Vector3.Lerp(posFocoB, firstPosB, t);
                nextRoom = false;
                LEDController.isTreasureRoom = false;
            }*/

        }


    }
}
