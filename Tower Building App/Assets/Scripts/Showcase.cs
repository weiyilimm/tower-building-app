using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase : MonoBehaviour{
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
            Material[] mats = GetComponent<Renderer>().materials;
            mats[0] = CodeConverter.codes.materials_map[prim_counter];
            mats[1] = CodeConverter.codes.materials_map[sec_counter];
            GetComponent<Renderer>().materials = mats;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
