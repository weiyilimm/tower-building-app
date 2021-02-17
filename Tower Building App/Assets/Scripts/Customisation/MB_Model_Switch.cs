using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MB_Model_Switch : MonoBehaviour{
    public GameObject parent;
    void Update(){
        // For each tower in the main building loop over the 4 models it has, set the temp_model variable to store the
        // id of the currently selected model. This will be sent to the persistant data store once the user clicks the 
        // confirm button to return to the main scene.
        foreach (Transform child in parent.transform){
            int child_length = child.transform.childCount;
            for(int counter=0; counter<child_length; counter++){
                GameObject grandchild = parent.transform.GetChild(counter).gameObject;
                if (grandchild.activeSelf) {
                   // User_Data.data.temp_model = counter;
                }
            }
        }
    }    
}
