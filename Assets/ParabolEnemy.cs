using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolEnemy : MonoBehaviour
{

    public GameObject bullet;
    enemyPatrol parent;
    public float launchForce;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<enemyPatrol>();
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Vector3 vec;
            GameObject bulletInstantiate = Instantiate(bullet, transform.position, transform.rotation);
            Transform positionTarget = GameObject.FindGameObjectWithTag("Player").transform;
            if(parent.getMovingRight())
                bulletInstantiate.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 10);
            else
                bulletInstantiate.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 10);


            //bulletInstantiate.GetComponent<Rigidbody2D>().AddForce(vec * launchForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(3);
        }

    }
}
