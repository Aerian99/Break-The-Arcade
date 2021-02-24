using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class arcadeMachine : MonoBehaviour
{
    public GameObject exclamation;
    public TextMeshProUGUI displayText;
    public string[] sentences;
    private int index;
    public float speed;

    IEnumerator Typing()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(speed);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exclamation.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Typing());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exclamation.SetActive(false);
        }
    }
}
