﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdjustMusic : MonoBehaviour
{

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu" && !MainMenuManager.comesFromQuests)
            UpdateVolume(0.5f);
    }
    public void UpdateVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    
}
