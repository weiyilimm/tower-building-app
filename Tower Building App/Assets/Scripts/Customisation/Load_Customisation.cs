using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Customisation : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        int index = CodeConverter.codes.subject_map[transform.name];
        int model = User_Data.data.building_stats[index].model;
        int counter = 0;

        //The empty game object for the subject contains each of the models possible to display hence the
        // following loop must access each of them and update them all.
        foreach (Transform child in transform){
            Material[] mats = child.transform.GetComponent<Renderer>().materials;
            Material swap;

            //Loops through the materials array for the given model, If the material designated as the primary is found
            // then the users choice of primary material is applied, similarly if the secondary material is found
            // then the users choice of secondary material is applied
            for (int i=0; i<mats.Length; i++){
                if (mats[i].name.Substring(0,1) == "1"){
                    int primary = User_Data.data.building_stats[index].primary_colour;
                    if (primary != 0){
                        swap = Instantiate(CodeConverter.codes.materials_map[User_Data.data.building_stats[index].primary_colour] as Material);
                        swap.name = "1 " + swap.name;
                        mats[i] = swap;
                    }
                } else if (mats[i].name.Substring(0,1) == "2"){
                    int secondary = User_Data.data.building_stats[index].secondary_colour;
                    if (secondary != 0){
                        swap = Instantiate(CodeConverter.codes.materials_map[User_Data.data.building_stats[index].secondary_colour] as Material);
                        swap.name = "2 " + swap.name;
                        mats[i] = swap;
                    }
                }
            }
            
            child.transform.GetComponent<Renderer>().materials = mats;

            //Checks to see if the current model in the loop is the one that should be displayed or not
            if (counter == model){
                transform.GetChild(counter).transform.gameObject.SetActive(true);
            } else {
                transform.GetChild(counter).transform.gameObject.SetActive(false);
            }

            counter += 1;
        }
    }
}
