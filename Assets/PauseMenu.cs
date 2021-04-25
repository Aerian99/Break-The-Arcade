using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool gamePaused, exitGame;
    private float cd;
    public GameObject m_PauseMenu, fadeOut, purple, yellow, red;

    private void Start()
    {
        gamePaused = exitGame =  false;
        cd = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                if (GameObject.Find("Temp Shop").GetComponent<OpenShop>().playerIn == false)
                { 
                    PauseGame();
                }
            
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        m_PauseMenu.SetActive(false);

        if (purple.activeInHierarchy)
            purple.GetComponent<PurpleShoot>().enabled = true;
        else if (yellow.activeInHierarchy)
            yellow.GetComponent<LaserShoot>().enabled = true;
        else if (red.activeInHierarchy)
            red.GetComponent<RedShoot>().enabled = true;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void PauseGame()
    {
        Time.timeScale = 0.0f;
        gamePaused = true;
        m_PauseMenu.SetActive(true);

        if (purple.activeInHierarchy)
            purple.GetComponent<PurpleShoot>().enabled = false;
        else if (yellow.activeInHierarchy)
            yellow.GetComponent<LaserShoot>().enabled = false;
        else if (red.activeInHierarchy)
            red.GetComponent<RedShoot>().enabled = false;

    }
}
