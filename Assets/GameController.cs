﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int level;
    public struct PlayerStats
    {
        public bool shotgunBlue, shotgunGreen, purpleBlue, purpleGreen, LaserBlue, LaserGreen;
        public float damageRed, damageYellow, damagePurple;
        public float velocity, dashCooldown;
        public float redVelocity, purpleVelocity;
        public bool isLuckUp;
        public int lifes;
    };

    [HideInInspector] public bool redUnlocked, yellowUnlocked;
    private GameObject player;
    public GameObject m_coins, bulletPurple, bulletRed;
    float cdAbsorb, maxcdAbsorb;
    [HideInInspector]public bool activatedAbsorb;
    GameObject[] robotPatrols, radialEnemies;
    public PlayerStats playerCaracteristics;
    public PlayerStats pCaracteristicsBeforeBoss;

    private static GameController gameControllerInstance;

    private void Awake()
    {
        if (gameControllerInstance == null)
        {
            gameControllerInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        playerCaracteristics.damagePurple = GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.damagePurpleGun;
        playerCaracteristics.damageRed = GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.damagePurpleGun;
        playerCaracteristics.damageYellow = GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.damagePurpleGun;
        playerCaracteristics.velocity = 12f;
        playerCaracteristics.dashCooldown = 4f;
        playerCaracteristics.purpleVelocity = 50f;
        playerCaracteristics.redVelocity = 40f;
        playerCaracteristics.damagePurple = 5f;
        playerCaracteristics.damageRed = 4f;
        playerCaracteristics.damageYellow = 5f;
        playerCaracteristics.lifes = 5 + GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.playerUpLifes;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxcdAbsorb = 2.5f;
        cdAbsorb = maxcdAbsorb;
        activatedAbsorb = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Lvl1")
        {
            level = 1;

        }else if(SceneManager.GetActiveScene().name == "Lvl2.01")
        {
            level = 2;
            transform.GetChild(1).position = new Vector3(15.63f, -0.29f, 0f);
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
            Destroy(this.gameObject);

        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            GameObject.Find("Quest Saver").GetComponent<QuestSaver>().LoadSystem();
        }
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
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<playerBehaviour>().coins >= 10 && playerCaracteristics.velocity < 18f)
        {
            playerCaracteristics.velocity += 1.5f;
            player.GetComponent<playerBehaviour>().coins -= 10;
            SoundManagerScript.PlaySound("buyShop");
        }
        else
        {
            SoundManagerScript.PlaySound("error");
        }

    }


    public void buyFasterBullets()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<playerBehaviour>().coins >= 15 && playerCaracteristics.purpleVelocity < 80f)
        {
            playerCaracteristics.purpleVelocity += 10;
            playerCaracteristics.redVelocity += 10;
            player.GetComponent<playerBehaviour>().coins -= 15;
            SoundManagerScript.PlaySound("buyShop");
        }
        else
        {
            SoundManagerScript.PlaySound("error");
        }
    }

    public void buyOnePunch()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<playerBehaviour>().coins >= 500)
        {
            playerCaracteristics.damagePurple += 1000f;
            playerCaracteristics.damageRed += 1000f;
            playerCaracteristics.damageYellow += 1000f;
            player.GetComponent<playerBehaviour>().coins -= 500;
            SoundManagerScript.PlaySound("buyShop");
        }
        else
        {
            SoundManagerScript.PlaySound("error");
        }
    }

    public void buyMoreEnemyDrops()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<playerBehaviour>().coins >= 500)
        {
            playerCaracteristics.isLuckUp = true;
            player.GetComponent<playerBehaviour>().coins -= 500;
            SoundManagerScript.PlaySound("buyShop");
        }
        else
        {
            SoundManagerScript.PlaySound("error");
        }
    }

    public void buyDashCooldown()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<playerBehaviour>().coins >= 50)
        {
            playerCaracteristics.dashCooldown -= 0.5f;
            player.GetComponent<playerBehaviour>().coins -= 50;
            SoundManagerScript.PlaySound("buyShop");
        }
        else
        {
            SoundManagerScript.PlaySound("error");
        }
    }

    public void buyTryIt()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<playerBehaviour>().coins >= 5000)
        {
            playerCaracteristics.dashCooldown = 0;
            playerCaracteristics.isLuckUp = true;
            playerCaracteristics.damagePurple = 1000f;
            playerCaracteristics.damageRed = 1000f;
            playerCaracteristics.damageYellow = 1000f;
            playerCaracteristics.purpleVelocity += 10f;
            playerCaracteristics.redVelocity += 10f;
            playerCaracteristics.velocity += 20f;
            playerCaracteristics.lifes = 1000;
            player.GetComponent<playerBehaviour>().coins -= 5000;
            SoundManagerScript.PlaySound("buyShop");
        }
        else
        {
            SoundManagerScript.PlaySound("error");
        }
    }
}
