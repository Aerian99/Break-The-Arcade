using UnityEngine;
using System.IO;
using System.Collections.Generic;
public static class SaveSystem
{
    
    public static void SaveQuest(Quest[] quests, QuestSaver.PowerUps powerUps)
    {
        //QUEST 1 PROPERTIES
        PlayerPrefs.SetString("Quest1Assigment",quests[0].assigment);
        PlayerPrefs.SetInt("Quest1MonstersKilled", quests[0].actualMonstersKilled);
        PlayerPrefs.SetInt("Quest1MonstersToKill", quests[0].monstersToKill);
        PlayerPrefs.SetString("Quest1TargetMonster", quests[0].typesOfMonsters);
        PlayerPrefs.SetString("Quest1Reward", quests[0].reward);
        PlayerPrefs.SetInt("Quest1ArrayPos", quests[0].position);
        


        //QUEST 2 PROPERTIES
        PlayerPrefs.SetString("Quest2Assigment", quests[1].assigment);
        PlayerPrefs.SetInt("Quest2MonstersKilled", quests[1].actualMonstersKilled);
        PlayerPrefs.SetInt("Quest2MonstersToKill", quests[1].monstersToKill);
        PlayerPrefs.SetString("Quest2TargetMonster", quests[1].typesOfMonsters);
        PlayerPrefs.SetString("Quest2Reward", quests[1].reward);
        PlayerPrefs.SetInt("Quest2ArrayPos", quests[1].position);



        //QUEST 3 PROPERTIES
        PlayerPrefs.SetString("Quest3Assigment", quests[2].assigment);
        PlayerPrefs.SetInt("Quest3MonstersKilled", quests[2].actualMonstersKilled);
        PlayerPrefs.SetInt("Quest3MonstersToKill", quests[2].monstersToKill);
        PlayerPrefs.SetString("Quest3TargetMonster", quests[2].typesOfMonsters);
        PlayerPrefs.SetString("Quest3Reward", quests[2].reward);
        PlayerPrefs.SetInt("Quest3ArrayPos", quests[2].position);


        //QUEST REWARDS
        PlayerPrefs.SetInt("PurpleBullet", powerUps.damagePurpleGun);
        PlayerPrefs.SetInt("YellowBullet", powerUps.damageLaserGun);
        PlayerPrefs.SetInt("RedBullet", powerUps.damageRedGun);
        PlayerPrefs.SetInt("HealPowerUp", powerUps.healPowerUp);
        PlayerPrefs.SetInt("PlayerLifes", powerUps.playerUpLifes);


    }

    public static QuestSaver.PowerUps LoadPlayer(Quest[] quests, QuestSaver.PowerUps powerUps, out int coins)
    {
        coins = PlayerPrefs.GetInt("TotalCoins");
       //QUEST 1 PROPERTIES
        quests[0].assigment = PlayerPrefs.GetString("Quest1Assigment");
        quests[0].actualMonstersKilled = PlayerPrefs.GetInt("Quest1MonstersKilled");
        quests[0].monstersToKill = PlayerPrefs.GetInt("Quest1MonstersToKill");
        quests[0].typesOfMonsters = PlayerPrefs.GetString("Quest1TargetMonster");
        quests[0].reward = PlayerPrefs.GetString("Quest1Reward");
        quests[0].position = PlayerPrefs.GetInt("Quest1ArrayPos");


        //QUEST 2 PROPERTIES
        quests[1].assigment = PlayerPrefs.GetString("Quest2Assigment");
        quests[1].actualMonstersKilled = PlayerPrefs.GetInt("Quest2MonstersKilled");
        quests[1].monstersToKill = PlayerPrefs.GetInt("Quest2MonstersToKill");
        quests[1].typesOfMonsters = PlayerPrefs.GetString("Quest2TargetMonster");
        quests[1].reward = PlayerPrefs.GetString("Quest2Reward");
        quests[1].position = PlayerPrefs.GetInt("Quest2ArrayPos");


        //QUEST 3 PROPERTIES
        quests[2].assigment = PlayerPrefs.GetString("Quest3Assigment");
        quests[2].actualMonstersKilled = PlayerPrefs.GetInt("Quest3MonstersKilled");
        quests[2].monstersToKill = PlayerPrefs.GetInt("Quest3MonstersToKill");
        quests[2].typesOfMonsters = PlayerPrefs.GetString("Quest3TargetMonster");
        quests[2].reward = PlayerPrefs.GetString("Quest3Reward");
        quests[2].position = PlayerPrefs.GetInt("Quest3ArrayPos");

        //QUEST REWARDS 
        powerUps.damagePurpleGun = PlayerPrefs.GetInt("PurpleBullet");
        powerUps.damageLaserGun = PlayerPrefs.GetInt("YellowBullet");
        powerUps.damageRedGun = PlayerPrefs.GetInt("RedBullet");
        powerUps.healPowerUp = PlayerPrefs.GetInt("HealPowerUp");
        powerUps.playerUpLifes = PlayerPrefs.GetInt("PlayerLifes");

        //RETURNS THE POWERUPS VALUES
        return powerUps;

    }

    public static void SaveCoins(int coins)
    {
        PlayerPrefs.SetInt("TotalCoins", coins);
    }

    public static void ResetAll()
    {
        PlayerPrefs.DeleteKey("Quest1Assigment");
        PlayerPrefs.DeleteKey("Quest1MonstersKilled");
        PlayerPrefs.DeleteKey("Quest1MonstersToKill");
        PlayerPrefs.DeleteKey("Quest1TargetMonster");
        PlayerPrefs.DeleteKey("Quest1Reward");
        PlayerPrefs.DeleteKey("Quest1ArrayPos");



        PlayerPrefs.DeleteKey("Quest2Assigment");
        PlayerPrefs.DeleteKey("Quest2MonstersKilled");
        PlayerPrefs.DeleteKey("Quest2MonstersToKill");
        PlayerPrefs.DeleteKey("Quest2TargetMonster");
        PlayerPrefs.DeleteKey("Quest2Reward");
        PlayerPrefs.DeleteKey("Quest2ArrayPos");




        PlayerPrefs.DeleteKey("Quest3Assigment");
        PlayerPrefs.DeleteKey("Quest3MonstersKilled");
        PlayerPrefs.DeleteKey("Quest3MonstersToKill");
        PlayerPrefs.DeleteKey("Quest3TargetMonster");
        PlayerPrefs.DeleteKey("Quest3Reward");
        PlayerPrefs.DeleteKey("Quest3ArrayPos");



        PlayerPrefs.DeleteKey("PurpleBullet");
        PlayerPrefs.DeleteKey("YellowBullet");
        PlayerPrefs.DeleteKey("RedBullet");
        PlayerPrefs.DeleteKey("HealPowerUp");
        PlayerPrefs.DeleteKey("PlayerLifes");

        PlayerPrefs.DeleteKey("TotalCoins");


    }
}
