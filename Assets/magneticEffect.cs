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
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.transform.position = Vector3.MoveTowards(other.transform.position, this.transform.parent.position, Time.deltaTime * 10f);
            if (other.gameObject.transform.localScale.x > 0 && other.gameObject.transform.localScale.y > 0)
            {
                other.gameObject.transform.localScale -= new Vector3(1f, 1f, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Bullet Pacman" || other.gameObject.tag == "AlienAttack") && absorbCooldown.coolFull == false)
        {
            SoundManagerScript.PlaySound("absorbSound"); // Reproducimos el sonido de absorber al entrar en el trigger de absorción (magneticZone).
        }
    }
}
