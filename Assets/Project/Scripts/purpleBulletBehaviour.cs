using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class purpleBulletBehaviour : MonoBehaviour {
    private GameObject effect;
    private float bulletForce = 5f;
    public ParticleSystem hitEffectPrefab;
    public GameObject hitDamagePopUp;

    void OnTriggerEnter2D (Collider2D other) {

        if (!other.gameObject.CompareTag("Player")
          && !other.gameObject.CompareTag("EnemyBullet")
          && !other.gameObject.CompareTag("Range")
          && !other.gameObject.CompareTag("PurpleBullet")
          && !other.gameObject.CompareTag("greyPlatform")
          && !other.gameObject.CompareTag("Triggers")
          && !other.gameObject.CompareTag("Wall") 
          && !other.gameObject.CompareTag("AlienWall") 
          && !other.gameObject.CompareTag("AlienAttack")
          && !other.gameObject.CompareTag("Bullet Pacman"))
        {
            Destroy (this.gameObject);
            effect = Instantiate (hitEffectPrefab, transform.position, hitEffectPrefab.transform.localRotation).gameObject;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<radialEnemyBehaviour>().lifes -= bulletForce;
            popUpDamage(bulletForce);
        }
        if (other.gameObject.CompareTag("Tower"))
        {
            other.gameObject.GetComponent<TowerBehaviour>().lifes -= bulletForce;
            popUpDamage(bulletForce);
        }
        if (other.gameObject.CompareTag("RobotPatrol"))
        {
            other.gameObject.GetComponent<enemyPatrol>().lifes -= bulletForce;
            popUpDamage(bulletForce);
        }

        Destroy (effect, 0.5f); // Eliminamos la explosión de la bala.
    }

    void OnDestroy () {
        effect = Instantiate (hitEffectPrefab, transform.position, hitEffectPrefab.transform.localRotation).gameObject;
        Destroy (effect, 0.5f);
    }
    
    void popUpDamage(float hitdamage)
    {
        GameObject dmg = Instantiate(hitDamagePopUp, transform.position, Quaternion.identity);
        dmg.GetComponent<TextMeshPro>().text = "-" + hitdamage;
    }
}