using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class barrilScript : MonoBehaviour
{
    public int lifes;
    public GameObject []lootBoxes;
    void Start()
    {
        lifes = 2;
    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("HIT");
        }
    }

    public void Die()
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0)
        {
            Instantiate(lootBoxes[(Random.Range(0, lootBoxes.Length))], this.transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
