using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AchievementsOptions : MonoBehaviour
{
    public bool isAtAchievements;
    public GameObject textToRender;
    public void goMainMenu()
    {
        if(isAtAchievements)
        {
            isAtAchievements = false;
            GameObject.Find("GENERAL CANVAS").GetComponent<Animator>().SetBool("SeePowerUps", false);
            textToRender.SetActive(true);
        }
        else
        {
            MainMenuManager.comesFromQuests = true;
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void goSeePowerUps()
    {
        if(!isAtAchievements)
        { 
            GameObject.Find("GENERAL CANVAS").GetComponent<Animator>().SetBool("SeePowerUps", true);
            isAtAchievements = true;
            textToRender.SetActive(false);
        }
    }


    public void ResetAll()
    {
        for (int i = 0; i < GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest.Length; i++)
        {
            GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest[i].assigment = "";
            GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.damageLaserGun = 0;
            GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.damagePurpleGun = 0;
            GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.damageRedGun = 0;
            GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.healPowerUp = 0;
            GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.playerUpLifes = 0;
        }
        SaveSystem.ResetAll();
        for (int i = 0; i < GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest.Length; i++)
        { 
            if (GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest[i].assigment == "")
            {
                GameObject.Find("Quest Saver").GetComponent<QuestSaver>().GenerateQuest(i);
            }
        }
        SaveSystem.SaveQuest(GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest, GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps);
    }

}

