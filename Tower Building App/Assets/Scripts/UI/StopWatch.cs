using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StopWatch : MonoBehaviour {

    //Used capitalize character to indicate public field
    //Used for 00:00:00 text shown in the clock scene
    public TextMeshProUGUI TimerText;
    //The variable will be used in scoring.cs 
    public static float TimeCounted;
    //Used lower case character to indicate private field
    private static bool playing;
    private DateTime gamePausedTime;
    public GameObject PauseButton;
    public GameObject PlayButton;
    //The dont destory object is canvas, it must be a parentless gameobject 
    public GameObject TimerDontDestory;
    public static StopWatch Instance;

    //Avoid multiple instances
    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(TimerDontDestory);
        }
        else{
            Destroy(TimerDontDestory);
        }
    }


    void Update () {
        if (playing == true)
        {   
            TimeCounted += Time.deltaTime;
            string hours = Mathf.Floor((TimeCounted % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((TimeCounted % 3600) / 60).ToString("00");
            string seconds = (TimeCounted % 60).ToString("00");
            TimerText.text = hours + ":" + minutes + ":" + seconds;
        }
        Debug.Log(gamePausedTime);
        // Debug.Log((float)(DateTime.Now - gamePausedTime).TotalSeconds);
    }

    // void OnApplicationPause (bool isGamePause)
    // {   
    //     //Store the current time when the app is paused
    //     if (isGamePause) {
    //         gamePausedTime = DateTime.Now;
    //     }
    // }

    // void OnApplicationFocus  (bool isGameFocus)
    // {
    //     if (isGameFocus) {
    //         BackgroundTimer();
    //     }
    //     // Debug.Log(TimeCounted);
    // }

    // void BackgroundTimer(){
    //     TimeCounted += ((float)(DateTime.Now - gamePausedTime).TotalSeconds);
    // }

    public void ClickPlay ()
    {
        playing = true;
        PauseButton.SetActive(true);
        PlayButton.SetActive(false);
    }

    public void ClickStop()
    {   
        playing = false;
        //Set the time to 0 
        TimeCounted = 0;
        //Replace the timer text to 00:00:00
        TimerText.text = "00:00:00";
        PauseButton.SetActive(false);
        PlayButton.SetActive(true);
    }
}