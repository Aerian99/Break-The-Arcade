using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimationEvent : MonoBehaviour
{
    public GameObject player, hand, purpleGun, redGun, laserGun, absorbGun, runDust1, runDust2;
    private Transform instantiatePoint;
    private GameObject playerImage;
    void Start()
    {
        playerImage = transform.GetChild(1).gameObject;
        instantiatePoint = transform.GetChild(1).transform;
        player.GetComponent<SpriteRenderer>().enabled = false;
        hand.GetComponent<SpriteRenderer>().enabled = false;
        purpleGun.GetComponent<SpriteRenderer>().enabled = false;
        redGun.GetComponent<SpriteRenderer>().enabled = false;
        laserGun.GetComponent<SpriteRenderer>().enabled = false;
        absorbGun.GetComponent<SpriteRenderer>().enabled = false;
        runDust1.gameObject.SetActive(false);
        runDust2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeOutEnd()
    {
        this.GetComponent<Animator>().SetTrigger("appearPlayer");
    }

    public void instantiatePlayer()
    {
        Destroy(playerImage);
        player.transform.position = instantiatePoint.transform.position;
        player.GetComponent<SpriteRenderer>().enabled = true;
        hand.GetComponent<SpriteRenderer>().enabled = true;
        purpleGun.GetComponent<SpriteRenderer>().enabled = true;
        redGun.GetComponent<SpriteRenderer>().enabled = true;
        laserGun.GetComponent<SpriteRenderer>().enabled = true;
        absorbGun.GetComponent<SpriteRenderer>().enabled = true;
        runDust1.gameObject.SetActive(true);
        runDust2.gameObject.SetActive(true);
    }
}
