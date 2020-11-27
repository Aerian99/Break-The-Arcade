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
            if (handController.currentPos == 0 && playerBehaviour._bulletCounterPurple != playerBehaviour.MAX_BULLETS_PURPLE)
            { playerBehaviour._bulletCounterPurple++; }
            else if (handController.currentPos == 1 && playerBehaviour._bulletCounterYellow != playerBehaviour.MAX_BULLETS_YELLOW)
            { playerBehaviour._bulletCounterYellow++; }
            else if (handController.currentPos == 2 && playerBehaviour._bulletCounterShotgun != playerBehaviour.MAX_BULLETS_SHOTGUN)
            { playerBehaviour._bulletCounterShotgun++; }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Bullet Pacman")
        {
            droneBehaviour.canBeAttacked = true;        
        }

    }
}
