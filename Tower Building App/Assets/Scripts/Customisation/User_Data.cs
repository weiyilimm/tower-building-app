using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using UnityEngine.Networking;

[System.Serializable]
public class User_Data : MonoBehaviour{
    public static User_Data data;
    public GameObject UserProfile;
    public string Username,json;
    public int global_xp;

    public int temp_primary;
    public int temp_secondary;
    public int temp_model;
    public int temp_height;
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
        createBuildings();

        // The stages of starting the game; authenticate user, get users data (username, XP), get that users building data

        // Login 
        // GetRequest("User");
        // GetRequest("Buildings");

        // OLD CODE leaving for referal to for now
        //file1.onClick.AddListener(() => LoadJson("Assets/JSON/file1.json"));
        Debug.Log("Run from User Data");
        CreateRequest("GET", "Models", 100);



    }

    private void createBuildings(){
        Building Main = new Building(100,4,0,500,4);
        building_stats.Add(Main);
        Building Main2 = new Building(101,5,1,500,2);
        building_stats.Add(Main2);
        Building Main3 = new Building(102,9,2,500,1);
        building_stats.Add(Main3);
        Building Main4 = new Building(103,11,3,500,3);
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
    }

    /* 
        RequestType = "GET" or "Update"
        Table = "Users" or "Models"
     */
    private void CreateRequest(string RequestType, string Table, int id = -1)
    {
        // Building name, User name. User -> 
        string apiString = "api/";

        // Go to Users table or the Buildings Table.
        if (RequestType == "GET")
        {
            // Get all the buildings.
            apiString = string.Concat(apiString, Table);

            if (id > -1)
            {
                string requestedId = string.Concat("/", id.ToString());
                apiString = string.Concat(apiString, requestedId);
            }
            apiString = string.Concat("http://localhost:8080/", apiString);
            StartCoroutine(GetRequest(apiString));

        }
    }

    private void CreateBuildingJSON(){
        // Create the JSON file storing the building data for writing to the database

        /*
            Assuming the JSON will be formatted as such:
            {MainBuildings: 
                [[Name:"Tower1", Primary:104, Secondary:201, Model:2, Height:3], ... , [Name:"Tower4", Primary...]],   
            SubjectBuildings:
                [[Name:"Arts", Primary:012, Secondary:402, Model:1, XP:2036], ... , [Name:"Physics&Maths", Primary...]]
            }
        */

        string BuildingsJSON = "test";
        
        // Loop through the 4 main building towers to add their data to the JSON string
        for (int i=0; i<4; i++){
            //do this
        }

        // Loop through the 8 subject buildings to add their data to the JSON string
        for (int j=0; j<8; j++){
            //do this
        }

    }

    // Translation Functions

    private string CreateUserJSON(){
        // Create the JSON file storing the User login data for writing to the database

        /*
            Assuming the JSON will be formatted as such:
            {User: 
                [username:"John", globalXP:24564, (any other relevant data)]   
            }
        */

        string UserJSON = "{User:[username:" + Username + ",globalXP:" + global_xp.ToString() + "]}";
        return UserJSON;
    }

    private void TranslateBuildingJSON(string rawJSON){
        // Reads a JSON file from the database to create / update the Building_Stats list stored in Unity
        
        /*
            Assuming the JSON will be formatted as such:
            {MainBuildings: 
                [[Name:"Tower1", Primary:104, Secondary:201, Model:2, Height:3], ... , [Name:"Tower4", Primary...]],   
            SubjectBuildings:
                [[Name:"Arts", Primary:012, Secondary:402, Model:1, XP:2036], ... , [Name:"Physics&Maths", Primary...]]
            }
        */
        
        JSONNode node;
        using (StreamReader r = new StreamReader(rawJSON)) {
            //read in the json
            json = r.ReadToEnd();

            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
        }

        //Clears the Unity building list representation so it can be created fresh with the correct data
        building_stats.Clear();

        // Loop through the 4 main building towers to create their Unity representations
        for (int i=0; i<4; i++){
            int primary_colour = int.Parse(node["MainBuildings"][i]["Primary"].Value);
            
            int secondary_colour = int.Parse(node["MainBuildings"][i]["Secondary"].Value);
            
            int model = int.Parse(node["MainBuildings"][i]["Model"].Value);
            
            int m_height = int.Parse(node["MainBuildings"][i]["Height"].Value);

            Building newBuilding = new Building(primary_colour,secondary_colour,model,0, m_height);
            building_stats.Add(newBuilding);
        }

        // Loop through the 8 subject buildings to create their Unity representations 
        for (int j=0; j<8; j++){
            int primary_colour = int.Parse(node["SubjectBuildings"][j]["Primary"].Value);
            
            int secondary_colour = int.Parse(node["SubjectBuildings"][j]["Secondary"].Value);
            
            int model = int.Parse(node["SubjectBuildings"][j]["Model"].Value);
            
            int building_xp = int.Parse(node["SubjectBuildings"][j]["XP"].Value);

            Building newBuilding = new Building(primary_colour,secondary_colour,model,building_xp, 0);
            building_stats.Add(newBuilding);
        }
    }

    private void TranslateUserJSON(string rawJSON){
        // Reads a JSON file from the database to create / update the Users data stored in Unity 

        /*
            Assuming the JSON will be formatted as such:
            {User: 
                [username:"John", globalXP:24564, (any other relevant data)]   
            }
        */

        JSONNode node;
        using (StreamReader r = new StreamReader(rawJSON)) {
            //read in the json
            json = r.ReadToEnd();

            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
        }
        string username = JSON.Parse(node["User"]["username"].Value);
        Username = username;
        int xp = int.Parse(node["User"]["global_xp"].Value);
        global_xp = xp;
    }


    IEnumerator GetRequest(string targetAPI){

        Debug.Log(targetAPI);
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        Debug.Log("Got the data");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error while sending " + uwr.error);
        } 
        else
        {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);
            BuildingTrue modelGot = JsonUtility.FromJson<BuildingTrue>(raw);
            Debug.Log("The model given was " + modelGot.building_name);
        }
    }

    IEnumerator PutRequest(string targetAPI, string json){
        // Constructs and sends a PUT request to the database to update it with the given JSON file

        Debug.Log(targetAPI);
        
        UnityWebRequest uwr = UnityWebRequest.Put(targetAPI, json);
        
        // add the JSON data to send here? or before in the creation of the call?
        Debug.Log("Sending the data...");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error while sending " + uwr.error);
        } 
        else
        {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Message Recieved - data updated");
        }
    }
}

public class Building{
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

public class BuildingTrue
{
    public long building_code;
    public string building_name;
    public int building_xp;
    public int height;
    public long model_group;
    public string primary_colour;
    public string secondary_colour;

    public BuildingTrue(long building_code, long building_name, int building_xp, int height,
        long model_group, string primary_colour, string secondary_colour)
    {
        building_code = building_code;
        building_name = building_name;
        building_xp = building_xp;
        height = height;
        model_group = model_group;
        primary_colour = primary_colour;
        secondary_colour = secondary_colour;
    }
}

