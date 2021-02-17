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
    public string UserID, Username, Email, Password;
    public int global_xp;

    public List<int[]> temp_data = new List<int[]>();
    public List<Building> building_stats = new List<Building>();

    //This script will store all of the data assigned to a single user
    // It will contain the object it travels thorugh the scenes on (Empty GameObject)
    // the details of the player (username, game-wide-XP, ect...)
    // and a List to store the data on each of the buildings (building name, building xp, wallcolour, ect...)

    void Start() {
        DontDestroyOnLoad(UserProfile);
        data = this;

        // Initialize the four temp_data arrays
        int[] main1 = new int[] {-1,-1,0,0};
        int[] main2 = new int[] {-1,-1,0,0};
        int[] main3 = new int[] {-1,-1,0,0};
        int[] main4 = new int[] {-1,-1,0,0};
        temp_data.Add(main1);
        temp_data.Add(main2);
        temp_data.Add(main3);
        temp_data.Add(main4);

        //CREATE BUILDING INSTANCES HERE
        createBuildings();

        // The stages of starting the game; authenticate user, get users data (username, XP), get that users building data

        // Login 
        // GetRequest("User");
        // GetRequest("Buildings");

        /*
         *      GET REQUESTS
        */
        //Debug.Log("Running the GET requests");
        //// Get a saved user
        //CreateRequest("GET", "Users", "bce13125-3d7f-4452-8428-efaecb8be59e");
        //// Get all saved users
        //CreateRequest("GET", "Users");

        /*
         *      POST REQUESTS
        */

        // Dev User
        UserID = System.Guid.NewGuid().ToString();
        Username = "Jim";
        Email = "Jim@email.com";
        Password = "7638";
        global_xp = 500;

        // Dev Building
        DatabaseBuildings currentBuilding = new DatabaseBuildings(140, "Effiel Tower", 0, -1, 4, -1, -1);
        var stringBuildingJsonData = JsonUtility.ToJson(currentBuilding);

        //// Create a new user
        // CreateRequest("CREATE User");

        //// Create a new model
        //CreateRequest("POST", "Models", data: data);

        //// Edit a existing user's details
        // CreateRequest("UPDATE User");

        // Add/Remove a building from an existing user. - OLD
        //CreateRequest("POST", "Users", "5d1841f8-8049-44a0-9fbf-992de0240e07", 140, stringBuildingJsonData);
    }

    public void Update() {
        if (Input.GetKeyDown("t")){
            /* CODE FOR TESTING JSON CREATION */
            UserID = "abc";
            Username = "BobertRoss";
            Email = "bobert@bobert.com";
            Password = "321password";
            global_xp = 2000000;
            string stringOutput = CreateUserJSON();
            Debug.Log(stringOutput);
        } else if (Input.GetKeyDown("y")) {
            /* CODE FOR TESTING THE TRANSLATION OF BUILDING DATA */
            Debug.Log("Starting translation...");
            TranslateBuildingJSON("Assets/Scripts/Customisation/test.json");
            Debug.Log(building_stats[0].primary_colour);
            Debug.Log(building_stats[0].secondary_colour);
            Debug.Log(building_stats[0].building_xp);
            Debug.Log(building_stats[0].model);
            Debug.Log(building_stats[0].m_height);
        }
    }

    private void createBuildings(){
        for (int i=0; i<12; i++) {
            Building newBuilding = new Building(-1,-1,0,40000,0);
            building_stats.Add(newBuilding);    
        }
        Scoring.MainXP = 40000;
        Scoring.ArtsXP = 40000;
        Scoring.BioCheXP = 40000;
        Scoring.ComSciXP = 40000;
        Scoring.EngXP = 40000;
        Scoring.GeoXP = 40000;
        Scoring.LanXP = 40000;
        Scoring.LawPolXP = 40000;
        Scoring.PhyMathXP = 40000;
    }

    /* 
        RequestType = "GET" or "Update"
        Table = "Users" or "Models"
        id = URI of either an User or Building.
     */
    public void CreateRequest(string RequestType, string id = "-1")
    {
        // Building name, User name. User -> 
        string apiString = "http://localhost:8080/api/";

        // The data and any building ids will be generated in function as a means
        // of reducing the number of external function calls in other parts of the app
        // and to increase the protection of said variables
        string data;
        // int buildingid;

        if (RequestType == "CREATE_User") {
            // Create a new User
            apiString = string.Concat(apiString + "Users/");
            Debug.Log(apiString);
            data = CreateUserJSON();
            StartCoroutine(PostRequest(apiString, data));

        } else if (RequestType == "GET_User") {
            // Get the data of the current User
            string requestedId = UserID + "/";
            // USE CASE: to get all the buildings belonging to the user at the start of the game.
            apiString = string.Concat(apiString, "Users/");
            apiString = string.Concat(apiString, requestedId);
            Debug.Log(apiString);
            StartCoroutine(GetRequest(apiString, "Single"));

        } else if (RequestType == "GET_All_Users") {
            // get the userid(hidden), username and xp of all users
            apiString = string.Concat(apiString + "Users/");
            Debug.Log(apiString);
            StartCoroutine(GetRequest(apiString, "Multiple"));

        } else if (RequestType == "UPDATE_User") {
            // Change the Users personal details 
            // Call CreateUserJSON
            apiString = apiString + "Users/" + UserID;
            Debug.Log(apiString);
            data = CreateUserJSON();
            StartCoroutine(PostRequest(apiString, data, "PUT"));

        } /* else if (RequestType == "UPDATE_Buildings") {
            // Update the property of one of the buildings belonging to the user, e.g. increasing the EXP.
            // Call CreateBuildingJSON
            string targetID;
            string targetAPI;
            if (id != "-1") { targetID = id; } else { targetID = UserID; }
            apiString = apiString + "/" + targetID + "/" + "Buildings" + "/";
            List<string> buildingData = CreateBuildingJSON();
            for (int i=0; i<12; i++) {
                targetAPI = apiString;
                buildingid = (i*10) + (building_stats[i].model);
                targetAPI = targetAPI + buildingid.ToString();
                data = buildingData[i];
                Debug.Log(targetAPI);
                StartCoroutine(PostRequest(apiString, data, "POST"));
            }
        } */



        /* OLD CODE KEEPING UNTIL NEW CODE HAS BEEN TESTED */
        /*
        if (RequestType == "GET")
        {
            // Want to get a specfic User/Building.
            if (id != "-1")
            {
                string requestedId = "";
                // Want to get all the attributes of a particular user (USE CASE: to get all the buildings belonging to the user at the start of the game).
                requestedId = "/" + id;
                apiString = string.Concat(apiString, requestedId);
                apiString = string.Concat("http://localhost:8080/", apiString);
                Debug.Log(apiString);
                StartCoroutine(GetRequest(apiString, "Single"));
            } 
            // Get all the Users/Buildings
            else
            {
                apiString = string.Concat(apiString + "/");
                apiString = string.Concat("http://localhost:8080/", apiString);
                Debug.Log(apiString);
                StartCoroutine(GetRequest(apiString, "Multiple"));
            }
        }
        else
        {
            // POST 
            if (id == "-1")
            {
                // Create a new User
                apiString = string.Concat(apiString + "/");
                apiString = string.Concat("http://localhost:8080/", apiString);
                Debug.Log(apiString);
                StartCoroutine(PostRequest(apiString, data));
            }
            else
            {
                if (buildingid == -1)
                {
                    // Change the Users personal details 
                    apiString = apiString + "/" + id;
                    apiString = "http://localhost:8080/" + apiString;
                    Debug.Log(apiString);
                    StartCoroutine(PostRequest(apiString, data, "PUT"));
                }
                else
                {
                    // Update the property of one of the buildings belonging to the user, e.g. increasing the EXP.
                    apiString = apiString + "/" + id + "/" + "Buildings" + "/" + buildingid;
                    apiString = "http://localhost:8080/" + apiString;
                    Debug.Log(apiString);
                    StartCoroutine(PostRequest(apiString, data, "POST"));
                }
            }
        } */
    }

    /*
            JSON will be formatted as such:
            {"id":???, 
                "userName":"example", 
                "email":email@email.com,
                "password":aifbreiu,
                "userBuildings":
                    [{buildingCode:12, 
                        buildingName:"Tower1",
                        building_xp:1454, 
                        "height":1, 
                        "primaryColour":104, 
                        "secondaryColour":201, 
                        "modelGroup":2}, ... , ],
                "totalExp":27353
            }
        */
    
    public List<DatabaseBuildings> CreateBuildingJSON() {
        
        List<DatabaseBuildings> uB = new List<DatabaseBuildings>();
        
        for (int i=0; i<12; i++){
            int bc = (i*10) + building_stats[i].model; // The unique code for the model within the subject
            string bn = CodeConverter.codes.buildingName_map[bc]; // The name of the building
            int bx = building_stats[i].building_xp; // The specific xp of the building
            int h = building_stats[i].m_height; // The height of the building (only differs for the Main)
            int mg = i; // The subject index
            int pc = building_stats[i].primary_colour; // The primary colour of the building
            int sc = building_stats[i].secondary_colour; // The secondary colour of the building
            
            DatabaseBuildings currentBuilding = new DatabaseBuildings(bc,bn,bx,h,mg,pc,sc);
            //string currentBuilding_string = JsonUtility.ToJson(currentBuilding);
            uB.Add(currentBuilding);
        }
        return uB;
    }
    
    // WRITE
    private string CreateUserJSON() {
        // Create the JSON file storing the User login data for writing to the database
        // id, userName, email, password, userBuidlings, totalExp
        
        List<DatabaseBuildings> uB = CreateBuildingJSON(); // new List<DatabaseBuildings>();
        DatabaseUser putData = new DatabaseUser(UserID, Username, Email, Password, uB, global_xp);
        string UserJSON = JsonUtility.ToJson(putData);

        return UserJSON;
    }

    private void TranslateBuildingJSON(string rawJSON){
        // Reads a JSON file from the database to create / update the Building_Stats list stored in Unity
        
        JSONNode node;
        using (StreamReader r = new StreamReader(rawJSON)) {
            //read in the json
            string json = r.ReadToEnd();

            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
        }

        //Clears the Unity building list representation so it can be created fresh with the correct data
        building_stats.Clear();

        // Loop through the buildings to create their Unity representations 
        for (int j=0; j<2; j++){
            // Might need to get the modelGroup as well if the buildings are not sent in order
            
            int primary_colour = int.Parse(node["userBuildings"][j]["primaryColour"].Value);
            int secondary_colour = int.Parse(node["userBuildings"][j]["secondaryColour"].Value);
            // Model integer is the final digit in the buildingCode
            string model_code = JSON.Parse(node["userBuildings"][j]["buildingCode"].Value);
            //string model_string = model_code.ToString();
            model_code = model_code.Substring(model_code.Length - 1);
            int model = int.Parse(model_code);

            int building_xp = int.Parse(node["userBuildings"][j]["building_xp"].Value);

            int m_height = int.Parse(node["userBuildings"][j]["height"].Value);

            Building newBuilding = new Building(primary_colour,secondary_colour,model,building_xp, m_height);
            building_stats.Add(newBuilding);
        }
    }

    private void TranslateUserJSON(string rawJSON){
        // Reads a JSON file from the database to create / update the Users data stored in Unity 

        JSONNode node;
        using (StreamReader r = new StreamReader(rawJSON)) {
            //read in the json
            string json = r.ReadToEnd();

            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
        }
        string userid = JSON.Parse(node["id"].Value);
        string username = JSON.Parse(node["userName"].Value);
        string email = JSON.Parse(node["email"].Value);
        string password = JSON.Parse(node["password"].Value);
        int totalExp = int.Parse(node["totalExp"].Value);
        UserID = userid;
        Username = username;
        Email = email;
        Password = password;
        global_xp = totalExp;
    }


    IEnumerator GetRequest(string targetAPI, string translationType){

        Debug.Log(targetAPI);
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        Debug.Log("Got the data");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("An Internal Server Error Was Encountered");
        } 
        else
        {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);

            // TRANSLATION CODE HERE
            if (translationType == "Single") {
                // If the translation type is Single then we know that the database is sending all of the Users data
                // which we translate using two seperate functions - one to deal with User data (username, global_xp ect)
                // and the other to deal with that users list/set of buildings (PhyMath, Arts, ComSci ect)
                TranslateUserJSON(raw);
                TranslateBuildingJSON(raw);
            } else if (translationType == "Multiple Users") {
                // This will be used for the leaderboard
                // New translation function will be written for this
            }
        }
    }

    IEnumerator PostRequest(string targetAPI, string data, string type = "POST")
    {
        byte[] rawData = System.Text.Encoding.UTF8.GetBytes(data);

        UnityWebRequest uwr = UnityWebRequest.Put(targetAPI, rawData);
        uwr.method = type;
        uwr.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("Sending the data ");
        Debug.Log("Data : " + data);
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError)
        {
            Debug.Log("An Internal Server Error Was Encountered");
        }
        else
        {
            // The POST request also returns the object it entered into the database.
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);

            // TRANSLATION CODE HERE (to check if data was correctly entered).

        }
        
    }

    IEnumerator PutRequest(string targetAPI, string json){
        // Constructs and sends a PUT request to the database to update it with the given JSON file

        Debug.Log(targetAPI);
        
        byte[] rawData = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest uwr = UnityWebRequest.Put(targetAPI, rawData);
        uwr.SetRequestHeader("Content-Type", "application/json"); 
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

[System.Serializable]
public class DatabaseUser {
    public string id;
    public string userName;
    public string email;
    public string password;
    public List<DatabaseBuildings> userBuildings;
    public long totalExp;

    public DatabaseUser(string userid, string un, string e, string p, List<DatabaseBuildings> uB, long xp) {
        id = userid;
        userName = un;
        email = e;
        password = p;
        userBuildings = uB;
        totalExp = xp;
    }
}

[System.Serializable]
public class DatabaseBuildings
{
    public long buildingCode;
    public string buildingName;
    public int building_xp;
    public int height;
    public long modelGroup;
    public int primaryColour;
    public int secondaryColour;

    public DatabaseBuildings(long bc, string bn, int bx, int h, long mg, int pc, int sc)
    {
        buildingCode = bc;
        buildingName = bn;
        building_xp = bx;
        height = h;
        modelGroup = mg;
        primaryColour = pc;
        secondaryColour = sc;
    }
}

