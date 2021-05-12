using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class barrilScript : MonoBehaviour
{
    public bool luckUp;
    public int lifes;
    public GameObject []lootBoxes;
    public GameObject gameController;
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("gameController");
        lifes = 2;
        luckUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController.GetComponent<GameController>().playerCaracteristics.isLuckUp)
        {
            luckUp = true;
        }
        if (lifes <= 0)
        {
            this.GetComponent<CapsuleCollider2D>().enabled = false;
            this.GetComponent<Animator>().SetBool("destroy", true);
        }
        
        if (handController.currentPos == 1 && Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            this.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PurpleBullet") || other.gameObject.CompareTag("RedBullet"))
        {
            this.GetComponent<Animator>().SetTrigger("hit");
            lifes--;
        }
    }

    public void Die()
    {
        if(!luckUp)
        { 
            int randomNumber = Random.Range(0, 4);
            if (randomNumber != 0)
            {
                if (lootBoxes.Length > 0)
                {
                    int randomLoot = Random.Range(0, 4);
                    if (randomLoot != 0)
                      Instantiate(lootBoxes[0], this.transform.position, Quaternion.identity);
                    else
                      Instantiate(lootBoxes[1], this.transform.position, Quaternion.identity);
                }
            }
            Destroy(this.gameObject);
        }
        else
        {
            if (lootBoxes.Length > 0)
            { 
                Instantiate(lootBoxes[(Random.Range(0, lootBoxes.Length))], this.transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
}
