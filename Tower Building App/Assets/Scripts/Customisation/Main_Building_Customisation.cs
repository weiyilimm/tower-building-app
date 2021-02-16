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
                    Block.transform.localScale = new Vector3(100,100,100);
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

                    // Get and apply the height changes to the tower and roof components
                    int height = User_Data.data.building_stats[index].m_height;
                    var ScaleChange = new Vector3(0,0,(100*height));
                    float pos_height = (float)(1.5*height);
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

    // This function will probably be pulled into a seperate script once the UI for the MB has been implemented
    void change_height(Transform Stack, string direction){
        Transform Block = Stack.transform.Find("Tower");
        Transform Cap = Stack.transform.Find("Roof");
            
        if (direction == "up") {
            if (Block.transform.localScale == new Vector3(100,100,500)){
                //These messages will change to a pop up message once we are able to do so
                Debug.Log("Can't get any taller than this!");
            } else {
                var ScaleChange = new Vector3(0,0,100);
                var PosChange = new Vector3(0,1.5f,0);
                Block.transform.localScale += ScaleChange;
                Block.transform.position += (PosChange/2);
                Cap.transform.position += PosChange;
            }
        } else if (direction == "down") {
            if (Block.transform.localScale == new Vector3(100,100,100)){
                Debug.Log("Can't get any shorter than this!");
            } else {
                var ScaleChange = new Vector3(0,0,-100);
                var PosChange = new Vector3(0,-1.5f,0);
                Block.transform.localScale += ScaleChange;
                Block.transform.position += (PosChange/2);
                Cap.transform.position += PosChange;
            }
        }    
    }
}

