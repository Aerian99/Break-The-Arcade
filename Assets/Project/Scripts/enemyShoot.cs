using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    // COMPONENTES
    private Transform target;
    public GameObject enemyBullet;

    // BULLET 
    private float bulletSpeed;
    private float timeBtwShoots;
    private float startTimeBtwShoots;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        bulletSpeed = 10f;
        //startTimeBtwShoots = UnityEngine.Random.Range(0.5f, 1f);
        startTimeBtwShoots = 1f;// Rango aleatorio entre el disparo de los enemigos, así los disparos se independizan según el enemigo.
        timeBtwShoots = startTimeBtwShoots;
    }


    void Update()
    {
        RotateTowards(target.position);
        if (timeBtwShoots <= 0)
        {
            ShootPlayer();
            timeBtwShoots = startTimeBtwShoots;
        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }
    }
    private void ShootPlayer() // Función para disparar hacia la ultima dirección en el frame del jugador.
    {
        GameObject bullet = Instantiate(enemyBullet, this.transform.position, this.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
    }
    private void RotateTowards(Vector2 target) // funcion para rotar mirando al player
    {
        float offset = -90f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
