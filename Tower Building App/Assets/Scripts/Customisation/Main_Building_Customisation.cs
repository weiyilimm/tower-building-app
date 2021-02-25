using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Building_Customisation : MonoBehaviour{
    public GameObject Main;

    // MAIN BUILDING CUSTOMISATION

    // Ability to change materials
    // Ability to change tower model
    // Ability to change tower height

    // > GP (all towers (4 towers with 4 models each))
    // >> P (4 models for single tower)
    // >>> C (single model)
    // >>>> GGC (single model component)

    void Start(){
            //This index can be changed to read from dictionary once codeConverter can be updated
            int index = 0;
            foreach (Transform tower in Main.transform){
                int sub_counter = 0;
                int location_counter = 0;
                foreach (Transform model in tower.transform){
                    // This statements checks if the current model is the one that should be displayed or not.
                    if (sub_counter == User_Data.data.building_stats[index].model){
                        model.gameObject.SetActive(true);
                    } else {
                        model.gameObject.SetActive(false);
                    }

                    // Get the Tower and roof model components
                    Transform Block = model.transform.Find("Tower");
                    Transform Cap = model.transform.Find("Roof");
                    
                    // Resets the starting positions of the Tower and Roof components so that they will always display correctly
                    // (100,100,100) is the standard staring scale for the main building models
                    Block.transform.localScale = new Vector3(100,100,100);
                    // For each tower you need to reset their starting positions
                    // The float values in the Vector3 declarations represent the starting local position coordinates of each tower
                    // that is - each towers relative position to the central parent object they all belong too.
                    // the values take different positive or negative values since they are at different rotational points around
                    // the centre parent object. The values are different for the fourth tower (location_counter = 3) as the model has
                    // a different origin point than the rest
                    if (location_counter == 0){
                        Block.transform.localPosition = new Vector3(0.5f,0.6f,0.5f);
                        Cap.transform.localPosition = new Vector3(0.5f,1.1f,0.5f);
                    } else if (location_counter == 1){
                        Block.transform.localPosition = new Vector3(-0.5f,0.6f,0.5f);
                        Cap.transform.localPosition = new Vector3(-0.5f,1.1f,0.5f);
                    } else if (location_counter == 2){
                        Block.transform.localPosition = new Vector3(-0.5f,0.6f,-0.5f);
                        Cap.transform.localPosition = new Vector3(-0.5f,1.1f,-0.5f);
                    } else if (location_counter == 3){
                        Block.transform.localPosition = new Vector3(0.458f,0.6f,-0.458f);
                        Cap.transform.localPosition = new Vector3(0.5f,1.1f,-0.5f);
                    }
                    location_counter += 1;

                    // Get the value for the tower height as chosen by the user in the menu
                    int height = User_Data.data.building_stats[index].m_height;
                    
                    // The scale factor is used to change the integer step heights decided by the user
                    // into a floating point number so that it better scales to the main scene proportions
                    float scale_factor = 0.7f;
                    
                    // here the height is multiplied by 100 as this represents what a standard scale increase would be
                    // and the further multiplication by the height and scale factor fits it to the main scene proportions
                    float scaledHeight = (float)(100 * (height*scale_factor));
                    var ScaleChange = new Vector3(0,0,scaledHeight);
                    
                    // Because the main building models start at scale 1.5 that needs to be accounted for
                    // when the new positions are applied to the tower and roof
                    float pos_height = (float)(1.5*(height*scale_factor));
                    var PosChange = new Vector3(0,pos_height,0);

                    Block.transform.localScale += ScaleChange;
                    Block.transform.position += (PosChange/2);
                    Cap.transform.position += PosChange;

                    // Loops through the model components applying the chosen materials to the primary and secondary slots
                    foreach (Transform model_component in model.transform){
                        //material change here
                        Material[] mats = model_component.transform.GetComponent<Renderer>().materials;
                        Material swap;

                        for (int i=0; i<mats.Length; i++){
                            if (mats[i].name.Substring(0,1) == "1"){
                                int primary = User_Data.data.building_stats[index].primary_colour;
                                if (primary != -1){
                                    swap = Instantiate(CodeConverter.codes.materials_map[User_Data.data.building_stats[index].primary_colour] as Material);
                                    swap.name = "1 " + swap.name;
                                    mats[i] = swap;
                                }
                            } else if (mats[i].name.Substring(0,1) == "2"){
                                int secondary = User_Data.data.building_stats[index].secondary_colour;
                                if (secondary != -1){
                                    swap = Instantiate(CodeConverter.codes.materials_map[User_Data.data.building_stats[index].secondary_colour] as Material);
                                    swap.name = "2 " + swap.name;
                                    mats[i] = swap;
                                }
                            }
                        }
                        model_component.transform.GetComponent<Renderer>().materials = mats;
                    }

                    sub_counter += 1;
                }

                index += 1;
            }
    }
}

