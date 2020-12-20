using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenLoad : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        SortedDictionary<int,Color> colour_map = new SortedDictionary<int,Color>();
        colour_map.Add(1,Color.blue);
        colour_map.Add(2,Color.green);
        colour_map.Add(3,Color.magenta);
        colour_map.Add(4,Color.red);
        colour_map.Add(5,Color.yellow);
        
        SortedDictionary<string,int> subject_map = new SortedDictionary<string,int>();
        subject_map.Add("Main Building",0);
        subject_map.Add("Biology Building",1);
        subject_map.Add("Computer Science Building",2);
        subject_map.Add("Geography Building",3);
        subject_map.Add("History Building",4);
        
        int index = subject_map[transform.name];

        foreach (Transform child in transform){
            int length  = child.GetComponent<Renderer>().materials.Length;
            for (int i=0; i<length; i++){
                if (i == 0) {
                    child.GetComponent<Renderer>().materials[0].color = colour_map[User_Data.data.building_stats[index].primary_colour];
                } else {
                    child.GetComponent<Renderer>().materials[1].color = colour_map[User_Data.data.building_stats[index].secondary_colour];
                }
            }
        }

        // OLD WORKING CODE
        // GetComponent<Renderer>().materials[0].color = colour_map[User_Data.data.building_stats[index].primary_colour];
        // GetComponent<Renderer>().materials[1].color = colour_map[User_Data.data.building_stats[index].secondary_colour];
    }

}