using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UnlockBuilding : MonoBehaviour
{   
    //3D Buildings to be unlocked
    public GameObject[] Buildings;
    //Buttons to store each building picture object
    public Button[] Buttons;
    //Get the specific building XP by accessing Scoring.cs 
    private double localXP;
    //90000XP required to unlock the next building
    private int BuildingXP = 90000;
    //lockIcon which overlay each button to indicate user that it hasn't been unlocked
    private GameObject lockIcon;
    //Get the current scene that user in
    private string currentSceneName;

    public TextMeshProUGUI XPText;
    void Start()
    {   
        //Get the current scene name that user in
        currentSceneName = SceneManager.GetActiveScene().name;
        //Assign the localXP to be specific building's XP
        switch (currentSceneName)
        {
            case "Main":
                localXP = Scoring.MainXP;
                break;
            case "Arts":
                localXP = Scoring.ArtsXP;
                break;
            case "BioChe":
                localXP = Scoring.BioCheXP;
                break;
            case "ComSci":
                localXP = Scoring.ComSciXP;
                break;
            case "Eng":
                localXP = Scoring.EngXP;
                break;
            case "Geo":
                localXP = Scoring.GeoXP;
                break;
            case "Lan":
                localXP = Scoring.LanXP;
                break;
            case "LawPol":
                localXP = Scoring.LawPolXP;
                break;
            case "PhyMath":
                localXP = Scoring.PhyMathXP;
                break;
        }

        XPText.text = localXP.ToString() + "XP";

        for (int i = 0; i<Buttons.Length; i++){
            /*
            XP required to unlock the building
            First building = 0
            Second building = 90000
            Third building = 180000
            */
            if (localXP >= i * BuildingXP){
                //Get the lock Icon from the button's child
                lockIcon = Buttons[i].transform.GetChild(0).gameObject;
                lockIcon.SetActive(false);
                for (int j = 0; j<Buildings.Length; j++)
                {
                    //We need assign new varibale for add listener to work
                    //Otherwise we will get indexoutofrange error
                    var x = j;
                    /* 
                    Make the correct 3d Building visible, other not visible
                    For example,
                    if the first button (i = 0) is clicked, then the first building (x = 0) will be set visible
                    */
                    if(i == x)
                    {
                        Buttons[i].onClick.AddListener(() => Buildings[x].SetActive(true));
                    }
                    else
                    {
                        Buttons[i].onClick.AddListener(() => Buildings[x].SetActive(false));
                    }
                }
            }
        }
    }
}
