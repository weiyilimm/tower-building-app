using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeHeight : MonoBehaviour
{   
    /*
    Increase building Height Button
    It will increase all individual shapes in building 
    But only the one that been selected by user will be shown, others will be hiden
    */
    public Button IncreaseHeightButton;
    //Decrease building Height Button
    public Button DecreaseHeightButton;
    /*
    Each individual building roofs and towers consist of multiple shapes
    E.G. BuildingOneRoofs array is storing rectangle roof, octagon roof
    */
    public GameObject[] BuildingOneRoofs;
    public GameObject[] BuildingOneTowers;
    public GameObject[] BuildingTwoRoofs;
    public GameObject[] BuildingTwoTowers;
    public GameObject[] BuildingThreeRoofs;
    public GameObject[] BuildingThreeTowers;
    public GameObject[] BuildingFourRoofs;
    public GameObject[] BuildingFourTowers;
    /*
    The buildings can be customized from materials, heights, and shapes
    Total of 4 buildings
    */
    public Button[] MainBuildings;
    /*
    The building shapes
    Currently has 4 shapes
    */
    public Button[] BuildingShapes;
    //A text to indidate how tall the building is as the button pressed count increases
    public TextMeshProUGUI BuildingHeight;
    //A pop up to indicate when the user hasn't selected a building
    public GameObject PopUpHeight;
    //A text from pop up
    public TextMeshProUGUI PopUpText;
    //Maximum height of the building
    private int maximumHeight = 15;
    /*
    A counter to count each individual building height
    Maximum height is 7 times button pressed
    */
    private int buildingOneHeight = 0;
    private int buildingTwoHeight = 0;
    private int buildingThreeHeight = 0;
    private int buildingFourHeight = 0;
    /*
    An integer to store which building been selected
    E.G. if the user select first building(Top right in the UI), then it is 0
    */
    private int buildingDecider = -1;
    void Start()
    {   
        ApplyAtStart();
        buildingDecider = 1;
        BuildingHeight.text = buildingTwoHeight.ToString();
        //When the user select a building, it triggger the SelectBuilding function
        MainBuildings[0].onClick.AddListener(() => SelectBuilding(0));
        MainBuildings[1].onClick.AddListener(() => SelectBuilding(1));
        MainBuildings[2].onClick.AddListener(() => SelectBuilding(2));
        MainBuildings[3].onClick.AddListener(() => SelectBuilding(3));
        //When the user select a shape for a building, it triggger the SelectShape function
        BuildingShapes[0].onClick.AddListener(() => SelectShape(0));
        BuildingShapes[1].onClick.AddListener(() => SelectShape(1));
        BuildingShapes[2].onClick.AddListener(() => SelectShape(2));
        BuildingShapes[3].onClick.AddListener(() => SelectShape(3));
        IncreaseHeightButton.onClick.AddListener(() => IncreaseBuildingHeight());
        DecreaseHeightButton.onClick.AddListener(() => DecreseBuildingHeight());
    }

    /*
    Choose which building to change materials, heights and shapes
    User have to select a building by clicking the building button in order to change height
    i parameter is to indicate which button been pressed
    E.G. 0 = first building, 1 = second building etc..
    */
    public void SelectBuilding(int num){
        if (num == 0){
            buildingDecider = num;
            BuildingHeight.text = buildingOneHeight.ToString();
        }
        if (num == 1){
            buildingDecider = num;
            BuildingHeight.text = buildingTwoHeight.ToString();
        }
        if (num == 2){
            buildingDecider = num;
            BuildingHeight.text = buildingThreeHeight.ToString();
        }
        if (num == 3){
            buildingDecider = num;
            BuildingHeight.text = buildingFourHeight.ToString();
        }
    }

    /*
    Select a shape for a building
    i parameter is to indicate which shape button been pressed for a building
    E.G. 0 = first shape in the UI models, 1 = second shape in the UI models
    */
    public void SelectShape(int i){
        //Use the temporary stored integer to get which building been selected
        User_Data.data.temp_data[buildingDecider][2] = i;
        if (buildingDecider == 0){
            for (int j = 0; j<4; j++){
                //Show the specific shape of building that user select
                if (j==i){
                    BuildingOneRoofs[j].SetActive(true);
                    BuildingOneTowers[j].SetActive(true);
                }
                //Hide all the other building 
                else{
                    BuildingOneRoofs[j].SetActive(false);
                    BuildingOneTowers[j].SetActive(false);
                } 
            }
        }
        if (buildingDecider == 1){
            for (int j = 0; j<4; j++){
                //Show the specific shape of building that user select
                if (j==i){
                    BuildingTwoRoofs[j].SetActive(true);
                    BuildingTwoTowers[j].SetActive(true);
                }
                //Hide all the other building 
                else{
                    BuildingTwoRoofs[j].SetActive(false);
                    BuildingTwoTowers[j].SetActive(false);
                } 
            }
        }
        if (buildingDecider == 2){
            for (int j = 0; j<4; j++){
                //Show the specific shape of building that user select
                if (j==i){
                    BuildingThreeRoofs[j].SetActive(true);
                    BuildingThreeTowers[j].SetActive(true);
                }
                //Hide all the other building 
                else{
                    BuildingThreeRoofs[j].SetActive(false);
                    BuildingThreeTowers[j].SetActive(false);
                } 
            }
        }
        if (buildingDecider == 3){
            for (int j = 0; j<4; j++){
                //Show the specific shape of building that user select
                if (j==i){
                    BuildingFourRoofs[j].SetActive(true);
                    BuildingFourTowers[j].SetActive(true);
                }
                //Hide all the other building 
                else{
                    BuildingFourRoofs[j].SetActive(false);
                    BuildingFourTowers[j].SetActive(false);
                } 
            }
        }
        if (buildingDecider == -1){
            PopUpText.text = "Please select a building to change shape.";
            PopUpHeight.SetActive(true);
        }
    }
    
    /*
    Change building height feature
    When user click the button once
    The tower z scale (height) +100
    The tower y position +0.5 to match the base
    The roof y position +1 to match the top 
    The ratio of towerScale:roofPosition:towerPosition is 50:0.5:0.25
    */
    public void IncreaseBuildingHeight(){
        /*
        To check which building has been selected and check the height
        The maximum height for the building is 7 times click 
        */
        if (buildingDecider == 0 && buildingOneHeight<maximumHeight){
            //Increase height of each individual shape of buildings 
            for(int i = 0; i<4; i++){
                BuildingOneTowers[i].transform.localScale += new Vector3(0,0,25);
                BuildingOneRoofs[i].transform.localPosition += new Vector3(0, 0.25f, 0);
                BuildingOneTowers[i].transform.localPosition += new Vector3(0, 0.125f, 0);
            }
            buildingOneHeight += 1;
            BuildingHeight.text = buildingOneHeight.ToString();
        }
        if (buildingDecider == 1 && buildingTwoHeight<maximumHeight){
            for(int i = 0; i<4; i++){
                BuildingTwoTowers[i].transform.localScale += new Vector3(0,0,25);
                BuildingTwoRoofs[i].transform.localPosition += new Vector3(0, 0.25f, 0);
                BuildingTwoTowers[i].transform.localPosition += new Vector3(0, 0.125f, 0);
            }
            buildingTwoHeight += 1;
            BuildingHeight.text = buildingTwoHeight.ToString();
        }
        if (buildingDecider == 2 && buildingThreeHeight<maximumHeight){
            for(int i = 0; i<4; i++){
                BuildingThreeTowers[i].transform.localScale += new Vector3(0,0,25);
                BuildingThreeRoofs[i].transform.localPosition += new Vector3(0, 0.25f, 0);
                BuildingThreeTowers[i].transform.localPosition += new Vector3(0, 0.125f, 0);
            }
            buildingThreeHeight += 1;
            BuildingHeight.text = buildingThreeHeight.ToString();
        }
        if (buildingDecider == 3 && buildingFourHeight<maximumHeight){
            for(int i = 0; i<4; i++){
                BuildingFourTowers[i].transform.localScale += new Vector3(0,0,25);
                BuildingFourRoofs[i].transform.localPosition += new Vector3(0, 0.25f, 0);
                BuildingFourTowers[i].transform.localPosition += new Vector3(0, 0.125f, 0);
            }
            buildingFourHeight += 1;
            BuildingHeight.text = buildingFourHeight.ToString();
        }

        // Check the height of the building is below the maximum and if so increase its height
        if (User_Data.data.temp_data[buildingDecider][3] < maximumHeight){
            User_Data.data.temp_data[buildingDecider][3] += 1;
        }

        //If the building hasn't been selected, pop up an instructions to indicate user need to select a building
        if (buildingDecider == -1){
            PopUpText.text = "Please select a building to change height.";
            PopUpHeight.SetActive(true);
        }
    }

    /*
    Same method as IncreaseBuildingHeight
    Just use -= instead of +=
    */
    public void DecreseBuildingHeight(){
        /*
        To check which building has been selected and check the height
        The minimum height for the building is 0 times click 
        */
        
        if (buildingDecider == 0 && buildingOneHeight>0){
            for(int i = 0; i<4; i++){
                BuildingOneTowers[i].transform.localScale -= new Vector3(0,0,25);
                BuildingOneRoofs[i].transform.localPosition -= new Vector3(0, 0.25f, 0);
                BuildingOneTowers[i].transform.localPosition -= new Vector3(0, 0.125f, 0);
            }
            buildingOneHeight -= 1;
            BuildingHeight.text = buildingOneHeight.ToString();
        }
        if (buildingDecider == 1 && buildingTwoHeight>0){
            for(int i = 0; i<4; i++){
                BuildingTwoTowers[i].transform.localScale -= new Vector3(0,0,25);
                BuildingTwoRoofs[i].transform.localPosition -= new Vector3(0, 0.25f, 0);
                BuildingTwoTowers[i].transform.localPosition -= new Vector3(0, 0.125f, 0);
            }
            buildingTwoHeight -= 1;
            BuildingHeight.text = buildingTwoHeight.ToString();
        }
        if (buildingDecider == 2 && buildingThreeHeight>0){
            for(int i = 0; i<4; i++){
                BuildingThreeTowers[i].transform.localScale -= new Vector3(0,0,25);
                BuildingThreeRoofs[i].transform.localPosition -= new Vector3(0, 0.25f, 0);
                BuildingThreeTowers[i].transform.localPosition -= new Vector3(0, 0.125f, 0);
            }
            buildingThreeHeight -= 1;
            BuildingHeight.text = buildingThreeHeight.ToString();
        }
        if (buildingDecider == 3 && buildingFourHeight>0){
            for(int i = 0; i<4; i++){
                BuildingFourTowers[i].transform.localScale -= new Vector3(0,0,25);
                BuildingFourRoofs[i].transform.localPosition -= new Vector3(0, 0.25f, 0);
                BuildingFourTowers[i].transform.localPosition -= new Vector3(0, 0.125f, 0);
            }
            buildingFourHeight -= 1;
            BuildingHeight.text = buildingFourHeight.ToString();
        }

        // Check the height of the building is above the minimum and if so decrease its height
        if (User_Data.data.temp_data[buildingDecider][3] > 0){
            User_Data.data.temp_data[buildingDecider][3] -= 1;
        }

        if (buildingDecider == -1){
            PopUpText.text = "Please select a building to change height.";
            PopUpHeight.SetActive(true);
        }
    }

    public void ApplyAtStart() {
        for (int i=0; i<4; i++) {
            buildingDecider = i;
            User_Data.data.temp_data[i][2] = User_Data.data.building_stats[i].m_height;
            for (int j=0; j<User_Data.data.temp_data[i][2]; j++) {
                IncreaseBuildingHeight();
            }
        }
        buildingDecider = -1;
    }
}
