using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    public GameObject BossObject;
    public GameObject[] doors;

    private void Start()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject GameController = GameObject.FindGameObjectWithTag("gameController");
        GameController.GetComponent<GameController>().pCaracteristicsBeforeBoss = GameController.GetComponent<GameController>().playerCaracteristics;
        GameController.GetComponent<GameController>().playerCaracteristics.LaserBlue = false;
        GameController.GetComponent<GameController>().playerCaracteristics.LaserGreen = false;
        GameController.GetComponent<GameController>().playerCaracteristics.shotgunBlue = false;
        GameController.GetComponent<GameController>().playerCaracteristics.shotgunGreen = false;
        GameController.GetComponent<GameController>().playerCaracteristics.purpleBlue = false;
        GameController.GetComponent<GameController>().playerCaracteristics.purpleGreen = false;

        BossObject.SetActive(true);
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<Animator>().SetBool("hasPassed", true);
            doors[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject GameController = GameObject.Find("Game Controller");
            GameController.GetComponent<GameController>().playerCaracteristics = GameController.GetComponent<GameController>().pCaracteristicsBeforeBoss;
        }
    }
}
