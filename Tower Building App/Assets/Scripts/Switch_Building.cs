using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch_Building : MonoBehaviour{
    public Button Original_Button, Boyd_orr_Button;
    public GameObject Original, Boyd_orr;

    // Start is called before the first frame update
    void Start(){
        Original.SetActive(true);
        Boyd_orr.SetActive(false);
        Original_Button.onClick.AddListener(original_switch);
        Boyd_orr_Button.onClick.AddListener(boyd_orr_switch);
    }

    void original_switch(){
        Original.SetActive(true);
        Boyd_orr.SetActive(false);
    }

    void boyd_orr_switch(){
        Original.SetActive(false);
        Boyd_orr.SetActive(true);
    }
}
