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

    // The Users data
    public string UserID, Username, Email, Password;
    public int global_xp;

    // A list of 4 int arrays which each store Primary, Secondary, Model and Height values
    public List<int[]> temp_data = new List<int[]>();

    // A list that stores the Users building data
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
        int[] main2 = new int[] {-1,-1,1,0};
        int[] main3 = new int[] {-1,-1,2,0};
        int[] main4 = new int[] {-1,-1,3,0};
        temp_data.Add(main1);
        temp_data.Add(main2);
        temp_data.Add(main3);
        temp_data.Add(main4);

        //Creates a set of default buildings that are then overwritten by any data retrieved from the database
        createBuildings();

        // Dev User
        // UserID = System.Guid.NewGuid().ToString();
    }

    private void createBuildings(){
        for (int j=0; j<4; j++) {
            Building newMain = new Building(-1,-1,j,0,0);
            building_stats.Add(newMain);
        }
        
        for (int i=0; i<8; i++) {
            Building newBuilding = new Building(-1,-1,1,0,0);
            building_stats.Add(newBuilding);    
        }

        Scoring.MainXP = 0;
        Scoring.ArtsXP = 0;
        Scoring.BioCheXP = 0;
        Scoring.ComSciXP = 0;
        Scoring.EngXP = 0;
        Scoring.GeoXP = 0;
        Scoring.LanXP = 0;
        Scoring.LawPolXP = 0;
        Scoring.PhyMathXP = 0;
    }

    public void CreateRequest(string RequestType, int subjectIndex = -1) {
        // Building name, User name. User -> 
        string apiString = "https://uni-builder-database.herokuapp.com/api/";

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
            apiString = apiString + "Buildings/";
            Debug.Log(apiString);
            StartCoroutine(GetRequest(apiString, "Single"));
    
        } else if (RequestType == "GET_Friends") {
            // get the userid(hidden), username and xp of all users
            apiString = apiString + "Users/" + UserID + "/Friends/";
            Debug.Log(apiString);
            StartCoroutine(GetRequest(apiString, "Multiple"));

        } else if (RequestType == "UPDATE_User") {
            // Change the Users personal details 
            // Call CreateUserJSON
            apiString = apiString + "Users/" + UserID;
            Debug.Log(apiString);
            data = CreateUserJSON();
            StartCoroutine(PostRequest(apiString, data, "PUT"));

        } else if (RequestType == "UPDATE_User_Building") {
            apiString = apiString + "Users/" + UserID + "/Buildings/";
            int bc = (subjectIndex*10) + building_stats[subjectIndex].model;
            string buildingCode = bc.ToString();
            string buildingGroup = subjectIndex.ToString();
            apiString = apiString + buildingCode + "/" + buildingGroup  + "/";
            Debug.Log(apiString);
            data = CreateBuildingJSON(subjectIndex);
            StartCoroutine(PostRequest(apiString, data, "POST"));
        }
    }
    
    private string CreateUserJSON() {
        // Create the JSON file storing the User login data for writing to the database
        // id, userName, email, password, userBuidlings, totalExp
        
        List<DatabaseBuildings> uB = new List<DatabaseBuildings>(); //CreateBuildingJSON();
        DatabaseUser putData = new DatabaseUser(UserID, Username, Email, Password, uB, global_xp);
        string UserJSON = JsonUtility.ToJson(putData);

        return UserJSON;
    }

    public string CreateBuildingJSON(int subjectIndex) {
        int bc = (subjectIndex*10) + building_stats[subjectIndex].model; // The unique code for the model within the subject
        int bx = building_stats[subjectIndex].building_xp; // The specific xp of the building
        int h = building_stats[subjectIndex].m_height; // The height of the building (only differs for the Main)
        int bg = subjectIndex; // The subject index
        int pc = building_stats[subjectIndex].primary_colour; // The primary colour of the building
        int sc = building_stats[subjectIndex].secondary_colour; // The secondary colour of the building
            
        DatabaseBuildings currentBuilding = new DatabaseBuildings(bc,bx,h,bg,pc,sc);
        string currentBuilding_string = JsonUtility.ToJson(currentBuilding);

        return currentBuilding_string;
    }

    public void TranslateUserJSON(string rawJSON){
        // Reads a JSON file from the database to create / update the Users data stored in Unity 

        JSONNode node;
        node = JSON.Parse(rawJSON);

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

    public void TranslateUserProfileJSON(string rawJSON){
        // Reads a JSON file from the database to create / update the Users data stored in Unity 
        JSONNode node;
        node = JSON.Parse(rawJSON);
        string username = JSON.Parse(node["userName"].Value);
        int totalExp = int.Parse(node["totalExp"].Value);
        Username = username;
        global_xp = totalExp;
    }

    public void TranslateBuildingJSON(string rawJSON){
        // Reads a JSON file from the database to create / update the Building_Stats list stored in Unity
        
        JSONNode node;
        node = JSON.Parse(rawJSON);

        // Loop through the buildings to create their Unity representations 
        for (int j=0; j<12; j++){
            
            int primary_colour = int.Parse(node["userBuildings"][j]["primaryColour"].Value);
            int secondary_colour = int.Parse(node["userBuildings"][j]["secondaryColour"].Value);
            // Model integer is the final digit in the buildingCode
            string model_code = JSON.Parse(node["userBuildings"][j]["buildingCode"].Value);
            model_code = model_code.Substring(model_code.Length - 1);
            int model = int.Parse(model_code);

            int building_xp = int.Parse(node["userBuildings"][j]["building_xp"].Value);

            int m_height = int.Parse(node["userBuildings"][j]["height"].Value);

            int subjectID = int.Parse(node["userBuildings"][j]["buildingGroup"].Value);

            Building newBuilding = new Building(primary_colour,secondary_colour,model,building_xp, m_height);
            building_stats[subjectID] = newBuilding;

            // Because the subjects are given a known id we can then update the corresponding
            // XP counter in the Scoring code
            if (subjectID == 0) {
                Scoring.MainXP = building_xp;
            } else if (subjectID == 4) {
                Scoring.PhyMathXP = building_xp;
            } else if (subjectID == 5) {
                Scoring.ComSciXP = building_xp;
            } else if (subjectID == 6) {
                Scoring.BioCheXP = building_xp;
            } else if (subjectID == 7) {
                Scoring.GeoXP = building_xp;
            } else if (subjectID == 8) {
                Scoring.LanXP = building_xp;
            } else if (subjectID == 9) {
                Scoring.EngXP = building_xp;
            } else if (subjectID == 10) {
                Scoring.LawPolXP = building_xp;
            } else if (subjectID == 11) {
                Scoring.ArtsXP = building_xp;
            }
        }
    }

    private void TranslateFriendsJSON(string rawJSON) {
        JSONNode node;
        node = JSON.Parse(rawJSON);

        int list_length = node.Count;

        Friend_API_v2.friendslist.Clear();

        for (int i = 0; i < list_length; i++) {
            string friendID = JSON.Parse(node[i]["id"].Value);
            string friendUsername = JSON.Parse(node[i]["userName"].Value);
            int friendXP = JSON.Parse(node[i]["totalExp"].Value);

            Friends newFriend = new Friends(friendID, friendUsername, friendXP);
            Friend_API_v2.friendslist.Add(newFriend);
        }
    }


    IEnumerator GetRequest(string targetAPI, string translationType){

        Debug.Log(targetAPI);
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        Debug.Log("Got the data");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);

            // TRANSLATION CODE HERE
            if (translationType == "Single") {
                // If the translation type is Single then we know that the database is sending all of the Users data
                // which we translate using two seperate functions - one to deal with User data (username, global_xp ect)
                // and the other to deal with that users list/set of buildings (PhyMath, Arts, ComSci ect)
                TranslateUserJSON(raw);
                TranslateBuildingJSON(raw);
            } else if (translationType == "Multiple") {
                // If the translation type is multiple then we are getting the list of friends
                // which need to be added to the friendslist in the friends scene
                TranslateFriendsJSON(raw);
            }
        }
    }

    IEnumerator PostRequest(string targetAPI, string data, string type = "POST") {
        byte[] rawData = System.Text.Encoding.UTF8.GetBytes(data);

        UnityWebRequest uwr = UnityWebRequest.Put(targetAPI, rawData);
        uwr.method = type;
        uwr.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("Sending the data ");
        Debug.Log("Data : " + data);
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            // The POST request also returns the object it entered into the database.
            string raw = uwr.downloadHandler.text;
            //Debug.Log("POST Received: " + raw);
        }   
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
    //public List<DatabaseBuildings> userBuildings;
    public long totalExp;

    public DatabaseUser(string userid, string un, string e, string p, List<DatabaseBuildings> uB, long xp) {
        id = userid;
        userName = un;
        email = e;
        password = p;
        //userBuildings = uB;
        totalExp = xp;
    }
}

[System.Serializable]
public class DatabaseBuildings
{
    public long buildingCode;
    public int building_xp;
    public int height;
    public long buildingGroup;
    public int primaryColour;
    public int secondaryColour;

    public DatabaseBuildings(long bc, int bx, int h, long bg, int pc, int sc)
    {
        buildingCode = bc;
        building_xp = bx;
        height = h;
        buildingGroup = bg;
        primaryColour = pc;
        secondaryColour = sc;
    }
}

