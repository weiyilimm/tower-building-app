using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Customisation : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        int index = CodeConverter.codes.subject_map[transform.name];

        foreach (Transform child in transform){
                Material[] mats = child.GetComponent<Renderer>().materials;
                int primary = User_Data.data.building_stats[index].primary_colour;
                int secondary = User_Data.data.building_stats[index].secondary_colour;
                if (primary != 0){
                    mats[0] = CodeConverter.codes.materials_map[User_Data.data.building_stats[index].primary_colour];
                }
                if (secondary != 0){
                    mats[1] = CodeConverter.codes.materials_map[User_Data.data.building_stats[index].secondary_colour];
                }
                child.GetComponent<Renderer>().materials = mats;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
