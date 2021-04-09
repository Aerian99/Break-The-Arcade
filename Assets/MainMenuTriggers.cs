using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTriggers : MonoBehaviour
{
    [HideInInspector] public bool playTriggerBool, questTriggerBool, optionsTriggerBool, creditsTriggerBool;
    void Start()
    {
        playTriggerBool = false;
        questTriggerBool = false;
        optionsTriggerBool = false;
        creditsTriggerBool = false;
    }

    public void playTrigger()
    {
        playTriggerBool = true;
    }
    
    public void questTrigger()
    {
        questTriggerBool = true;
    }
    
    public void optionsTrigger()
    {
        optionsTriggerBool = true;
    }
    
    public void creditsTrigger()
    {
        creditsTriggerBool = true;
    }
}
