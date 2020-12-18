using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class temp_scene_switch : MonoBehaviour{
    public Button customise;
    // Start is called before the first frame update
    void Start(){
        customise.onClick.AddListener(Load_customise);
    }

    void Load_customise(){
        SceneManager.LoadScene(sceneName:"Main_Building_Customisation");   
    }
}
