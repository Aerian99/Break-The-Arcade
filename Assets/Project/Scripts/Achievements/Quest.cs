using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public bool isActive;
    
    public string assigment;
    public string reward;
    public int monstersToKill;
    public int actualMonstersKilled;
    public string typesOfMonsters;
    public int position;
}
