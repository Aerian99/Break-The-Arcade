using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxScriptBoss : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(enableBox());
    }

    IEnumerator enableBox()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManagerScript.PlaySound("dropSound");
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
            Destroy(this.gameObject);

            if (handController.currentPos == 0)
            {
                player.GetComponent<playerBehaviour>().reservedAmmoPurple += 15;
            }

            if (handController.currentPos == 1)
            {
                player.GetComponent<playerBehaviour>().reservedAmmoYellow += 10;
            }

            if (handController.currentPos == 2)
            {
                player.GetComponent<playerBehaviour>().reservedAmmoShotgun += 3;
            }
        }
    }
}
