using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public float speed, playerRange, playerApartRange;
    public LayerMask playerLayer;

    private bool inRange, maxDistance;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer); //Dibuja un circulo para ver la distancia y pone el bool en true si esta en esa distancia
        maxDistance = Physics2D.OverlapCircle(transform.position, playerApartRange, playerLayer);
        if (inRange && !maxDistance)
        { 
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerApartRange);
    }
}
