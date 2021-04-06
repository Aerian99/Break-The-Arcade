using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsGoToPlayer : MonoBehaviour
{
    private GameObject player;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 30;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(goToPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator goToPlayer()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        while (true)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity =  Vector3.Normalize(player.transform.position - gameObject.transform.position) * speed;
            yield return null;
        }


        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            SoundManagerScript.PlaySound("coin");
            Destroy(gameObject);
        }
    }
}
