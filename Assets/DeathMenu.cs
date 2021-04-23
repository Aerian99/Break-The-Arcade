using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    void Start()
    {
        StartCoroutine(GoMenu());
    }

    IEnumerator GoMenu()
    {   
        yield return new WaitForSeconds(4);
        anim.SetBool("fadeIn", true);
        yield return new WaitForSeconds(1.5f);
        this.gameObject.GetComponent<AudioSource>().Stop();
        if(GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().level == 1)
        {
            GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.lifes = 5 + GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.playerUpLifes;
            SceneManager.LoadScene("Lvl1");
        }

        if (GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().level == 2)
        {
            GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.lifes = 5 + GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.playerUpLifes;
            SceneManager.LoadScene("Lvl2.01");
        }

    }
}
