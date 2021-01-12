using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishPlatform : MonoBehaviour
{
    bool startDissapear = false;
    bool reloadingTime = false;
    float cd, maxCd, cdReloading, maxCdReloading;
    public GameObject[] activePlatforms;

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
        if(startDissapear && !GameObject.FindGameObjectWithTag("PlatformController").GetComponent<PlatformController>().alreadyCalculating)
        {
            GameObject.FindGameObjectWithTag("PlatformController").GetComponent<PlatformController>().controlPlatform(activePlatforms);
        }
        else if(GameObject.FindGameObjectWithTag("PlatformController").GetComponent<PlatformController>().alreadyCalculating)
        {
            startDissapear = false;
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
