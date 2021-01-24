﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject playButton, ExitButton, optionsButton, text, slider, volumeButton, backButton;
    public void ShowButtons()
    {
        playButton.SetActive(true);
        ExitButton.SetActive(true);
        optionsButton.SetActive(true);
        text.SetActive(false);
        SoundManagerScript.PlaySound("coin");
        GameObject.Find("p_cursor").GetComponent<CursorScript>().coinAdded = true;
    }
    public void Play()
    {
        SceneManager.LoadScene("MapGeneration");
    }

    public void Options()
    {
        playButton.SetActive(false);
        ExitButton.SetActive(false);
        optionsButton.SetActive(false);
        volumeButton.SetActive(true);
        slider.SetActive(true);
        backButton.SetActive(true);
    }

    public void Back()
    {
        volumeButton.SetActive(false);
        slider.SetActive(false);
        backButton.SetActive(false);
        playButton.SetActive(true);
        optionsButton.SetActive(true);
        ExitButton.SetActive(true);

    }
    public void Exit()
    {
        Application.Quit();
    }
}
