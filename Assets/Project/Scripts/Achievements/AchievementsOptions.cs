using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AchievementsOptions : MonoBehaviour
{
    public bool isAtAchievements;
    public GameObject textToRender;
    public void goMainMenu()
    {
        if(isAtAchievements)
        {
            isAtAchievements = false;
            GameObject.Find("GENERAL CANVAS").GetComponent<Animator>().SetBool("SeePowerUps", false);
            textToRender.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void goSeePowerUps()
    {
        if(!isAtAchievements)
        { 
            GameObject.Find("GENERAL CANVAS").GetComponent<Animator>().SetBool("SeePowerUps", true);
            isAtAchievements = true;
            textToRender.SetActive(false);
        }
    }
}
