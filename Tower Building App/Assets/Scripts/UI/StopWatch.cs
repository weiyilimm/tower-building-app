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
    private bool playing;
    

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

    public void ClickPlay ()
    {
        playing = true;
    }

    public void ClickStop()
    {   
        playing = false;
        //Set the time to 0 
        TimeCounted = 0;
        //Replace the timer text to 00:00:00
        TimerText.text = "00:00:00";
    }
}