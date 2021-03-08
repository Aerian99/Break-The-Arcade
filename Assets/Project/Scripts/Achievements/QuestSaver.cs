using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestSaver : MonoBehaviour
{
    public struct PowerUps
    {
        public int damagePurpleGun, damageLaserGun, damageRedGun, healPowerUp, playerUpLifes;
    }

    public PowerUps m_PowerUps;
    private static QuestSaver questSaverInstance;
    public Quest[] quest = new Quest[3];

    private string[] enemies = { "Radials", "Robot Patrols", "Roof Patrols", "Rotators", "Aliens" };
    private string[] fixObjective = { "Kill", "Defeat", "Obliterate" };
    private string[] rewards = { "1 HP more", "+1 Damage Purple Gun", "+1 Damage Laser Gun", "+1 Damage ShootGun", "Heal powerUp heals 1 more" };

    private void Awake()
    {
        m_PowerUps.damagePurpleGun = m_PowerUps.damageLaserGun = m_PowerUps.healPowerUp = m_PowerUps.damageRedGun = m_PowerUps.playerUpLifes = 0;
        DontDestroyOnLoad(this.gameObject);
        if (questSaverInstance == null)
        {
            questSaverInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        m_PowerUps = SaveSystem.LoadPlayer(quest, m_PowerUps);

        for (int y = 0; y < quest.Length; y++)
        {
            if (quest[y].assigment == "")
            {
                GenerateQuest(y);
            }
        }

        StartCoroutine(Saving());
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Achievements")
        {
            GameObject.Find("QuestManager").GetComponent<QuestManager>().quest = quest;
        }

        CheckQuestComplete();
        for (int y = 0; y < quest.Length; y++)
        {
            if(quest[y].isActive)
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().special[y].SetActive(true);
            }
            else
            {
                GameObject.Find("QuestManager").GetComponent<QuestManager>().special[y].SetActive(false);
            }

            if (quest[y].assigment == "")
            {
                GenerateQuest(y);
            }
        }

    }
    public void GenerateQuest(int index)
    {
        int rand = Random.Range(0, 100);

        if(rand <= 5)
        {
            quest[index].assigment = "Kill the Boss";
            quest[index].reward = "+2 Damage ShootGun";
            quest[index].monstersToKill = 1;
            quest[index].typesOfMonsters = "Boss";
            quest[index].actualMonstersKilled = 0;
            quest[index].position = index;
            quest[index].isActive = true;
        }
        else
        {
            int numberOfEnemies; string typeOfEnemy;

            numberOfEnemies = Random.Range(8, 32);
            typeOfEnemy = enemies[Random.Range(0, enemies.Length)];
            quest[index].assigment = fixObjective[Random.Range(0, fixObjective.Length)] + " " + numberOfEnemies + " " + typeOfEnemy;
            quest[index].reward = rewards[Random.Range(0, rewards.Length)];
            quest[index].monstersToKill = numberOfEnemies;
            quest[index].typesOfMonsters = typeOfEnemy;
            quest[index].actualMonstersKilled = 0;
            quest[index].position = index;
            quest[index].isActive = false;
        }

       
    }

    public void CheckQuestComplete()
    {
        for (int  i = 0;  i < quest.Length;  i++)
        {
            if(quest[i].actualMonstersKilled >= quest[i].monstersToKill)
            {
                switch (quest[i].reward)
                {
                    case "1 HP more":
                        m_PowerUps.playerUpLifes += 1;
                        break;
                    case "+1 Damage Purple Gun":
                        m_PowerUps.damagePurpleGun += 1;
                        break;
                    case "+1 Damage Laser Gun":
                        m_PowerUps.damageLaserGun += 1;
                        break;
                    case "+1 Damage ShootGun":
                        m_PowerUps.damageRedGun += 1;
                        break;
                    case "Heal powerUp heals 1 more":
                        m_PowerUps.healPowerUp += 1;
                        break;
                    case "+2 Damage ShootGun":
                        m_PowerUps.damageRedGun += 2;
                        quest[i].isActive = false;
                        break;
                    default:
                        break;
                }
                quest[i].assigment = "";
            }
        }
    }
    IEnumerator Saving()
    {
        while (true)
        {
            SaveSystem.SaveQuest(quest, m_PowerUps);
            yield return new WaitForSeconds(5);
        }
        yield return null;
    }
}
