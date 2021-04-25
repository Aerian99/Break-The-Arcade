using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    public Vector3 groundReference;
    public GameObject ammoPrefab;

    public GameObject[] enemiesGround;
    public GameObject[] flyingEnemies;
    public GameObject[] coinSpawner;

    public GameObject[] bubbleBullets;
    public enum BallType { RED, YELLOW, PURPLE};
    public GameObject redBall, yellowBall, purpleBall, YouWinText;
    public Vector3 startPosition;
    private int rows, columns;

    float cdWaitSpawn = 3f;
    // Start is called before the first frame update
    void Start()
    {
        columns = 7;
        rows = 3;
        //GENERATE BALLS
        StartCoroutine(SpawnBalls());
    }

    // Update is called once per frame
    void Update()
    {
        if(cdWaitSpawn <= 0)
        {
            if(gameObject.transform.childCount <= 0)
            {
                CheckRemainingBullets();
                for (int i = 0; i < coinSpawner.Length; i++)
                    coinSpawner[i].GetComponent<CoinWinBoss>().coinSpawner = true;
                YouWinText.GetComponent<Animator>().SetBool("bossDead", true);
                StartCoroutine(EndGame());

            }
        }
        else
        {
            cdWaitSpawn -= Time.deltaTime;
        }
    }

    IEnumerator SpawnBalls()
    {
        float initX = startPosition.x;
        GameObject ball;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                BallType rand = (BallType)Random.Range(0, 3);
                switch (rand)
                {
                    case BallType.RED:
                        ball = Instantiate(redBall, startPosition, Quaternion.identity);
                        ball.transform.parent = gameObject.transform;
                        startPosition.x += 3f;
                        break;
                    case BallType.YELLOW:
                        ball = Instantiate(yellowBall, startPosition, Quaternion.identity);
                        ball.transform.parent = gameObject.transform;
                        startPosition.x += 3f;
                        break;
                    case BallType.PURPLE:
                        ball = Instantiate(purpleBall, startPosition, Quaternion.identity);
                        ball.transform.parent = gameObject.transform;
                        startPosition.x += 3f;
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(0.1f);
            }
            startPosition.x = initX;
            startPosition.y -= 3f;
        }

        yield return null;
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(4.0f);
        GameObject.Find("-----SCENEMANAGEMENT").GetComponent<PlaySceneManager>().finishedGame = true;
    }
    public void spawnBall(Vector3 position, BallType type)
    {
        GameObject ball;
        switch (type)
        {
            case BallType.RED:
                ball = Instantiate(redBall, position, Quaternion.identity);
                ball.transform.parent = gameObject.transform;
                break;
            case BallType.YELLOW:
                ball = Instantiate(yellowBall, position, Quaternion.identity);
                ball.transform.parent = gameObject.transform;
                break;
            case BallType.PURPLE:
                ball = Instantiate(purpleBall, position, Quaternion.identity);
                ball.transform.parent = gameObject.transform;
                break;
            default:
                break;
        }
       
        
    }

    public void spawnEnemy(Vector3 position)
    {
        SoundManagerScript.PlaySound("pop");
        int randNumber = Random.Range(0, 2);

        if(randNumber == 0)
        {
            Instantiate(enemiesGround[Random.Range(0, enemiesGround.Length)], groundReference, Quaternion.identity);
        }
        else
        {
            float positionYToSpawn = Random.Range(groundReference.y + 5, position.y);
            Instantiate(flyingEnemies[Random.Range(0, flyingEnemies.Length)], new Vector3(position.x, positionYToSpawn, position.z), Quaternion.identity);
        }
    }

    public void spawnBullets(Vector3 position)
    {
        SoundManagerScript.PlaySound("pop");
        Instantiate(bubbleBullets[Random.Range(0, bubbleBullets.Length)], position, Quaternion.identity);
    }

    public void spawnAmmo(Vector3 position)
    {
        Instantiate(ammoPrefab, position, Quaternion.identity);
    }

    private void CheckRemainingBullets()
    {
        if (GameObject.Find("demoBullet(Clone)"))
        {
            Destroy(GameObject.Find("demoBullet(Clone)"));
        }
    }
}
