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

    void Start(){
        if (playing == true)
        {
            PauseButton.SetActive(true);
            PlayButton.SetActive(false);
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
    }

    
    //Unity built-in method to detect when the user left the app
    void OnApplicationPause (bool isGamePause)
    {   
        //Store the current time when the app is paused
        if (isGamePause) {
            gamePausedTime = DateTime.Now;
        }
    }

    
    //Unity built-in method to detect when the user is come back
    void OnApplicationFocus  (bool isGameFocus)
    {   
        //Trigger BackgroundTimer method when user come back 
        if (isGameFocus && playing == true ) {
            BackgroundTimer();
        }
    }

    /*
    Calculate the total time difference between 
    time when user left and time when user come back
    */
    void BackgroundTimer(){
        TimeCounted += ((float)(DateTime.Now - gamePausedTime).TotalSeconds);
    }

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
