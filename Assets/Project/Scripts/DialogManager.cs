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

    public GameObject bocadillo, purple, yellow, red;
    public bool typing = false;
    public bool isDashTutorial;
    public GameObject highlight;
    private bool endType;

    private void Start()
    {
        endType = false;
        if(!typing)
        {
            StartCoroutine(Typing());
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && endType)
        {
            NextSentence();
            endType = false;
        }
        if (isDashTutorial && index == 2)
        {
            highlight.SetActive(true);
            isDashTutorial = false;
        }
        if (!isDashTutorial && index == 0)
        {
            highlight.SetActive(false);
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

        endType = true;
    }

    public void NextSentence()
    {
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
