using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoEnemyBehaviour2 : MonoBehaviour
{
    private GameObject player;
    public GameObject explosionEffect;
    
    // STATS
    public float lifes;
    private float moveSpeed;
    private float followDistance;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        lifes = 20f;
        moveSpeed = 4f;
        followDistance = 35f;
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();

        if (lifes <= 0f)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Dead();
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            SoundManagerScript.PlaySound("radialEnemyDeath");
        }
    }

    void followPlayer()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) < followDistance)
        {
            this.transform.position = Vector2.MoveTowards (this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), moveSpeed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PurpleBullet") || other.gameObject.CompareTag("RedBullet"))
        {
            GetComponent<Animator>().SetTrigger("hit");
        }
    }
}
