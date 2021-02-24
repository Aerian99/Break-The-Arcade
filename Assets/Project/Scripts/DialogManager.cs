using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public string[] sentences;
    private int index;
    public float speed;

    public GameObject continueButton;

    private void Start()
    {
        StartCoroutine(Typing());
    }
    private void Update()
    {
        if (displayText.text == sentences[index])
        { 
            continueButton.SetActive(true);
        }
    }
    IEnumerator Typing()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
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
            displayText.text = "";
            continueButton.SetActive(false);
        }
    }
}
