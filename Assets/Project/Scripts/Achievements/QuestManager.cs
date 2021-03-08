using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestManager : MonoBehaviour
{
    public Quest[] quest;
    public TextMeshProUGUI[] questToShow, achievementProgress;
    

    private void Update()
    {
        for (int i = 0; i < GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest.Length; i++)
        {
            quest = GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest;
        }

        for (int i = 0; i < quest.Length; i++)
        {
            questToShow[i].text = quest[i].assigment;
        }
        updateAchievements();
    }

    public void updateAchievements()
    {
        for (int i = 0; i < quest.Length; i++)
        {
            achievementProgress[i].text = quest[i].actualMonstersKilled + " / " + quest[i].monstersToKill;
        }
    }
}
