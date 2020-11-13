using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purpleBulletBehaviour : MonoBehaviour {
    private GameObject effect;
    public ParticleSystem hitEffectPrefab;

    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.tag != "Player" 
        && other.gameObject.tag != "EnemyBullet" 
        && other.gameObject.tag != "Range" 
        && other.gameObject.tag != "NPC")

        {
            Destroy (this.gameObject);
            effect = Instantiate (hitEffectPrefab, transform.position, hitEffectPrefab.transform.localRotation).gameObject;
        }

        Destroy (effect, 0.5f); // Eliminamos la explosión de la bala.
    }

    void OnDestroy () {
        effect = Instantiate (hitEffectPrefab, transform.position, hitEffectPrefab.transform.localRotation).gameObject;
        Destroy (effect, 0.5f);
    }
}