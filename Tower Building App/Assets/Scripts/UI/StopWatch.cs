using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StopWatch : MonoBehaviour {

    public TextMeshProUGUI Timer;
    public TextMeshProUGUI Score;
    public int MultiplierXP = 2;
    private bool playing;
    private float theTime;

 
    void Update () {
        if (playing == true)
        {
            theTime += Time.deltaTime;
            string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
            string seconds = (theTime % 60).ToString("00");
            Timer.text = hours + ":" + minutes + ":" + seconds;
        }
    }

    public void ClickPlay ()
    {
        playing = true;
    }

    public void ClickStop()
    {
        playing = false;
    }

    public void Reset()
    {   
        Score.text = (Math.Round(theTime) * MultiplierXP).ToString() + "XP ";
        playing = false;
        Timer.text = "00:00:00";
        theTime = 0;
    }
}