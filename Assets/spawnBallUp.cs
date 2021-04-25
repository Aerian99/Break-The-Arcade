using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBallUp : MonoBehaviour
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
        if (Yellow)
        {
            if (collision.tag == "RedBullet")
            {
                Vector3 tempPosition = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
                transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBall(tempPosition, BubbleBehaviour.BallType.RED);
                Destroy(collision.gameObject);
            }
            else if (collision.tag == "PurpleBullet")
            {
                Vector3 tempPosition = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
                transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBall(tempPosition, BubbleBehaviour.BallType.PURPLE);
                Destroy(collision.gameObject);
            }
        }
        else if (Red)
        {
            if (collision.tag == "RedBullet")
            {
                int randNumber = Random.Range(0, 2);
                if (randNumber == 1)
                    transform.parent.parent.GetComponent<BubbleBehaviour>().spawnAmmo(transform.position);
                randNumber = Random.Range(0, 4);
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
                Vector3 tempPosition = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
                transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBall(tempPosition, BubbleBehaviour.BallType.PURPLE);
                Destroy(collision.gameObject);
            }
        }
        else if (Purple)
        {
            if (collision.tag == "PurpleBullet")
            {
                int randNumber = Random.Range(0, 2);
                if (randNumber == 1)
                    transform.parent.parent.GetComponent<BubbleBehaviour>().spawnAmmo(transform.position);
                randNumber = Random.Range(0, 4);
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
                Vector3 tempPosition = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
                transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBall(tempPosition, BubbleBehaviour.BallType.RED);
                Destroy(collision.gameObject);
            }
        }

    }

    public void doYesYellow()
    {
        int randNumber = Random.Range(0, 2);
        if (randNumber == 1)
            transform.parent.parent.GetComponent<BubbleBehaviour>().spawnAmmo(transform.position);
        randNumber = Random.Range(0, 4);
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
        Vector3 tempPosition = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
        transform.parent.parent.GetComponent<BubbleBehaviour>().spawnBall(tempPosition, BubbleBehaviour.BallType.YELLOW);
    }
}
