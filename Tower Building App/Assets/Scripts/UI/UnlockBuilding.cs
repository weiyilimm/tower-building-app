using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockBuilding : MonoBehaviour
{
    public GameObject[] buildings;
    private double XP;
    private Button button;
    private int BuildingXP = 2000;
    private GameObject lockIcon;

    void Start()
    {   
        XP = StopWatch.GlobalXP;
        button = this.GetComponent<Button>();
        lockIcon = this.transform.GetChild(0).gameObject;
        for (int i = 0; i<buildings.Length; i++){
            /*
            XP required to unlock the building
            First building = 0
            Second building = 500
            Third building = 1000
            */
            if (XP >= i * BuildingXP){
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
