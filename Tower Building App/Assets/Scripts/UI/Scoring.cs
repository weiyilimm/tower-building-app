using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = System.Random;

public class Scoring : MonoBehaviour
{   
    //All static attributes will be used in UnlockBuilding.cs & ColorPicker.cs
    public static double MainXP = 0,
                        ArtsXP = 0,
                        BioCheXP = 0,
                        ComSciXP = 0,
                        EngXP = 0,
                        GeoXP = 0,
                        LanXP = 0,
                        LawPolXP = 0,
                        PhyMathXP = 0;
    /*
    XP multiplier is used to multiply the XP per second
    Default is 1XP per second
    */
    private int multiplierXP = 1;
    //Local Earned XP is how much XP earned in a specific building
    private double localEarnedXP;
    /*
    Global Earned XP is how much XP earned for all buildings
    Default is 10% of the local XP 
    */
    private double globalEarnedXP;
    //The text that appear in the Pop Up Window
    public TextMeshProUGUI EarnedScoreText;
    //The motivational quote that appear in the Pop Up Window
    public TextMeshProUGUI MotivationalQuote;
    //The drop down menu to get the current value that user selected
    public TMP_Dropdown DropDown;
    //Pop up is used to pop up a window when they earn XP
    public GameObject PopUp;
    /*
    timeCounted is how much time user has studied
    In order to do this, we need to access the static variable TimeCounted in StopWatch.cs
    */
    private float timeCounted;

    private string[] quotes = {"Work until your bank account looks like a phone number.",
    "Failure is not the opposite of success, it's part of success.",
    "Never put off until tomorrow what you can do the day after tomorrow.",
    "I didn’t fail the test. I just found 100 ways to do it wrong.",
    "Look at those successful people, you're not one of them.",
    "Strive for progress, not perfection.",
    "There are no traffic jams on the extra mile.",
    "People say nothing is impossible, but I do nothing every day.",
    "Procrastination gives you something to look forward to.",
    "Hard work pays off eventually, but laziness pays off now.",
    "I love deadlines. I love the whooshing noise they make as they go by.",
    "Nothing makes a person more productive than the last minute.",
    "Due tomorrow? Do tomorrow."
    }; 

    public void earnXP(){
        //Get the value in StopWatch.cs
        timeCounted = StopWatch.TimeCounted;
        //Local XP 1XP per second
        localEarnedXP = Math.Round(timeCounted) * multiplierXP;
        //Global XP is 10% of local XP
        globalEarnedXP = Math.Round(localEarnedXP * 0.1);
        
        //Pop up appears to show how much XP user has earned
        PopUp.SetActive(true);
        //Pop up text
        EarnedScoreText.text = "You've just earned" + " " + (localEarnedXP + globalEarnedXP).ToString() +"XP in" + " " + (DropDown.options[DropDown.value].text)
                                + " " + (globalEarnedXP).ToString() + "XP in other buildings";
        
        Random rand = new Random();  
        int index = rand.Next(quotes.Length);  
        MotivationalQuote.text = quotes[index];

        //Every building earns global XP (10 % of local XP)
        MainXP += globalEarnedXP;
        ArtsXP += globalEarnedXP;
        BioCheXP += globalEarnedXP;
        ComSciXP += globalEarnedXP;
        EngXP += globalEarnedXP;
        GeoXP += globalEarnedXP;
        LanXP += globalEarnedXP;
        LawPolXP += globalEarnedXP;
        PhyMathXP += globalEarnedXP;

        // Loop through the users building data and increase all of them by the 10% value
        for (int k=0; k<12; k++) {
            User_Data.data.building_stats[k].building_xp += (int)globalEarnedXP;
        }

        //Main building XP is global so it should get the full amount earned every time
        MainXP += localEarnedXP;
        for (int l=0; l<4; l++) {
            User_Data.data.building_stats[l].building_xp += (int)localEarnedXP;
        }

        //Update the User_Data variable that stores the global xp (global XP is the max_amount of XP earned for
        // every study session combined)
        User_Data.data.global_xp = (int)MainXP;

        //Get the current value of the dropdown, when the stop button is clicked
        switch (DropDown.options[DropDown.value].text){
            case "ARTS":
                ArtsXP += localEarnedXP;
                User_Data.data.building_stats[11].building_xp += (int)localEarnedXP;
                break;
            case "BIOLOGY CHEMISTRY":
                BioCheXP += localEarnedXP;
                User_Data.data.building_stats[6].building_xp += (int)localEarnedXP;
                break;
            case "COMPUTER SCIENCE":
                ComSciXP += localEarnedXP;
                User_Data.data.building_stats[5].building_xp += (int)localEarnedXP;
                break;
            case "ENGINEERING":
                EngXP += localEarnedXP;
                User_Data.data.building_stats[9].building_xp += (int)localEarnedXP;
                break;
            case "GEOGRAPHY":
                GeoXP += localEarnedXP;
                User_Data.data.building_stats[7].building_xp += (int)localEarnedXP;
                break;
            case "LANGUAGES":
                LanXP += localEarnedXP;
                User_Data.data.building_stats[8].building_xp += (int)localEarnedXP;
                break;
            case "LAW POLITICS":
                LawPolXP += localEarnedXP;
                User_Data.data.building_stats[10].building_xp += (int)localEarnedXP;
                break;
            case "PHYSICS MATH":
                PhyMathXP += localEarnedXP;
                User_Data.data.building_stats[4].building_xp += (int)localEarnedXP;
                break;
        }
        
        // Create and send an update request to the database for the Users new info then
        // Loop over the users buildings and send update requests for each one
        User_Data.data.CreateRequest("UPDATE_User");
        for (int s=0; s<12; s++) {
            User_Data.data.CreateRequest("UPDATE_User_Building", s);
        }
    }
}
