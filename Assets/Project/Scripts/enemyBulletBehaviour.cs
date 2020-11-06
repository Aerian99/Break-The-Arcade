using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletBehaviour : MonoBehaviour
{

    private Animator animator;
    private Transform target;
    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.tag != "AbsorbGun" && other.gameObject.tag != "Range")
        {
            Destroy(this.gameObject);
            playerBehaviour._playerLifes--;
            other.GetComponent<Animator>().SetTrigger("hit");
        }
        else if 
        (other.gameObject.tag != "Enemy" 
        && other.gameObject.tag != "PurpleBullet" 
        && other.gameObject.tag != "YellowBullet"
        && other.gameObject.tag != "RedBullet" 
        && other.gameObject.tag != "Range" 
        && other.gameObject.tag != "NPC")
        {
            Destroy(this.gameObject);
        }
    }
}
