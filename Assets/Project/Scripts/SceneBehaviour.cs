using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    private bool isPlaying;

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
