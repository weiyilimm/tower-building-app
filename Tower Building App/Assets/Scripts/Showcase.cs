using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        StartCoroutine(materials_showcase());
    }

    IEnumerator materials_showcase(){
        int prim_counter = 1;
        int sec_counter = 0;
        while (true){
            yield return new WaitForSeconds(2);
            prim_counter += 1;
            sec_counter += 1;
            if (prim_counter == 14) {prim_counter = 0;}
            if (sec_counter == 14) {sec_counter = 0;}
            foreach (Transform child in transform){
                int length  = child.GetComponent<Renderer>().materials.Length;
                if (length == 1){
                    string mat_name = child.GetComponent<MeshRenderer>().material.name;
                    if (mat_name.Substring(0,1) == "0" || mat_name.Substring(0,1) == "1"){
                        child.GetComponent<MeshRenderer>().material = CodeConverter.codes.materials_map[prim_counter];
                    } else if (mat_name.Substring(0,1) == "2"){
                        child.GetComponent<MeshRenderer>().material = CodeConverter.codes.materials_map[sec_counter];
                    }
                }else{
                    Material[] mats = child.GetComponent<Renderer>().materials;
                    string mat1_name = mats[0].name;
                    string mat2_name = mats[1].name;
                    if (mat1_name.Substring(0,1) == "0" || mat1_name.Substring(0,1) == "1" || mat1_name.Substring(0,1) == "2"){
                        mats[0] = CodeConverter.codes.materials_map[sec_counter];
                    }
                    //mats[1] = CodeConverter.codes.materials_map[counter];
                    if (mat2_name.Substring(0,1) == "0" || mat2_name.Substring(0,1) == "1" || mat2_name.Substring(0,1) == "2"){
                        mats[1] = CodeConverter.codes.materials_map[prim_counter];
                    }
                    child.GetComponent<Renderer>().materials = mats;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
