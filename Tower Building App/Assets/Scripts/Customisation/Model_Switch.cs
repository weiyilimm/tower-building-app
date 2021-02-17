using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Model_Switch : MonoBehaviour{
    public GameObject parent;
    
    // Finds the number of child objects (buildings for the subject) then loops through them to see which is active
    // The building that is active is set as the users temporary choice which will them be confirmed once the user clicks the 
    // confirm button
    void Update(){
        int parent_length = parent.transform.childCount;
        for(int counter=0; counter<parent_length; counter++){
            GameObject child = parent.transform.GetChild(counter).gameObject;
            if (child.activeSelf) {
                User_Data.data.temp_data[0][2] = counter;
            }
        }
    }    
}
