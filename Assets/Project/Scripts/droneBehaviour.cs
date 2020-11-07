using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class droneBehaviour : MonoBehaviour
{
    private Transform playerCharacter;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    public TextMeshProUGUI lifes;
    private int _droneLifes;

    public void Awake()
    {
        playerCharacter = GameObject.FindWithTag("Player").transform;
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        _droneLifes = 10;
    }

    public void Update()
    {
        this.spriteRenderer.flipX = playerCharacter.transform.position.x > this.transform.position.x;
        if (_droneLifes <= 0)
        {
            anim.SetBool("dead", true);
            Destroy(this.gameObject, 0.1f);
        }
        if (_droneLifes >= 0)
        {
            lifes.text = "" + _droneLifes;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PurpleBullet")
        {
            anim.SetTrigger("hit");
            _droneLifes -= 2;
        }
        else if (other.gameObject.tag == "YellowBullet")
        {
            anim.SetTrigger("hit");
            _droneLifes -= 1;
        }
        else if (other.gameObject.tag == "RedBullet")
        {
            anim.SetTrigger("hit");
            _droneLifes -= 5;
        }
    }
}
