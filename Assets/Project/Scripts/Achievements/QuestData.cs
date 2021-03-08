using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData : MonoBehaviour
{
    public string[] descriptions;
    public int[] monstersLeft;
    public int[] totalMonsters;

    public QuestData(Quest[] quests)
    {
        descriptions = new string[3];
        monstersLeft = new int[3];
        totalMonsters = new int[3];

        for (int i = 0; i < quests.Length; i++)
        {
            descriptions[i] = quests[i].assigment;
            monstersLeft[i] = quests[i].actualMonstersKilled;
            totalMonsters[i] = quests[i].monstersToKill;

        }

    }

}
