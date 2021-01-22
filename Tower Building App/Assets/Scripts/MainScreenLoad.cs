using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenLoad : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        
        int index = CodeConverter.codes.subject_map[transform.name];

        foreach (Transform child in transform){
            int length  = child.GetComponent<Renderer>().materials.Length;
            if (length == 1){
                child.GetComponent<MeshRenderer>().material = CodeConverter.codes.materials_map[User_Data.data.building_stats[index].primary_colour];
            }else{
                Material[] mats = child.GetComponent<Renderer>().materials;
                mats[0] = CodeConverter.codes.materials_map[User_Data.data.building_stats[index].primary_colour];
                mats[1] = CodeConverter.codes.materials_map[User_Data.data.building_stats[index].secondary_colour];
                child.GetComponent<Renderer>().materials = mats;
            }
        }
    }
}