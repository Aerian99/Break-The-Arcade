using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellowBulletBehaviour : MonoBehaviour
{
    private GameObject effect;
    public GameObject hitEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player"
            && other.gameObject.tag != "EnemyBullet"
            && other.gameObject.tag != "Range"
            && other.gameObject.tag != "RedBullet"
            && other.gameObject.tag != "PurpleBullet"
            && other.gameObject.tag != "NPC"
            && other.gameObject.tag != "Platform")
        {
            Destroy(this.gameObject);
            effect = Instantiate(hitEffect, transform.position, hitEffect.transform.localRotation);
        }

        Destroy(effect, 0.3f); // Eliminamos la explosión de la bala.
    }

    void OnDestroy()
    {
        effect = Instantiate(hitEffect, transform.position, hitEffect.transform.localRotation);
        Destroy(effect, 0.3f);
    }
}