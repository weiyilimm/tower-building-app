using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnlockBuilding : MonoBehaviour
{
    public GameObject[] buildings;
    private double globalXP;
    private double localXP;
    private Button button;
    private int BuildingXP = 2000;
    private GameObject lockIcon;
    private string currentSceneName;
    void Start()
    {   
        currentSceneName = SceneManager.GetActiveScene().name;
        switch (currentSceneName){
            case "Main":
                localXP = StopWatch.MainXP;
                break;
            case "Arts":
                localXP = StopWatch.ArtsXP;
                break;
            case "BioChe":
                localXP = StopWatch.BioCheXP;
                break;
            case "ComSci":
                localXP = StopWatch.ComSciXP;
                break;
            case "Eng":
                localXP = StopWatch.EngXP;
                break;
            case "Geo":
                localXP = StopWatch.GeoXP;
                break;
            case "Lan":
                localXP = StopWatch.LanXP;
                break;
            case "LawPol":
                localXP = StopWatch.LawPolXP;
                break;
            case "PhyMath":
                localXP = StopWatch.PhyMathXP;
                break;
        }
        globalXP = StopWatch.GlobalXP;
        button = this.GetComponent<Button>();
        lockIcon = this.transform.GetChild(0).gameObject;
        for (int i = 0; i<buildings.Length; i++){
            /*
            XP required to unlock the building
            First building = 0
            Second building = 500
            Third building = 1000
            */
            if ((globalXP >= i * BuildingXP) || (localXP >= i * BuildingXP)){
                if (button.name[0].ToString() == i.ToString()){
                    lockIcon.SetActive(false);
                    for (int j = 0; j<buildings.Length; j++){
                        //We need assign new varibale for add listener to work
                        //Otherwise we will get indexoutofrange error
                        var x = j;
                        if(i == x){
                            button.onClick.AddListener(() => buildings[x].SetActive(true));
                        }
                        else{
                            button.onClick.AddListener(() => buildings[x].SetActive(false));
                        }
                    }
                }
            }
        }
    }
}
