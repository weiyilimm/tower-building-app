using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeHeight : MonoBehaviour
{   
    public Button ChangeHeightButton;
    public GameObject[] Roofs;
    public GameObject[] Towers;
    public Button[] MainBuildings;
    public TextMeshProUGUI BuildingHeight;
    public GameObject PopUpHeight;
    private GameObject roof;
    private GameObject tower;
    private int towerOneHeight = 0;
    private int towerTwoHeight = 0;
    private int towerThreeHeight = 0;
    private int towerFourHeight = 0;
    void Start()
    {   
        MainBuildings[0].onClick.AddListener(() => SelectBuilding(0));
        MainBuildings[1].onClick.AddListener(() => SelectBuilding(1));
        MainBuildings[2].onClick.AddListener(() => SelectBuilding(2));
        MainBuildings[3].onClick.AddListener(() => SelectBuilding(3));
        ChangeHeightButton.onClick.AddListener(() => ChangeBuildingHeight());
        
    }

    /*
    Choose which building to change height
    User have to select a building by clicking the building button in order to change height
    0 = first building, 1 = second building etc..
    */
    public void SelectBuilding(int num){
        if (num == 0){
            roof = Roofs[0];
            tower = Towers[0];
            BuildingHeight.text = towerOneHeight.ToString();
        }
        if (num == 1){
            roof = Roofs[1];
            tower = Towers[1];
            BuildingHeight.text = towerTwoHeight.ToString();
        }
        if (num == 2){
            roof = Roofs[2];
            tower = Towers[2];
            BuildingHeight.text = towerThreeHeight.ToString();
        }
        if (num == 3){
            roof = Roofs[3];
            tower = Towers[3];
            BuildingHeight.text = towerFourHeight.ToString();
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
    public void ChangeBuildingHeight(){
        /*
        To check which building has been selected and check the height
        The maximum height for the building is 7 times click 
        */
        if (roof == Roofs[0] && towerOneHeight<7){
            tower.transform.localScale += new Vector3(0,0,25);
            roof.transform.localPosition += new Vector3(0, 0.25f, 0);
            tower.transform.localPosition += new Vector3(0, 0.125f, 0);
            towerOneHeight += 1;
            BuildingHeight.text = towerOneHeight.ToString();
        }
        if (roof == Roofs[1] && towerTwoHeight<7){
            tower.transform.localScale += new Vector3(0,0,25);
            roof.transform.localPosition += new Vector3(0, 0.25f, 0);
            tower.transform.localPosition += new Vector3(0, 0.125f, 0);
            towerTwoHeight += 1;
            BuildingHeight.text = towerTwoHeight.ToString();
        }
        if (roof == Roofs[2] && towerThreeHeight<7){
            tower.transform.localScale += new Vector3(0,0,25);
            roof.transform.localPosition += new Vector3(0, 0.25f, 0);
            tower.transform.localPosition += new Vector3(0, 0.125f, 0);
            towerThreeHeight += 1;
            BuildingHeight.text = towerThreeHeight.ToString();
        }
        if (roof == Roofs[3] && towerFourHeight<7){
            tower.transform.localScale += new Vector3(0,0,25);
            roof.transform.localPosition += new Vector3(0, 0.25f, 0);
            tower.transform.localPosition += new Vector3(0, 0.125f, 0);
            towerFourHeight += 1;
            BuildingHeight.text = towerFourHeight.ToString();
        }
        if (roof == null){
            PopUpHeight.SetActive(true);
        }
    }
}
