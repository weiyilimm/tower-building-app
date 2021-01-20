using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Customisation : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        int index = CodeConverter.codes.subject_map[transform.name];
        int model = User_Data.data.building_stats[index].model;
        int counter = 0;

        foreach (Transform child in transform){
            Material[] mats = child.transform.GetComponent<Renderer>().materials;
            int primary = User_Data.data.building_stats[index].primary_colour;
            int secondary = User_Data.data.building_stats[index].secondary_colour;
            if (primary != 0){
                mats[0] = CodeConverter.codes.materials_map[User_Data.data.building_stats[index].primary_colour];
            }
            if (secondary != 0){
                mats[1] = CodeConverter.codes.materials_map[User_Data.data.building_stats[index].secondary_colour];
            }
            child.transform.GetComponent<Renderer>().materials = mats;

            if (counter == model){
                transform.GetChild(counter).transform.gameObject.SetActive(true);
            } else {
                transform.GetChild(counter).transform.gameObject.SetActive(false);
            }

            counter += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
