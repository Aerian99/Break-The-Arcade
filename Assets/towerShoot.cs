using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerShoot : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject canvasGO;

    public GameObject bulletPrefab;
    private float FireRate = 2.5f;
    private float NextTimeToFire = 2f;
    private float shootForce = 10f;

    private GameObject player;

    private float lifes;
    [HideInInspector] public bool isDying;
    float fade;
    public Material mat;

    GameObject bulletGO;

    public Transform firePoint;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        lifes = 50f;
        fade = 1;
        isDying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().activatedAbsorb)
        {
            if (Time.time > NextTimeToFire)
            {
                Shoot();
            }

        }

        if (lifes < 0f)
        {
            Dead();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            lifes -= 10f;
        }
    }

    void Shoot()
    {
        bulletGO = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        bulletGO.GetComponent<Rigidbody2D>().AddForce(transform.up * shootForce, ForceMode2D.Impulse);
        NextTimeToFire = Time.time + FireRate;
        Debug.Log("HELLOW");
    }

    void Dead()
    {
        mat.SetColor("_Color", new Color(0.1294118f, 0.5921569f, 0.8039216f));
        this.GetComponent<SpriteRenderer>().material = mat;
        isDying = true;
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        gameObject.GetComponent<Collider2D>().enabled = false;
        fade -= Time.deltaTime;
        mat.SetFloat("_Fade", fade);

        if (fade <= 0)
        {
            Destroy(gameObject);
        }
    }
}

