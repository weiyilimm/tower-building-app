using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        StartCoroutine(materials_showcase());
    }

    IEnumerator materials_showcase(){
        int prim_counter = 1; //primary
        int sec_counter = 0; //secondary
        int[] mat_len = { 14, 8, 6, 7, 8 };
        while (true){
            //matte and metallic
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < mat_len[i]; j++) {
                    yield return new WaitForSeconds(1);
                    prim_counter = j + i * 100;

                    if (j + 1 < mat_len[i])
                        sec_counter = j + 1 + i * 100;
                    else
                        sec_counter = i * 100;

                    Material[] mats = GetComponent<Renderer>().materials; //object materials
                    mats[0] = CodeConverter.codes.materials_map[prim_counter];
                    mats[1] = CodeConverter.codes.materials_map[sec_counter];
                    GetComponent<Renderer>().materials = mats;
                }
            }
            //emmisive
            for (int j = 0; j < mat_len[2]; j++)
            {
                yield return new WaitForSeconds(1);

                    sec_counter = j + 200;

                Material[] mats = GetComponent<Renderer>().materials; //object materials
                mats[0] = CodeConverter.codes.materials_map[0];
                mats[1] = CodeConverter.codes.materials_map[sec_counter];
                GetComponent<Renderer>().materials = mats;
            }
            //gradients
            for (int j = 0; j < mat_len[3]; j++)
            {
                yield return new WaitForSeconds(1);
                prim_counter = j + 300;

                if (j + 1 < mat_len[3])
                    sec_counter = j + 301;
                else
                    sec_counter = 300;

                Material[] mats = GetComponent<Renderer>().materials; //object materials
                mats[0] = CodeConverter.codes.materials_map[prim_counter];
                mats[1] = CodeConverter.codes.materials_map[sec_counter];
                GetComponent<Renderer>().materials = mats;
            }

            //fancy
            yield return new WaitForSeconds(1);
            for (int j = 0; j < mat_len[4]; j++)
            { 
                prim_counter = j + 400;

                Material[] mats = GetComponent<Renderer>().materials; //object materials
                mats[0] = CodeConverter.codes.materials_map[prim_counter];
                mats[1] = CodeConverter.codes.materials_map[prim_counter];
                GetComponent<Renderer>().materials = mats;

                yield return new WaitForSeconds(5);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
