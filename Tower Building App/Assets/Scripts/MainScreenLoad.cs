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
            // for (int i=0; i<length; i++){
            //    if (i == 0) {
            //        child.GetComponent<MeshRenderer>().materials[0] = CodeConverter.codes.materials_map[User_Data.data.building_stats[index].primary_colour];
            //    } else {
            //        child.GetComponent<MeshRenderer>().materials[1] = CodeConverter.codes.materials_map[User_Data.data.building_stats[index].secondary_colour];
            //    }
            //}
        }

        // OLD WORKING CODE
        // GetComponent<Renderer>().materials[0].color = colour_map[User_Data.data.building_stats[index].primary_colour];
        // GetComponent<Renderer>().materials[1].color = colour_map[User_Data.data.building_stats[index].secondary_colour];
    }

}