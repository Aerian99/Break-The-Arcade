using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redBulletBehaviour : MonoBehaviour
{
    private GameObject effect;
    public ParticleSystem hitEffectPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.gameObject.CompareTag("Player")
            && !other.gameObject.CompareTag("EnemyBullet")
            && !other.gameObject.CompareTag("Range")
            && !other.gameObject.CompareTag("RedBullet")
            && !other.gameObject.CompareTag("NPC")
            && !other.gameObject.CompareTag("greyPlatform")
            && !other.gameObject.CompareTag("Triggers")
            && !other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
            effect = Instantiate(hitEffectPrefab, transform.position, hitEffectPrefab.transform.localRotation).gameObject;
        } 
        Destroy(effect, 0.5f); // Eliminamos la explosión de la bala.
    }
    
    void OnDestroy()
    {
        effect = Instantiate(hitEffectPrefab, transform.position, hitEffectPrefab.transform.localRotation).gameObject;
        Destroy(effect, 0.5f);
    }
}

