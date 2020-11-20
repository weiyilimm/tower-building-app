using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.P)){
            if (SceneManager.GetActiveScene().name == "PreFab_Building"){
                SceneManager.LoadScene(sceneName:"Custom_Building");
            } else if (SceneManager.GetActiveScene().name == "Custom_Building"){
                SceneManager.LoadScene(sceneName:"PreFab_Building");
            };
        };
    }
}
