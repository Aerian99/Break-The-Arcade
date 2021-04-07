using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public float lifes;
    [HideInInspector] public bool isDying;
    float fade;
    public Material mat;
    public GameObject explosionEffect;

    void Start()
    {
        lifes = 20f;
        fade = 1;
        isDying = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (lifes <= 0f)
        {
            GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().throwCoins("turret", this.gameObject);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<Collider2D>().enabled = false;
            //Dead();
            SoundManagerScript.PlaySound("radialEnemyDeath");
            Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //lifes -= 10f;
        }
    }
}
