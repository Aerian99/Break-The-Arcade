using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkNPC : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform playerCharacter;
    private Animator anim1;
    private Animator anim2;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCharacter = GameObject.FindWithTag("Player").transform;
        anim1 = GetComponent<Animator>();
        anim2 = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        this.spriteRenderer.flipX = playerCharacter.transform.position.x < this.transform.position.x;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim1.SetBool("onZone", true);
            anim2.SetBool("isIN", true);
            anim2.SetBool("isOut", false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim1.SetBool("onZone", false);
            anim2.SetBool("isOut", true);
            anim2.SetBool("isIN", false);
        }
    }
}
