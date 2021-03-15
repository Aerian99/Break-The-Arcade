using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosiveBarrel : MonoBehaviour
{
    public int lifes;
    public GameObject deathExplosion;
    private float explosionRange;
    public LayerMask playerLayer, enemyLayer, barrelNLayer, barrelELayer;
    private bool explosionDamagedPlayer;
    Collider2D[] explosionBarrelNormal, explosionBarrelE, explosionDamagedEnemy;


    void Start()
    {
        lifes = 2;
        explosionRange = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifes <= 0)
        {
            explosionDamagedPlayer = Physics2D.OverlapCircle(this.transform.position, explosionRange, playerLayer);
            if (explosionDamagedPlayer && !playerBehaviour.activeImmunity)
            {
                playerBehaviour.activeImmunity = true;
            }
            explosionDamagedEnemy = Physics2D.OverlapCircleAll(this.transform.position, explosionRange, enemyLayer);
            for (int i = 0; i < explosionDamagedEnemy.Length; i++)
            {
                explosionDamagedEnemy[i].gameObject.GetComponent<enemyPatrol>().lifes = 0;
            }

            explosionBarrelNormal = Physics2D.OverlapCircleAll(this.transform.position, explosionRange, barrelNLayer);
            for (int i = 0; i < explosionBarrelNormal.Length; i++)
            {
                explosionBarrelNormal[i].gameObject.GetComponent<barrilScript>().lifes = 0;
            }

            CheckExplosion();

            /*this.GetComponent<CapsuleCollider2D>().enabled = false;
            this.GetComponent<Animator>().SetBool("destroy", true);*/
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
        Destroy(this.gameObject);
        GameObject explosionGO = Instantiate(deathExplosion, transform.position, Quaternion.identity);
        Destroy(explosionGO, 0.7f);
        SoundManagerScript.PlaySound("alienExplosion");
    }

    public void CheckExplosion()
    {
        explosionBarrelE = Physics2D.OverlapCircleAll(this.transform.position, explosionRange, barrelELayer);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        for (int i = 0; i < explosionBarrelE.Length; i++)
        {
            explosionBarrelE[i].gameObject.GetComponent<explosiveBarrel>().CheckExplosion();
        }
        Die();
    }

    void ExplodeOtherBarrels()
    { 
    
    
    }
}
