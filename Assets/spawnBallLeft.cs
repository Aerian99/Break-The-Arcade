using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBallLeft : MonoBehaviour
{
    public bool Yellow, Red, Purple;
    GameObject parentObject;
    // Start is called before the first frame update
    void Start()
    {
        parentObject = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Yellow)
        { 
            if (collision.tag == "RedBullet")
            {
                Vector3 tempPosition = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
                transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBall(tempPosition, BubbleBehaviour.BallType.RED);

                Destroy(collision.gameObject);
            }
            else if (collision.tag == "PurpleBullet")
            {
                Vector3 tempPosition = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
                transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBall(tempPosition, BubbleBehaviour.BallType.PURPLE);

                Destroy(collision.gameObject);
            }
        }
        else if (Red)
        {
            if (collision.tag == "RedBullet")
            {
                //DO BULLETS OR SPAWN MONSTERS
                transform.parent.parent.GetComponent<BubbleBehaviour>().spawnAmmo(transform.position);
                int randNumber = Random.Range(0, 4);
                if (randNumber == 0)
                {
                    transform.parent.parent.GetComponent<BubbleBehaviour>().spawnEnemy(transform.position);
                }
                else
                {
                    transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBullets(transform.position);
                }

                Destroy(collision.gameObject);
                Destroy(parentObject);
            }
            else if (collision.tag == "PurpleBullet")
            {
                Vector3 tempPosition = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
                transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBall(tempPosition, BubbleBehaviour.BallType.PURPLE);
                Destroy(collision.gameObject);
            }
        }
        else if (Purple)
        {
            if (collision.tag == "PurpleBullet")
            {
                //DO BULLETS OR SPAWN MONSTERS
                transform.parent.parent.GetComponent<BubbleBehaviour>().spawnAmmo(transform.position);
                int randNumber = Random.Range(0, 4);
                if (randNumber == 0)
                {
                    transform.parent.parent.GetComponent<BubbleBehaviour>().spawnEnemy(transform.position);
                }
                else
                {
                    transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBullets(transform.position);
                }
                Destroy(collision.gameObject);
                Destroy(parentObject);
            }
            else if (collision.tag == "RedBullet")
            {
                Vector3 tempPosition = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
                transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBall(tempPosition, BubbleBehaviour.BallType.RED);
                Destroy(collision.gameObject);
            }
        }

    }

    public void doYesYellow()
    {
        //DO BULLETS OR SPAWN MONSTERS
        transform.parent.parent.GetComponent<BubbleBehaviour>().spawnAmmo(transform.position);
        int randNumber = Random.Range(0, 4);
        if (randNumber == 0)
        {
            transform.parent.parent.GetComponent<BubbleBehaviour>().spawnEnemy(transform.position);
        }
        else
        {
            transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBullets(transform.position);
        }

        Destroy(parentObject);
    }

    public void doNoYellow()
    {
        Vector3 tempPosition = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
        transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBall(tempPosition, BubbleBehaviour.BallType.YELLOW);
    }
}
