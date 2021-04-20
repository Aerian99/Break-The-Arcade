using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class purpleBulletBehaviour : MonoBehaviour {
    private GameObject effect;
    public float bulletForce = 5f;
    public ParticleSystem hitEffectPrefab;
    public GameObject hitDamagePopUp;

    private void Start()
    {
        //bulletForce += GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.damagePurpleGun;
    }
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
          && !other.gameObject.CompareTag("Bullet Pacman")
          && !other.gameObject.CompareTag("BubbleLimit")
          && !other.gameObject.CompareTag("BubbleTrigger"))
        {
            Destroy (this.gameObject);
            effect = Instantiate (hitEffectPrefab, transform.position, hitEffectPrefab.transform.localRotation).gameObject;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<radialEnemyBehaviour>())
            {
                other.gameObject.GetComponent<radialEnemyBehaviour>().lifes -= bulletForce;
            }
            else if (other.gameObject.GetComponent<burstEnemyBehaviour>())
            {
                other.gameObject.GetComponent<burstEnemyBehaviour>().lifes -= bulletForce;
            }
            else if (other.gameObject.GetComponent<demoEnemyBehaviour>())
            {
                other.gameObject.GetComponent<demoEnemyBehaviour>().lifes -= bulletForce;
            }
            else if (other.gameObject.GetComponent<demoEnemyBehaviour2>())
            {
                other.gameObject.GetComponent<demoEnemyBehaviour2>().lifes -= bulletForce;
            }
            else if (other.gameObject.GetComponent<EnemyPatrol2>())
            {
                other.gameObject.GetComponent<EnemyPatrol2>().lifes -= bulletForce;
            }
            popUpDamage(bulletForce);
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            if(other.gameObject.name == "Boss Knight")
            {
                other.gameObject.GetComponent<BossKhightBehaviour>().health -= (int)bulletForce;
                float slider = bulletForce / other.gameObject.GetComponent<BossKhightBehaviour>().maxHealth;
                other.gameObject.GetComponent<BossKhightBehaviour>().sliderHealth.transform.GetChild(2).GetComponent<Image>().fillAmount -= slider;
                popUpDamage(bulletForce);
            }
            else
            {
                other.gameObject.GetComponent<BossPhaseBehaviour>().health -= (int)bulletForce;
                float slider = bulletForce / other.gameObject.GetComponent<BossPhaseBehaviour>().maxHealth;
                other.gameObject.GetComponent<BossPhaseBehaviour>().sliderHealth.transform.GetChild(2).GetComponent<Image>().fillAmount -= slider;
                popUpDamage(bulletForce);
            }
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
        if (other.gameObject.CompareTag("AlienEnemy"))
        {
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