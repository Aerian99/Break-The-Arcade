using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBox : MonoBehaviour
{
    private GameObject player;
    private int healRatio;
    private void Start()
    {
        healRatio = 1 + GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.healPowerUp;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManagerScript.PlaySound("dropSound");
            if (player.GetComponent<playerBehaviour>()._playerLifes < 6)
            {
                if (GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.lifes + healRatio >= 5)
                    GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.lifes = 5;
                else
                    GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.lifes += healRatio;

            }
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
            Destroy(this.gameObject);
        }
    }
}
