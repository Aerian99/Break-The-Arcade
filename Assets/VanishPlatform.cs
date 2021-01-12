using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishPlatform : MonoBehaviour
{
    bool startDissapear = false;
    bool reloadingTime = false;
    float cd, maxCd, cdReloading, maxCdReloading;

    private void Start()
    {
        maxCdReloading = 2f;
        cdReloading = maxCdReloading;
        maxCd = 3f;
        cd = maxCd;
    }
    // Update is called once per frame
    void Update()
    {
        if(startDissapear)
        {
            cd -= Time.deltaTime;
            for (int i = 0; i < this.transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Animator>().SetBool("PlayFade", true);
                transform.GetChild(i).GetComponent<Animator>().SetBool("IsOver", false);
            }

            if(cd <= 0)
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Animator>().SetBool("PlayFade", false);
                }
                cd = maxCd;
                startDissapear = false;
                gameObject.GetComponent<EdgeCollider2D>().enabled = false;
                reloadingTime = true;
            }
        }

        if(reloadingTime)
        {
            cdReloading -= Time.deltaTime;

            if(cdReloading <= 0)
            {
                cdReloading = maxCdReloading;
                reloadingTime = false;
                gameObject.GetComponent<EdgeCollider2D>().enabled = true;
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Animator>().SetBool("IsOver", true);
                }
            }


        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            startDissapear = true;
        }
    }
}
