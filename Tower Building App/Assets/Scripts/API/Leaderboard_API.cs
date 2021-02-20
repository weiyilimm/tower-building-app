using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

[System.Serializable]
public class Leaderboard_API : MonoBehaviour {
    
    public List<leaderboard_data> LB_data = new List<leaderboard_data>();
    
    /*  JSON formatting - NOT YET FINAL!
        {"users": [
            {"id":sgisvi, "userName":"BobertRoss", totalExp: 25000},
            {"id":fsiufb, "userName":"RobertBoss", totalExp: 16000},
            {"id":oncsbd, "userName":"JimJimSr", totalExp: 12000},
            {"id":mbcuse, "userName":"JumJumJr", totalExp: 5000},
            ...
            {"id":sgisvi, "userName":"user1", totalExp: 1000},
        ]}
    */

    void Start() {
        // GET Request - Top 50 users by totalExp then
        // Translate the data retrieved from the GET request
        
        /* CreateRequest("GET_Leaderboard"); COMMENTED OUT FOR NOW TO TEST DISPLAYING DATA */

        //HARD CODE for testing purposes
        using (StreamReader r = new StreamReader("Assets/Scripts/API/leaderboard.json")) {
            string json = r.ReadToEnd();
            TranslateToLeaderboard(json);
        }

        //Print out the data for the five users in the leaderboard
        foreach (leaderboard_data data in LB_data){
            Debug.Log(data.UserName + " " + data.TotalExp);
        }
        
        // Display the data using the UI -> Put this into a coroutine.

    }

    void CreateRequest(string RequestType) {
        string apiString = "http://localhost:8080/api/Users/";

        if (RequestType == "GET_Leaderboard") { 
            StartCoroutine(GetRequest(apiString));
        }
    }

    IEnumerator GetRequest(string targetAPI) {
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
            TranslateToLeaderboard(raw);
        }
    }

    private void TranslateToLeaderboard(string rawJSON){ 

        JSONNode node;
        //using (StreamReader r = new StreamReader(rawJSON)) {
        //    //read in the json
        //    Debug.Log("Passed the stream reader");
        //    string json = r.ReadToEnd();
        //    Debug.Log("Passed the readToEnd");
        //    //reformat the json into dictionary style convention
        //    node = JSON.Parse(json);
        //}

        node = JSON.Parse(rawJSON);

        Debug.Log(node);

        string userid;
        string username;
        int totalExp;
        
        for (int i=0; i<5; i++) {
            userid = JSON.Parse(node[i]["id"].Value);
            username = JSON.Parse(node[i]["userName"].Value);
            totalExp = JSON.Parse(node[i]["totalExp"].Value);

            leaderboard_data data = new leaderboard_data(userid, username, totalExp);
            LB_data.Add(data);
        }
    }
}

public class leaderboard_data {
    public string UserId;
    public string UserName;
    public int TotalExp;

    public leaderboard_data(string ui, string un, int xp) {
        UserId = ui;
        UserName = un;
        TotalExp = xp;
    }
}
