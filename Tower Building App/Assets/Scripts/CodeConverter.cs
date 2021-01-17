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
        
        //Test code for loading all of a type in a single command then looping to add
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
        foreach (Material m in Fancy)
        {
            int index = 400 + counter;
            materials_map.Add(index, m);
            counter++;
        }




        // Adds all of the buildings in the app to a lookup dictionary
        // NOTE - once we have a finalised list of buildings we should be able to change the code to use a loop
        //  until then the static form provides a slightly easier and more flexible approach for testing
        subject_map.Add("Main Building",0);
        subject_map.Add("Biology Building",1);
        subject_map.Add("Computer Science Building",2);
        subject_map.Add("Geography Building",3);
        subject_map.Add("History Building",4);
    }
}
