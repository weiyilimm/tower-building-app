using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StopWatch : MonoBehaviour {

    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI EarnedScoreText;
    public TMP_Dropdown dropDown;
    public static double GlobalXP = 0;
    public static double MainXP = 0;
    public static double ArtsXP = 0;
    public static double BioCheXP = 0;
    public static double ComSciXP = 0;
    public static double EngXP = 0;
    public static double GeoXP = 0;
    public static double LanXP = 0;
    public static double LawPolXP = 0;
    public static double PhyMathXP = 0;

    private int multiplierXP = 1;
    private double earnedLocalXP;
    private double earnedGlobalXP;
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
        //Local XP 1XP per second
        earnedLocalXP = Math.Round(theTime) * multiplierXP;
        //Global XP is 10% of local XP
        earnedGlobalXP = earnedLocalXP * 0.1;
        GlobalXP += earnedGlobalXP;
        playing = false;
        //Set the time to 0 
        theTime = 0;
        //Replace the timer text to 00:00:00
        TimerText.text = "00:00:00";
        //Pop up appears to show how much XP user has earned
        PopUp.SetActive(true);
        //Pop up text
        EarnedScoreText.text = "You've just earned" + " " + (earnedLocalXP + GlobalXP).ToString() +"XP in" + " " + (dropDown.options[dropDown.value].text)
                                + " " + (earnedGlobalXP).ToString() + "XP in other buildings";

        theTime = 0;
        MainXP += earnedGlobalXP;
        ArtsXP += earnedGlobalXP;
        BioCheXP += earnedGlobalXP;
        ComSciXP += earnedGlobalXP;
        EngXP += earnedGlobalXP;
        GeoXP += earnedGlobalXP;
        LanXP += earnedGlobalXP;
        LawPolXP += earnedGlobalXP;
        PhyMathXP += earnedGlobalXP;
        switch (dropDown.options[dropDown.value].text){
            case "MAIN":
                MainXP += earnedLocalXP;
                Debug.Log(BioCheXP);
                break;
            case "ARTS":
                ArtsXP += earnedLocalXP;
                break;
            case "BIOLOGY CHEMISTRY":
                BioCheXP += earnedLocalXP;
                Debug.Log(BioCheXP);
                break;
            case "COMPUTER SCIENCE":
                ComSciXP += earnedLocalXP;
                break;
            case "ENGINEERING":
                EngXP += earnedLocalXP;
                break;
            case "GEOGRAPHY":
                GeoXP += earnedLocalXP;
                break;
            case "LANGUAGES":
                LanXP += earnedLocalXP;
                break;
            case "LAW POLITICS":
                LawPolXP += earnedLocalXP;
                break;
            case "PHYSICS MATH":
                PhyMathXP += earnedLocalXP;
                break;
        }
    }
}