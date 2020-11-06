using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb_Gun : MonoBehaviour
{
    private EdgeCollider2D arcCollider;
    public GameObject arcEffect;
    private Transform arcTransform;
    private GameObject effect;

    void Start()
    {
        arcCollider = GetComponent<EdgeCollider2D>();
        arcTransform = transform.GetChild(0).transform;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyBullet" && absorbCooldown.coolFull == false) 
        {
            playerBehaviour._bulletCounter++;
            Destroy(other.gameObject);
        }
    }
}
