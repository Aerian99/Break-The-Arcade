using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public  TextMeshProUGUI timeCounter;

    private TimeSpan timePlaying;

    private bool timerGoing;

    private float elapsedTime;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        timeCounter.text = "00:00:00";
        timerGoing = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timerGoing);
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while(timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm' : 'ss' : 'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
}
