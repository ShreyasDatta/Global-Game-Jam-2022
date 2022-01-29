using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class Timer:MonoBehaviour
{
    public static Timer instance;

    public Text timerText;
    private TimeSpan timePlaying;

    private bool TimeGoing;

    private float elaspedTime;



    void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        timerText.text = "00:00:00";
        TimeGoing = true;
        elaspedTime = 0f;
    }

    void Update(){
        
        StartCoroutine(UpdateTimer());
    }

    public void endTimer(){
        TimeGoing = false;
    }

    public string TimePlayed(){
        return timerText.ToString();
    }

    private IEnumerator UpdateTimer()
    {
        while(TimeGoing){
            elaspedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elaspedTime);
            string timePlayingString = string.Format("{0:D2}:{1:D2}:{2:D2}", timePlaying.Hours, timePlaying.Minutes, timePlaying.Seconds);
            timerText.text = timePlayingString;
            yield return null;
        }
    }
}