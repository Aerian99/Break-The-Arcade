using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestSaver : MonoBehaviour
{
    private static QuestSaver questSaverInstance;
    public Quest[] quest = new Quest[3];


    private string[] enemies = { "Radials", "Robot Patrols", "Roof Patrols", "Rotators", "Aliens" };
    private string[] fixObjective = { "Kill", "Defeat", "Obliterate" };

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (questSaverInstance == null)
        {
            questSaverInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        SaveSystem.LoadPlayer(quest);


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

        for (int y = 0; y < quest.Length; y++)
        {
            if (quest[y].assigment == "")
            {
                GenerateQuest(y);
            }
        }

        CheckQuestComplete();
    }
    public void GenerateQuest(int index)
    {
        int numberOfEnemies; string typeOfEnemy;

        numberOfEnemies = Random.Range(8, 32);
        typeOfEnemy = enemies[Random.Range(0, enemies.Length)];
        quest[index].assigment = fixObjective[Random.Range(0, fixObjective.Length)] + " " + numberOfEnemies + " " + typeOfEnemy;
        quest[index].reward = "1HP";
        quest[index].monstersToKill = numberOfEnemies;
        quest[index].typesOfMonsters = typeOfEnemy;
        quest[index].actualMonstersKilled = 0;
        quest[index].position = index;
    }

    public void CheckQuestComplete()
    {
        for (int  i = 0;  i < quest.Length;  i++)
        {
            if(quest[i].actualMonstersKilled >= quest[i].monstersToKill)
            {
                quest[i].assigment = "";
            }
        }
    }
    IEnumerator Saving()
    {
        while (true)
        {
            SaveSystem.SaveQuest(quest);
            yield return new WaitForSeconds(5);
        }
        yield return null;
    }
}
