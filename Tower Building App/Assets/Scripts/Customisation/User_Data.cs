using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;

[System.Serializable]
public class User_Data : MonoBehaviour{
    // The User_Data class and the object that stores it
    public static User_Data data;
    public GameObject UserProfile;
    
    // The Users given username and global xp points
    public string Username,json;
    public int global_xp;

    // The variables to temporarily store the users choices until they select confirm
    public int temp_primary;
    public int temp_secondary;
    public int temp_model;
    public int temp_height;

    // A list that stores the subject building data in a known order
    public List<Building> building_stats = new List<Building>();
    
    //This script will store all of the data assigned to a single user
    // It will contain the object it travels thorugh the scenes on (Empty GameObject)
    // the details of the player (username, game-wide-XP, ect...)
    // and a List to store the data on each of the buildings (building name, building xp, wallcolour, ect...)

    void Start(){
        DontDestroyOnLoad(UserProfile);
        data = this;

        //CREATE BUILDING INSTANCES HERE
        Building Main = new Building(100,4,2,500,4);
        building_stats.Add(Main);
        Building Main2 = new Building(101,5,3,500,2);
        building_stats.Add(Main2);
        Building Main3 = new Building(102,9,0,500,1);
        building_stats.Add(Main3);
        Building Main4 = new Building(103,11,1,500,3);
        building_stats.Add(Main4);
        Building Art = new Building(-1,-1,0,450,1);
        building_stats.Add(Art);
        Building Biology_Chemistry = new Building(3,5,0,400,1);
        building_stats.Add(Biology_Chemistry);
        Building ComputerScience = new Building(-1,-1,0,350,1);
        building_stats.Add(ComputerScience);
        Building Engineering = new Building(-1,-1,0,300,1);
        building_stats.Add(Engineering);
        Building Geography_History = new Building(-1,-1,0,250,1);
        building_stats.Add(Geography_History);
        Building Languages = new Building(-1,-1,0,200,1);
        building_stats.Add(Languages);
        Building Law_Politics = new Building(-1,-1,0,150,1);
        building_stats.Add(Law_Politics);
        Building Physics_Maths = new Building(-1,-1,0,100,1);
        building_stats.Add(Physics_Maths);

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
            int m_height = 1;
            Building newBuilding = new Building(primary_colour,secondary_colour,model,building_xp, m_height);
            building_stats.Add(newBuilding);
        }
    }
}

public class Building{
    // The different attributes of a building
    public int primary_colour;
    public int secondary_colour;
    public int model;
    public int building_xp;
    public int m_height;

    public Building(int primary, int secondary, int m, int xp, int h){
        primary_colour = primary;
        secondary_colour = secondary;
        model = m;
        building_xp = xp;
        m_height = h;
    }
}