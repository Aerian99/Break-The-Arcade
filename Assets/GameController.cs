using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject m_coins;
    float cdAbsorb, maxcdAbsorb;
    [HideInInspector]public bool activatedAbsorb;
    GameObject[] robotPatrols, radialEnemies;
    // Start is called before the first frame update
    void Start()
    {
        BeginGame();
        maxcdAbsorb = 2.5f;
        cdAbsorb = maxcdAbsorb;
        activatedAbsorb = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activatedAbsorb)
        {
            radialEnemies = GameObject.FindGameObjectsWithTag("AnimationLight");
            robotPatrols = GameObject.FindGameObjectsWithTag("RobotPatrol");
            for (int i = 0; i < robotPatrols.Length; i++)
            {
                robotPatrols[i].GetComponent<enemyPatrol>().patrolSpeed = 6f;
                robotPatrols[i].GetComponent<Animator>().SetBool("absorbed", true);
               
            }
            for (int i = 0; i < radialEnemies.Length; i++)
            {
                radialEnemies[i].GetComponent<Animator>().SetBool("disabled", true);
            }
            if (cdAbsorb <= 0)
            {
                cdAbsorb = maxcdAbsorb;
                activatedAbsorb = false;
                deactivateEnemyStates();
            }
           
            cdAbsorb -= Time.deltaTime;
        }

        
    }

    public void BeginGame()
    {
       gameObject.GetComponent<TimerController>().BeginTimer();
    }
    void deactivateEnemyStates()
    {
        radialEnemies = GameObject.FindGameObjectsWithTag("AnimationLight");
        robotPatrols = GameObject.FindGameObjectsWithTag("RobotPatrol");
        for (int i = 0; i < robotPatrols.Length; i++)
        {
            
            robotPatrols[i].GetComponent<Animator>().SetBool("absorbed", false);
            robotPatrols[i].GetComponent<enemyPatrol>().patrolSpeed = 2f;
        }

        for (int i = 0; i < radialEnemies.Length; i++)
        {
            radialEnemies[i].GetComponent<Animator>().SetBool("disabled", false);
        }

    }

    public void ActivateEnemyStates()
    {
        activatedAbsorb = true;
    }
    public float RandomFloat(float min, float max)
    {
        return Random.Range(min, max);
    }

    public void throwCoins(string m_name, GameObject m_enemy)
    {
        switch (m_name)
        {
            case "gumRobot":
                for (int i = 0; i < 2; i++)
                {
                    Vector2 velocity = new Vector2(Random.Range(-3, 3), 7);
                    float angle;
                    angle = 20 * Mathf.Deg2Rad;

                    //z  uniformly on [cos0,1]
                    float Z = RandomFloat(Mathf.Cos(angle), 1);

                    //fi  uniformly on [0,2pi).
                    float fi = RandomFloat(0, (2 * 3.14f));

                    //to obtain the vector(sqrt 1-z2 cos angle, sqrt 1-z2 sin fi, z)
                    float pointX = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Cos(fi);
                    float pointY = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Sin(fi);
                    float pointZ = Z;

                    Vector3 normalizedVector = Vector3.Normalize(new Vector3(pointX, pointY, pointZ));
                    GameObject throwedObj = Instantiate(m_coins, m_enemy.transform.position, Quaternion.identity);
                    throwedObj.GetComponent<Rigidbody2D>().AddForce(new Vector3(normalizedVector.x += velocity.x, normalizedVector.y += velocity.y, 0), ForceMode2D.Impulse);
                }
                break;
            case "radialEnemy":
                for (int i = 0; i < 4; i++)
                {
                    Vector2 velocity = new Vector2(Random.Range(-3, 3), 7);
                    float angle;
                    angle = 20 * Mathf.Deg2Rad;

                    //z  uniformly on [cos0,1]
                    float Z = RandomFloat(Mathf.Cos(angle), 1);

                    //fi  uniformly on [0,2pi).
                    float fi = RandomFloat(0, (2 * 3.14f));

                    //to obtain the vector(sqrt 1-z2 cos angle, sqrt 1-z2 sin fi, z)
                    float pointX = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Cos(fi);
                    float pointY = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Sin(fi);
                    float pointZ = Z;

                    Vector3 normalizedVector = Vector3.Normalize(new Vector3(pointX, pointY, pointZ));
                    GameObject throwedObj = Instantiate(m_coins, m_enemy.transform.position, Quaternion.identity);
                    throwedObj.GetComponent<Rigidbody2D>().AddForce(new Vector3(normalizedVector.x += velocity.x, normalizedVector.y += velocity.y, 0), ForceMode2D.Impulse);
                }
                break;
            case "burstEnemy":
                for (int i = 0; i < 5; i++)
                {
                    Vector2 velocity = new Vector2(Random.Range(-3, 3), 7);
                    float angle;
                    angle = 20 * Mathf.Deg2Rad;

                    //z  uniformly on [cos0,1]
                    float Z = RandomFloat(Mathf.Cos(angle), 1);

                    //fi  uniformly on [0,2pi).
                    float fi = RandomFloat(0, (2 * 3.14f));

                    //to obtain the vector(sqrt 1-z2 cos angle, sqrt 1-z2 sin fi, z)
                    float pointX = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Cos(fi);
                    float pointY = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Sin(fi);
                    float pointZ = Z;

                    Vector3 normalizedVector = Vector3.Normalize(new Vector3(pointX, pointY, pointZ));
                    GameObject throwedObj = Instantiate(m_coins, m_enemy.transform.position, Quaternion.identity);
                    throwedObj.GetComponent<Rigidbody2D>().AddForce(new Vector3(normalizedVector.x += velocity.x, normalizedVector.y += velocity.y, 0), ForceMode2D.Impulse);
                }
                break;
            case "turret":
                for (int i = 0; i < 3; i++)
                {
                    Vector2 velocity = new Vector2(Random.Range(-3, 3), 7);
                    float angle;
                    angle = 20 * Mathf.Deg2Rad;

                    //z  uniformly on [cos0,1]
                    float Z = RandomFloat(Mathf.Cos(angle), 1);

                    //fi  uniformly on [0,2pi).
                    float fi = RandomFloat(0, (2 * 3.14f));

                    //to obtain the vector(sqrt 1-z2 cos angle, sqrt 1-z2 sin fi, z)
                    float pointX = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Cos(fi);
                    float pointY = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Sin(fi);
                    float pointZ = Z;

                    Vector3 normalizedVector = Vector3.Normalize(new Vector3(pointX, pointY, pointZ));
                    GameObject throwedObj = Instantiate(m_coins, m_enemy.transform.position, Quaternion.identity);
                    throwedObj.GetComponent<Rigidbody2D>().AddForce(new Vector3(normalizedVector.x += velocity.x, normalizedVector.y += velocity.y, 0), ForceMode2D.Impulse);
                }
                break;
            case "patrolTop":
                for (int i = 0; i < 4; i++)
                {
                    Vector2 velocity = new Vector2(Random.Range(-3, 3), 7);
                    float angle;
                    angle = 20 * Mathf.Deg2Rad;

                    //z  uniformly on [cos0,1]
                    float Z = RandomFloat(Mathf.Cos(angle), 1);

                    //fi  uniformly on [0,2pi).
                    float fi = RandomFloat(0, (2 * 3.14f));

                    //to obtain the vector(sqrt 1-z2 cos angle, sqrt 1-z2 sin fi, z)
                    float pointX = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Cos(fi);
                    float pointY = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Sin(fi);
                    float pointZ = Z;

                    Vector3 normalizedVector = Vector3.Normalize(new Vector3(pointX, pointY, pointZ));
                    GameObject throwedObj = Instantiate(m_coins, m_enemy.transform.position, Quaternion.identity);
                    throwedObj.GetComponent<Rigidbody2D>().AddForce(new Vector3(normalizedVector.x += velocity.x, normalizedVector.y += velocity.y, 0), ForceMode2D.Impulse);
                }
                break;
            default:
                break;
        }
       
    }
}
