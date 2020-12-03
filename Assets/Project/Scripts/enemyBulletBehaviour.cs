using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletBehaviour : MonoBehaviour
{
    private float cdExplote, cdMaxExplote;
    private bool canExplote, explosionDamaged;
    [HideInInspector]public bool exploted;
    private float explosionRange;
    public LayerMask layer, PlatformLayer;
    public GameObject deathExplosion;
    private Animator animator;

    private void Start()
    {
        cdMaxExplote = 1f;
        cdExplote = cdMaxExplote;
        explosionRange = 1f;
        canExplote = exploted = false;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (canExplote)
        { 
            cdExplote -= Time.fixedDeltaTime;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            animator.SetTrigger("activeExplosion");
        }

        if (cdExplote <= 0)
        {
            Collider2D[] platforms = Physics2D.OverlapCircleAll(transform.position, explosionRange, PlatformLayer);
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].GetComponent<ProtectionBarrierAliens>().hitted = true;
            }
            explosionDamaged = Physics2D.OverlapCircle(this.transform.position, explosionRange, layer);
            if (explosionDamaged && !playerBehaviour.activeImmunity)
            {
                playerBehaviour.activeImmunity = true;
            }
            Destroy(this.gameObject);
            GameObject explosionGO = Instantiate(deathExplosion, transform.position, Quaternion.identity);
            Destroy(explosionGO, 0.7f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.tag != "AbsorbGun" && other.gameObject.tag != "Range")
        {
            if (playerBehaviour.canBeDamaged)
            {
                playerBehaviour.activeImmunity = true;
                other.GetComponent<Animator>().SetTrigger("hit");
            }
            Destroy(this.gameObject);
        }
        else if
        (other.gameObject.tag != "Enemy"
        && other.gameObject.tag != "PurpleBullet"
        && other.gameObject.tag != "YellowBullet"
        && other.gameObject.tag != "RedBullet"
        && other.gameObject.tag != "EnemyBullet"
        && other.gameObject.tag != "Bullet Pacman"
        && other.gameObject.tag != "Range"
        && other.gameObject.tag != "CyanEnemy"
        && other.gameObject.tag != "OrangeEnemy"
        && other.gameObject.tag != "RedEnemy"
        && other.gameObject.tag != "NPC" 
        && other.gameObject.tag != "greyPlatform"
        && other.gameObject.tag != "Triggers")
        {
            Destroy(this.gameObject);
        } 
        else if (other.gameObject.tag == "AlienWall")
        {
            Debug.Log("Hellow");
            other.GetComponent<ProtectionBarrierAliens>().hitted = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canExplote = true;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
