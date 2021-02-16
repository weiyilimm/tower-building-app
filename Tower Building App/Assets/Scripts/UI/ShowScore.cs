using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowScore : MonoBehaviour
{

    public TextMeshProUGUI[] EarnedScoreText;
    private double localEarnedXP;
    void Start()
    {
        for (int i = 0; i < EarnedScoreText.Length; i ++){
            switch(i){
                case 0:
                    localEarnedXP = Scoring.MainXP;
                    break;
                case 1:
                    localEarnedXP = Scoring.ArtsXP;
                    break;
                case 2:
                    localEarnedXP = Scoring.BioCheXP;
                    break;
                case 3:
                    localEarnedXP = Scoring.ComSciXP;
                    break;
                case 4:
                    localEarnedXP = Scoring.EngXP;
                    break;
                case 5:
                    localEarnedXP = Scoring.GeoXP;
                    break;
                case 6:
                    localEarnedXP = Scoring.LanXP;
                    break;
                case 7:
                    localEarnedXP = Scoring.LawPolXP;
                    break;
                case 8:
                    localEarnedXP = Scoring.PhyMathXP;
                    break;
            }
            EarnedScoreText[i].text = localEarnedXP.ToString() + "XP";   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
