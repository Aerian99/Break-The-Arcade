using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [HideInInspector] public bool redUnlocked, yellowUnlocked;
    private GameObject player;
    public GameObject m_coins, bulletPurple, bulletRed;
    float cdAbsorb, maxcdAbsorb;
    [HideInInspector]public bool activatedAbsorb;
    GameObject[] robotPatrols, radialEnemies;

    private static GameController gameControllerInstance;
    // Start is called before the first frame update
    void Start()
    {
       

        if (gameControllerInstance == null)
        {
            gameControllerInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        maxcdAbsorb = 2.5f;
        cdAbsorb = maxcdAbsorb;
        activatedAbsorb = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            Destroy(this.gameObject);
        if (activatedAbsorb)
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
    public void backToGame()
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).gameObject.active = true;
        gameObject.GetComponent<Animator>().SetTrigger("backToLevel");
    }

    public void buySpeed()
    {
        if(player.GetComponent<playerBehaviour>().coins >= 10)
        {
            player.GetComponent<playerMovement>().moveSpeed += 1.5f;
            player.GetComponent<playerBehaviour>().coins -= 10;
        }
    }


    public void buyFasterBullets()
    {
        if (player.GetComponent<playerBehaviour>().coins >= 15)
        {
            player.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<PurpleShoot>().bulletSpeed += 10f;
            player.transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<RedShoot>().bulletForce += 10f;
            player.GetComponent<playerBehaviour>().coins -= 15;
        }
    }

    public void buyOnePunch()
    {
        if (player.GetComponent<playerBehaviour>().coins >= 500)
        {
            bulletPurple.GetComponent<purpleBulletBehaviour>().bulletForce += 1000f;
            bulletRed.GetComponent<redBulletBehaviour>().bulletForce += 1000f;
            LaserShoot.damage += 1000f;
            player.GetComponent<playerBehaviour>().coins -= 500;
        }
    }

    public void buyMoreEnemyDrops()
    {
        if (player.GetComponent<playerBehaviour>().coins >= 500)
        {
            for (int i = 0; i < GameObject.Find("Barrels").transform.childCount; i++)
            {
                GameObject.Find("Barrels").transform.GetChild(i).GetComponent<barrilScript>().luckUp = true;
            }
            
            player.GetComponent<playerBehaviour>().coins -= 500;
        }
    }

    public void buyDashCooldown()
    {
        if (player.GetComponent<playerBehaviour>().coins >= 50)
        {
            player.GetComponent<playerMovement>().maxDashCooldown -= 0.5f;
            player.GetComponent<playerBehaviour>().coins -= 50;
        }
    }

    public void buyTryIt()
    {
        if (player.GetComponent<playerBehaviour>().coins >= 5000)
        {
            player.GetComponent<playerMovement>().maxDashCooldown = 0;
            for (int i = 0; i < GameObject.Find("Barrels").transform.childCount; i++)
            {
                GameObject.Find("Barrels").transform.GetChild(i).GetComponent<barrilScript>().luckUp = true;
            }
            bulletPurple.GetComponent<purpleBulletBehaviour>().bulletForce += 1000f;
            bulletRed.GetComponent<redBulletBehaviour>().bulletForce += 1000f;
            LaserShoot.damage += 1000f;
            player.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<PurpleShoot>().bulletSpeed += 10f;
            player.transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<RedShoot>().bulletForce += 10f;
            player.GetComponent<playerMovement>().moveSpeed += 20;
            player.GetComponent<playerBehaviour>()._playerLifes += 1000;
            player.GetComponent<playerBehaviour>().coins -= 5000;
        }
    }
}
