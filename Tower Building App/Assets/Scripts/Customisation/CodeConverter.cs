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
    public SortedDictionary<int, string> buildingName_map = new SortedDictionary<int,string>();

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
        subject_map.Add("Main2",1);
        subject_map.Add("Main3",2);
        subject_map.Add("Main4",3);
        subject_map.Add("PhyMath",4);
        subject_map.Add("ComSci",5);
        subject_map.Add("BioChe",6);
        subject_map.Add("Geo",7);
        subject_map.Add("Lan",8);
        subject_map.Add("Eng",9);
        subject_map.Add("LawPol",10);
        subject_map.Add("Arts",11);

        // Need a dictionary to map the a known building code to the names of each individual model
        buildingName_map.Add(0,"Main1A");
        buildingName_map.Add(1,"Main1B");
        buildingName_map.Add(2,"Main1C");
        buildingName_map.Add(3,"Main1D");
        buildingName_map.Add(10,"Main2A");
        buildingName_map.Add(11,"Main2B");
        buildingName_map.Add(12,"Main2C");
        buildingName_map.Add(13,"Main2D");
        buildingName_map.Add(20,"Main3A");
        buildingName_map.Add(21,"Main3B");
        buildingName_map.Add(22,"Main3C");
        buildingName_map.Add(23,"Main3D");
        buildingName_map.Add(30,"Main4A");
        buildingName_map.Add(31,"Main4B");
        buildingName_map.Add(32,"Main4C");
        buildingName_map.Add(33,"Main4D");
        buildingName_map.Add(40,"Observatory");
        buildingName_map.Add(41,"Basic Shapes");
        buildingName_map.Add(42,"Glitch Cube");
        buildingName_map.Add(43,"Space Shuttle");
        buildingName_map.Add(50,"UNK COMPSCI");
        buildingName_map.Add(51,"Sci-Fi Building");
        buildingName_map.Add(52,"PC Tower");
        buildingName_map.Add(60,"Helix Building");
        buildingName_map.Add(61,"DNA Building");
        buildingName_map.Add(62,"Microscope");
        buildingName_map.Add(70,"Parthenon");
        buildingName_map.Add(71,"Parthenon - Destroyed");
        buildingName_map.Add(72,"Globe");
        buildingName_map.Add(73,"Temple");
        buildingName_map.Add(80,"Eiffel Tower");
        buildingName_map.Add(81,"Pisa");
        buildingName_map.Add(82,"Pagoda");
        buildingName_map.Add(90,"Crane");
        buildingName_map.Add(91,"Burj Khalifa");
        buildingName_map.Add(92,"Steampunk Big Ben");
        buildingName_map.Add(100,"Court House");
        buildingName_map.Add(101,"MI6");
        buildingName_map.Add(102,"Chess Set");
        buildingName_map.Add(110,"UNK ART 1");
        buildingName_map.Add(111,"Louvre");
        buildingName_map.Add(112,"UNK ART 2");
    }
}
