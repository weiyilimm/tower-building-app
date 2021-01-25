using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;

[System.Serializable]
public class CodeConverter : MonoBehaviour{
    public static CodeConverter codes;
    public GameObject ConverterObject;
    public SortedDictionary<int,Material> materials_map = new SortedDictionary<int,Material>();
    public SortedDictionary<string,int> subject_map = new SortedDictionary<string,int>();

    // Start is called before the first frame update
    void Start(){
        DontDestroyOnLoad(ConverterObject);
        codes = this;
        
        //The sets of materials are loaded from the resources folder into an array. Then each element in the array is 
        // taken in turn and added to the Materials dictionary under its predecided code
        Material[] Matte = Resources.LoadAll<Material>("Materials/Matte"); 
        int counter = 0;
        foreach (Material m in Matte){
            int index = counter;
            materials_map.Add(index,m);
            counter++;
        }

        Material[] Metallic = Resources.LoadAll<Material>("Materials/Metallic");
        counter = 0;
        foreach (Material m in Metallic){
            int index = 100 + counter;
            materials_map.Add(index,m);
            counter++;
        }

        Material[] Emissive = Resources.LoadAll<Material>("Materials/Emissive");
        counter = 0;
        foreach (Material m in Emissive){
            int index = 200 + counter;
            materials_map.Add(index,m);
            counter++;
        }

        Material[] Gradient = Resources.LoadAll<Material>("Materials/Gradient");
        counter = 0;
        foreach (Material m in Gradient){
            int index = 300 + counter;
            materials_map.Add(index,m);
            counter++;
        }

        Material[] Fancy = Resources.LoadAll<Material>("Materials/Fancy");
        counter = 0;
        foreach (Material m in Fancy){
            int index = 400 + counter;
            materials_map.Add(index,m);
            counter++;
        }

        // The subjects are each given a corresponding code so that they know which part of the users
        // list of buildings they must access on read and write operations
        subject_map.Add("Main",0);
        subject_map.Add("Art",1);
        subject_map.Add("Biology&Chemistry",2);
        subject_map.Add("ComputerScience",3);
        subject_map.Add("Engineering",4);
        subject_map.Add("Geography&History",5);
        subject_map.Add("Languages",6);
        subject_map.Add("Law&Politics",7);
        subject_map.Add("Physics&Maths",8);
    }
}
