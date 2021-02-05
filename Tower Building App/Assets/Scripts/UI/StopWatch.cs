using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StopWatch : MonoBehaviour {

    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI EarnedScoreText;
    public static double GlobalXP = 0;
    private int multiplierXP = 2;
    private double earnedScore;
    private bool playing;
    private float theTime;
    public GameObject PopUp;

    void Update () {
        if (playing == true)
        {   
            theTime += Time.deltaTime;
            string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
            string seconds = (theTime % 60).ToString("00");
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
    }

    public void Reset()
    {   
        PopUp.SetActive(true);
        earnedScore = Math.Round(theTime) * multiplierXP;
        GlobalXP += earnedScore;
        EarnedScoreText.text = "You've just earned" + " " + (earnedScore).ToString() + "XP" 
                                + " " + "with a total global XP of" + " " + (GlobalXP).ToString() + "XP";
        playing = false;
        TimerText.text = "00:00:00";
        theTime = 0;
        Debug.Log(GlobalXP);
    }
}