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

        GetComponent<Renderer>().materials[0].color = colour_map[User_Data.data.building_stats[0].primary_colour];
        GetComponent<Renderer>().materials[1].color = colour_map[User_Data.data.building_stats[0].secondary_colour];
    }

}