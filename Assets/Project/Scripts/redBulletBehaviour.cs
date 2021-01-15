using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class redBulletBehaviour : MonoBehaviour
{
    private GameObject effect;
    private float bulletForce = 10f;
    public ParticleSystem hitEffectPrefab;
    public GameObject hitDamagePopUp;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.gameObject.CompareTag("Player")
            && !other.gameObject.CompareTag("EnemyBullet")
            && !other.gameObject.CompareTag("Range")
            && !other.gameObject.CompareTag("RedBullet")
            && !other.gameObject.CompareTag("greyPlatform")
            && !other.gameObject.CompareTag("Triggers")
            && !other.gameObject.CompareTag("Wall") 
            && !other.gameObject.CompareTag("AlienWall") 
            && !other.gameObject.CompareTag("AlienAttack")
            && !other.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            effect = Instantiate(hitEffectPrefab, transform.position, hitEffectPrefab.transform.localRotation).gameObject;
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<radialEnemyBehaviour>().lifes -= bulletForce;
            popUpDamage(bulletForce);
        }
        if (other.gameObject.CompareTag("RobotPatrol"))
        {
            other.gameObject.GetComponent<enemyPatrol>().lifes -= bulletForce;
            popUpDamage(bulletForce);
        }
        
        Destroy(effect, 0.5f); // Eliminamos la explosión de la bala.
    }
    
    void OnDestroy()
    {
        effect = Instantiate(hitEffectPrefab, transform.position, hitEffectPrefab.transform.localRotation).gameObject;
        Destroy(effect, 0.5f);
    }
    void popUpDamage(float hitdamage)
    {
        GameObject dmg = Instantiate(hitDamagePopUp, transform.position, Quaternion.identity);
        dmg.GetComponent<TextMeshPro>().text = "-" + hitdamage;
    }
}

