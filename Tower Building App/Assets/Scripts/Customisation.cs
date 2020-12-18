using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Customisation : MonoBehaviour{
    public Button Back_Button, Confirm_Button, Primary_Blue, Primary_Green, Primary_Purple, Primary_Red, Primary_Yellow;
    public Button Secondary_Blue, Secondary_Green, Secondary_Purple, Secondary_Red, Secondary_Yellow, Model_1, Model_2, Model_3;
    public TMP_Text Building_Name, Building_XP;
    public string Building_Name_string;
    public int Building_XP_int, temp_Primary, temp_Secondary, temp_ModelCode;

    // Start is called before the first frame update
    void Start(){
        temp_Primary = 0;
        temp_Secondary = 0;
        temp_ModelCode = 0;
        
        //Building_XP_int = 0;
        //Building_XP.text = Building_XP_int.ToString();
        
        // Creates events for each colour of button, the numbers represent the type (primary or secondary) and then the 
        // colour itself. More numbers could be added to represent material type ect.
        Primary_Blue.onClick.AddListener(() => change_colour(0,1));
        Primary_Green.onClick.AddListener(() => change_colour(0,2));
        Primary_Purple.onClick.AddListener(() => change_colour(0,3));
        Primary_Red.onClick.AddListener(() => change_colour(0,4));
        Primary_Yellow.onClick.AddListener(() => change_colour(0,5));

        Secondary_Blue.onClick.AddListener(() => change_colour(1,1));
        Secondary_Green.onClick.AddListener(() => change_colour(1,2));
        Secondary_Purple.onClick.AddListener(() => change_colour(1,3));
        Secondary_Red.onClick.AddListener(() => change_colour(1,4));
        Secondary_Yellow.onClick.AddListener(() => change_colour(1,5));

        Model_1.onClick.AddListener(() => change_model(1));
        Model_2.onClick.AddListener(() => change_model(2));
        Model_3.onClick.AddListener(() => change_model(3));

        Confirm_Button.onClick.AddListener(Confirm);
        Back_Button.onClick.AddListener(Back);
    }

    void change_colour(int ColourType, int ColourCode){
        //Update the temp User_Data here - 0 means primary, 1 means secondary
        if (ColourType == 0){
            temp_Primary = ColourCode;
        } else {
            temp_Secondary = ColourCode;
        }
    }

    void change_model(int ModelCode){
        temp_ModelCode = ModelCode;
    }

    void Confirm(){
        //Update the user data and return to main screen
        User_Data.data.building_stats[0].primary_colour = temp_Primary;
        User_Data.data.building_stats[0].secondary_colour = temp_Secondary;
        User_Data.data.building_stats[0].model = temp_ModelCode;
        
        SceneManager.LoadScene(sceneName:"MainScene");
    }

    void Back(){
        //return to the main screen without updating
        SceneManager.LoadScene(sceneName:"MainScene");
    }

    // Update is called once per frame
    void Update(){
    
    }
}
