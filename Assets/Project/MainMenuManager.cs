using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject playButton, ExitButton, text;
    public void ShowButtons()
    {
        playButton.SetActive(true);
        ExitButton.SetActive(true);
        text.SetActive(false);
        GameObject.Find("p_cursor").GetComponent<CursorScript>().coinAdded = true;
    }
    public void Play()
    {
        SceneManager.LoadScene("MapGeneration");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
