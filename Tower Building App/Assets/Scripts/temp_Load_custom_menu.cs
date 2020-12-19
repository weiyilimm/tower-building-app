using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class temp_Load_custom_menu : MonoBehaviour{
    public Button Back_Button, Main, Bio, Comp, Geo, Hist;
    // Start is called before the first frame update
    void Start(){
        Back_Button.onClick.AddListener(() => Load_Scene("MainScene"));
        Main.onClick.AddListener(() => Load_Scene("Main_Building"));
        Bio.onClick.AddListener(() => Load_Scene("Biology_Building"));
        Comp.onClick.AddListener(() => Load_Scene("Comp_Sci_Building"));
        Geo.onClick.AddListener(() => Load_Scene("Geography_Building"));
        Hist.onClick.AddListener(() => Load_Scene("History_Building"));        
    }

    void Load_Scene(string target_scene){
        SceneManager.LoadScene(sceneName:target_scene);  
    }

    // Update is called once per frame
    void Update(){
        
    }
}
