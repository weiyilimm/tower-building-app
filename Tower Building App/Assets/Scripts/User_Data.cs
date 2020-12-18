using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;

[System.Serializable]
public class User_Data : MonoBehaviour{
    public static User_Data data;
    public GameObject UserProfile;
    public string Username,json;
    public int global_xp;
    public List<Building> building_stats = new List<Building>();
    
    //public Button file1,file2;
    
    //This script will store all of the data assigned to a single user
    // It will contain the object it travels thorugh the scenes on (Empty GameObject)
    // the details of the player (username, game-wide-XP, ect...)
    // and a List to store the data on each of the buildings (building name, building xp, wallcolour, ect...)

    void Start(){
        DontDestroyOnLoad(UserProfile);
        data = this;

        //CREATE BUILDING INSTANCES HERE

        //file1.onClick.AddListener(() => LoadJson("Assets/JSON/file1.json"));
        //file2.onClick.AddListener(() => LoadJson("Assets/JSON/file2.json"));
    }

    private void LoadJson(string filename)
    {
        JSONNode node;
        using (StreamReader r = new StreamReader(filename))
        {
            //read in the json
            json = r.ReadToEnd();

            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
        }
        string username = JSON.Parse(node["username"].Value);
        Debug.Log(username);
        Username = username;
        int xp = int.Parse(node["global_xp"].Value);
        Debug.Log(global_xp);
        global_xp = xp;
        building_stats.Clear();
        for (int i=0; i<2; i++){
            int primary_colour = int.Parse(node["buildings"][i]["primary_colour"].Value);
            Debug.Log(primary_colour);
            int secondary_colour = int.Parse(node["buildings"][i]["secondary_colour"].Value);
            Debug.Log(secondary_colour);
            int model = int.Parse(node["buildings"][i]["model"].Value);
            Debug.Log(model);
            int building_xp = int.Parse(node["buildings"][i]["building_xp"].Value);
            Debug.Log(building_xp);
            Building newBuilding = new Building(primary_colour,secondary_colour,model,building_xp);
            building_stats.Add(newBuilding);
        }
    }
}

public class Building{
    public int primary_colour;
    public int secondary_colour;
    public int model;
    public int building_xp;

    public Building(int primary, int secondary, int m, int xp){
        primary_colour = primary;
        secondary_colour = secondary;
        model = m;
        building_xp = xp;
    }
}