using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestManager : MonoBehaviour
{
    public Quest[] quest;
    public TextMeshProUGUI[] questToShow, achievementProgress, rewardsToShow, seePowerUps;

    public GameObject[] special;
    QuestSaver questLogic;
    private void Start()
    {
        questLogic = GameObject.Find("Quest Saver").GetComponent<QuestSaver>();
    }
    private void Update()
    {
        for (int i = 0; i < questLogic.quest.Length; i++)
        {
            quest = questLogic.quest;
        }

        for (int i = 0; i < quest.Length; i++)
        {
            questToShow[i].text = quest[i].assigment;
            rewardsToShow[i].text = "Reward: " + quest[i].reward;
        }
        updateAchievements();
        printPowerUps();
    }

    public void updateAchievements()
    {
        for (int i = 0; i < quest.Length; i++)
        {
            achievementProgress[i].text = quest[i].actualMonstersKilled + " / " + quest[i].monstersToKill;
        }
    }
    public void printPowerUps()
    {
        seePowerUps[0].text = "Purple Weapon DMG:   " + questLogic.m_PowerUps.damagePurpleGun;
        seePowerUps[1].text = "Laser Weapon DMG:    " + questLogic.m_PowerUps.damageLaserGun;
        seePowerUps[2].text = "Shotgun Weapon DMG:   " + questLogic.m_PowerUps.damageRedGun;
        seePowerUps[3].text = "PowerUp Extra Life:   " + questLogic.m_PowerUps.healPowerUp;
        seePowerUps[4].text = "Player Extra Life:   " + questLogic.m_PowerUps.playerUpLifes;
    }
}
