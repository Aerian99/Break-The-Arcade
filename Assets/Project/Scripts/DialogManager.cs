using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public string[] sentences;
    public int index;
    public float speed;

    public GameObject continueButton, bocadillo, purple, yellow, red;
    public bool typing = false;

    private void Start()
    {
        if(!typing)
        {
            StartCoroutine(Typing());
        }
    }
    private void Update()
    {
        if (displayText.text == sentences[index])
        { 
            continueButton.SetActive(true);
        }

        if (continueButton.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {

            NextSentence();
        }
    }
    public IEnumerator Typing()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            SoundManagerScript.StopSound();
            SoundManagerScript.PlaySound("arcadeSpeaking");
            displayText.text += letter;
            yield return new WaitForSeconds(speed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            displayText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            typing = false;
            index = 0;
            displayText.text = "";
            continueButton.SetActive(false);
            bocadillo.SetActive(false);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<playerMovement>().enabled = true;
            if (purple.activeInHierarchy)
                purple.GetComponent<PurpleShoot>().enabled = true;
            else if (yellow.activeInHierarchy)
                yellow.GetComponent<YellowShoot>().enabled = true;
            else if (red.activeInHierarchy)
                red.GetComponent<RedShoot>().enabled = true;

        }
    }
}
