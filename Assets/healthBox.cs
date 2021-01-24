using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManagerScript.PlaySound("dropSound");
            playerBehaviour._playerLifes += 1;
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
            Destroy(this.gameObject);
        }
    }
}
