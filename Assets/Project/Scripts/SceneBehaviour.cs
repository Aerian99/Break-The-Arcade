using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    private bool isPlaying;
    public GameObject player;

    private void Start()
    {
        isPlaying = false;
    }

    void Update()
    {
        playGameOver();
        if (playerBehaviour._playerLifes == 0)
        {
            StartCoroutine(PlayAgain(0.8f));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            player.transform.position = new Vector3(95f, 2.3f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            player.transform.position = new Vector3(142.7f, 9.3f, 0f);
        }
    }
        IEnumerator PlayAgain(float time)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene("Prototype");
        }

        void playGameOver()
        {
            if (playerBehaviour._playerLifes == 0 && !isPlaying)
            {
                SoundManagerScript.PlaySound("gameOver");
                isPlaying = true;
            }
        }
}
