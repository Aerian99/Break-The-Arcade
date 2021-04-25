using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    float cd, cdDeath, cdAnim, cdGoMenu, cdFadeOutMenu;
    public bool isDead, hasWon, finishedGame;

    private void Start()
    {
        cd = 0;
        cdAnim = 0.2f;
        cdDeath = 1.5f;
        cdFadeOutMenu = 2.8f;
        cdGoMenu = 3.5f;
    }

    private void Update()
    {
        if (isDead)
        {
            cd += Time.deltaTime;
            if (cd >= cdAnim)
                anim.SetBool("fadeIn", true);
            if (cd >= cdDeath)
            {
                MainMenuManager.comesFromLevel = true;
                SceneManager.LoadScene("GameOver");
            }
        }

        if (hasWon)
        {
            CheckRemainingBullets();
            cd += Time.deltaTime;
            if (cd >= cdDeath)
            {
                GameObject.Find("YouWin").GetComponent<Animator>().SetBool("bossDead", true);
            }
            if (cd >= cdFadeOutMenu)
            {
                GameObject.Find("fadeOut").GetComponent<Animator>().SetBool("fadeIn", true);
            }
            if (cd >= cdGoMenu)
            {
                SceneManager.LoadScene("lvl2.01");
            }
        }
        if (finishedGame)
        {
            MainMenuManager.comesFromLevel = true;
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }

    private void CheckRemainingBullets()
    {
        if (GameObject.Find("demoBullet(Clone)"))
        {
            Destroy(GameObject.Find("demoBullet(Clone)"));
        }
    }
}

