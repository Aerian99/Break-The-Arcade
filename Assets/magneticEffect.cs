using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magneticEffect : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Bullet Pacman" || other.gameObject.tag == "AlienAttack") && absorbCooldown.coolFull == false)
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, this.transform.parent.position, Time.deltaTime * 10f);
        }
    }
}
