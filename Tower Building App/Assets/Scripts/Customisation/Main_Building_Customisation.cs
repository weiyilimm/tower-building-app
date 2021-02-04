using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Building_Customisation : MonoBehaviour{
    public GameObject Main;

    // MAIN BUILDING CUSTOMISATION NEEDS

    // Ability to change materials
    // Ability to change tower model
    // Ability to change tower height

    // > GP (all towers (4 towers with 4 models each))
    // >> P (4 models for single tower) --- LOOP HERE FOR HEIGHT AND MODEL
    // >>> C (single model) --- LOOP HERE FOR MATERIALS
    // >>>> GGC (single model component)


    // Start is called before the first frame update
    void Start(){
        
    }

    void Update(){
        if (Input.GetKeyDown("r")){
            //This index can be changed to read from dictionary once codeConverter can be updated
            int index = 0;
            foreach (Transform tower in Main.transform){
                int sub_counter = 0;
                foreach (Transform model in tower.transform){
                    //Model and Size changes here
                    if (sub_counter == User_Data.data.building_stats[index].model){
                        model.gameObject.SetActive(true);
                    } else {
                        model.gameObject.SetActive(false);
                    }

                    Transform Block = model.transform.Find("Tower");
                    Transform Cap = model.transform.Find("Roof");

                    Block.transform.localScale = new Vector3(100,100,100);
                    //Block.transform.position = new Vector3(0,0,0);
                    //Cap.transform.position = new Vector3(0,0,0);

                    int height = User_Data.data.building_stats[index].m_height;
                    var ScaleChange = new Vector3(0,0,(100*height));
                    float pos_height = (float)(1.5*height);
                    var PosChange = new Vector3(0,pos_height,0);

                    Block.transform.localScale += ScaleChange;
                    Block.transform.position += (PosChange/2);
                    Cap.transform.position += PosChange;

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
        if (Input.GetKeyDown("t")){
            foreach (Transform tower in Main.transform){
                foreach (Transform model in tower.transform){
                    change_height(model,"up");
                }
            }
        }
    }

    void change_height(Transform Stack, string direction){
        Transform Block = Stack.transform.Find("Tower");
        Transform Cap = Stack.transform.Find("Roof");
            
        if (direction == "up") {
            //do this
            if (Block.transform.localScale == new Vector3(100,100,500)){
                Debug.Log("Can't get any taller than this!");
            } else {
                var ScaleChange = new Vector3(0,0,100);
                var PosChange = new Vector3(0,1,0);
                Block.transform.localScale += ScaleChange;
                Block.transform.position += (PosChange/2);
                Cap.transform.position += PosChange;
            }
        } else if (direction == "down") {
            //do this
            if (Block.transform.localScale == new Vector3(100,100,100)){
                Debug.Log("Can't get any shorter than this!");
            } else {
                var ScaleChange = new Vector3(0,0,-100);
                var PosChange = new Vector3(0,-1,0);
                Block.transform.localScale += ScaleChange;
                Block.transform.position += (PosChange/2);
                Cap.transform.position += PosChange;
            }
        }    
    }
}

