using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    bool startDissapear, reloadingTime, alreadyCalculating;
    float cd, maxCd, cdReloading, maxCdReloading;

    // Start is called before the first frame update
    void Start()
    {
        startDissapear = reloadingTime = alreadyCalculating = false;
        maxCdReloading = 2f;
        cdReloading = maxCdReloading;
        maxCd = 3f;
        cd = maxCd;
    }

    // Update is called once per frame
    void Update()
    {
        if (startDissapear)
        {
            cd -= Time.deltaTime;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<Animator>().SetBool("RedPlatform", true);
                gameObject.transform.GetChild(i).GetComponent<Animator>().SetBool("PlayFade", true);
                gameObject.transform.GetChild(i).GetComponent<Animator>().SetBool("IsOver", false);
            }

            if (cd <= 0)
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    gameObject.transform.GetChild(i).GetComponent<Animator>().SetBool("PlayFade", false);
                }
                cd = maxCd;
                startDissapear = false;
                gameObject.GetComponent<EdgeCollider2D>().enabled = false;
                reloadingTime = true;
            }
        }

        if (reloadingTime)
        {
            cdReloading -= Time.deltaTime;
           
            if (cdReloading <= 0)
            {
                cdReloading = maxCdReloading;
                reloadingTime = false;

                gameObject.GetComponent<EdgeCollider2D>().enabled = true;
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    gameObject.transform.GetChild(i).GetComponent<Animator>().SetBool("IsOver", true);
                }
            }

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startDissapear = true;
        }
    }
}
