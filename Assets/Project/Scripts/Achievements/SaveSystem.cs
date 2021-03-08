using UnityEngine;
using System.IO;
using System.Collections.Generic;
public static class SaveSystem
{
    
    public static void SaveQuest(Quest[] quests)
    {
        PlayerPrefs.SetString("Quest1Assigment",quests[0].assigment);
        PlayerPrefs.SetInt("Quest1MonstersKilled", quests[0].actualMonstersKilled);
        PlayerPrefs.SetInt("Quest1MonstersToKill", quests[0].monstersToKill);
        PlayerPrefs.SetString("Quest1TargetMonster", quests[0].typesOfMonsters);
        PlayerPrefs.SetString("Quest1Reward", quests[0].reward);
        PlayerPrefs.SetInt("Quest1ArrayPos", quests[0].position);



        PlayerPrefs.SetString("Quest2Assigment", quests[1].assigment);
        PlayerPrefs.SetInt("Quest2MonstersKilled", quests[1].actualMonstersKilled);
        PlayerPrefs.SetInt("Quest2MonstersToKill", quests[1].monstersToKill);
        PlayerPrefs.SetString("Quest2TargetMonster", quests[1].typesOfMonsters);
        PlayerPrefs.SetString("Quest2Reward", quests[1].reward);
        PlayerPrefs.SetInt("Quest2ArrayPos", quests[1].position);




        PlayerPrefs.SetString("Quest3Assigment", quests[2].assigment);
        PlayerPrefs.SetInt("Quest3MonstersKilled", quests[2].actualMonstersKilled);
        PlayerPrefs.SetInt("Quest3MonstersToKill", quests[2].monstersToKill);
        PlayerPrefs.SetString("Quest3TargetMonster", quests[2].typesOfMonsters);
        PlayerPrefs.SetString("Quest3Reward", quests[2].reward);
        PlayerPrefs.SetInt("Quest3ArrayPos", quests[2].position);

    }

    public static void LoadPlayer(Quest[] quests)
    {
       
        quests[0].assigment = PlayerPrefs.GetString("Quest1Assigment");
        quests[0].actualMonstersKilled = PlayerPrefs.GetInt("Quest1MonstersKilled");
        quests[0].monstersToKill = PlayerPrefs.GetInt("Quest1MonstersToKill");
        quests[0].typesOfMonsters = PlayerPrefs.GetString("Quest1TargetMonster");
        quests[0].reward = PlayerPrefs.GetString("Quest1Reward");
        quests[0].position = PlayerPrefs.GetInt("Quest1ArrayPos");



        quests[1].assigment = PlayerPrefs.GetString("Quest2Assigment");
        quests[1].actualMonstersKilled = PlayerPrefs.GetInt("Quest2MonstersKilled");
        quests[1].monstersToKill = PlayerPrefs.GetInt("Quest2MonstersToKill");
        quests[1].typesOfMonsters = PlayerPrefs.GetString("Quest2TargetMonster");
        quests[1].reward = PlayerPrefs.GetString("Quest2Reward");
        quests[1].position = PlayerPrefs.GetInt("Quest2ArrayPos");



        quests[2].assigment = PlayerPrefs.GetString("Quest3Assigment");
        quests[2].actualMonstersKilled = PlayerPrefs.GetInt("Quest3MonstersKilled");
        quests[2].monstersToKill = PlayerPrefs.GetInt("Quest3MonstersToKill");
        quests[2].typesOfMonsters = PlayerPrefs.GetString("Quest3TargetMonster");
        quests[2].reward = PlayerPrefs.GetString("Quest3Reward");
        quests[2].position = PlayerPrefs.GetInt("Quest3ArrayPos");

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
    }
}
