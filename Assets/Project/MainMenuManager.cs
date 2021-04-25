using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject playButton,
        ExitButton,
        optionsButton,
        text,
        slider,
        volumeButton,
        backButton,
        questsButton,
        credits,
        creditText;

    public Animator camera, fadepanel;

    public bool pressedPlay;
    public static bool comesFromQuests, comesFromLevel;

    public void ShowButtons()
    {
        playButton.SetActive(true);
        ExitButton.SetActive(true);
        optionsButton.SetActive(true);
        questsButton.SetActive(true);
        text.SetActive(false);
        credits.SetActive(true);
        SoundManagerScript.PlaySound("coin");
        //GameObject.Find("p_cursor").GetComponent<CursorScript>().coinAdded = true;
        GameObject.Find("cursor_alternative").GetComponent<CursorAlternative>().coinAdded = true;
        camera.SetBool("CoinInsert", true);
    }

    public void Play()
    {
        if (playButton.GetComponent<MainMenuTriggers>().playTriggerBool)
        {
            //SceneManager.LoadScene("Lvl1");
            camera.SetBool("In", true);
            fadepanel.SetBool("In", true);
            pressedPlay = true;
        }

        if (camera.GetComponent<DemoCameraAnimation>().endCamAnimation)
        {
            Destroy(GameObject.Find("Music"));
            SceneManager.LoadScene("Lvl1");
        }
    }

    public void Options()
    {
        if (optionsButton.GetComponent<MainMenuTriggers>().optionsTriggerBool)
        {
            playButton.SetActive(false);
            ExitButton.SetActive(false);
            optionsButton.SetActive(false);
            questsButton.SetActive(false);
            credits.SetActive(false);
            volumeButton.SetActive(true);
            slider.SetActive(true);
            backButton.SetActive(true);
            optionsButton.GetComponent<MainMenuTriggers>().optionsTriggerBool = false;
        }
    }

    public void Back()
    {
        volumeButton.SetActive(false);
        slider.SetActive(false);
        creditText.SetActive(false);
        backButton.SetActive(false);
        playButton.SetActive(true);
        optionsButton.SetActive(true);
        ExitButton.SetActive(true);
        questsButton.SetActive(true);
        credits.SetActive(true);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Achievements()
    {
        if (questsButton.GetComponent<MainMenuTriggers>().questTriggerBool)
        {
            SceneManager.LoadScene("Achievements");
        }
    }

    public void Credits()
    {
        if (credits.GetComponent<MainMenuTriggers>().creditsTriggerBool)
        {
            playButton.SetActive(false);
            ExitButton.SetActive(false);
            optionsButton.SetActive(false);
            questsButton.SetActive(false);
            credits.SetActive(false);
            creditText.SetActive(true);
            credits.GetComponent<MainMenuTriggers>().creditsTriggerBool = false;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        Play();
        Achievements();
        Options();
        Credits();

        if (comesFromQuests)
        {
            GameObject.Find("cursor_alternative").GetComponent<CursorAlternative>().coinAdded = true;
            playButton.SetActive(true);
            ExitButton.SetActive(true);
            optionsButton.SetActive(true);
            questsButton.SetActive(true);
            text.SetActive(false);
            credits.SetActive(true);
            camera.SetBool("CoinInsert", true);
            comesFromQuests = false;
        }
    }
}