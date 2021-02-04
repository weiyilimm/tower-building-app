using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MB_Model_Switch : MonoBehaviour{
    public GameObject parent;
    void Update(){
        foreach (Transform child in parent.transform){
            int child_length = child.transform.childCount;
            for(int counter=0; counter<child_length; counter++){
                GameObject grandchild = parent.transform.GetChild(counter).gameObject;
                if (grandchild.activeSelf) {
                    User_Data.data.temp_model = counter;
                }
            }
        }
    }    
}
