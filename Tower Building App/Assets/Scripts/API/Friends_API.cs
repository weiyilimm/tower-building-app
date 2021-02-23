using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

//  Friend List Url 
// "Users/{id}/Friends" 
// "Users/{userId}/Friends/{friendId}"

[System.Serializable]
public class Friends_API : MonoBehaviour {
    
    // A list that stores the username and userid of each player the current user has marked as a friend
    List<Friends> friendslist = new List<Friends>();

    /*  JSON formatting
        {[
            {"userId":"e6j8g6", "friendId":"c2j2f8"},
            ...
            {"userId":"e6j8g6", "friendId":"g4h5g3"}
        ]}
    */

    void Start() {
        // GET request - Given a userID return all entries in the FRIENDS table with that userID in the 'USER' column
        // Translate the data retrieved from the GET request to a string list of friend ids
        // for each friend id - do a get request of that id to get the username
        
        
        /* CreateRequest("Get_FriendIDs"); */
        
        Friend newFriend = Friends("2gh4e", "JumJumJr", 2562);
        friendslist.Add(newFriend);
        Friend newFriend2 = Friends("2guse", "JohnJohnSr", 462735);
        friendslist.Add(newFriend2);
        Friend newFriend3 = Friends("8xh4e", "BobertRoss", 94);
        friendslist.Add(newFriend3);
        Friend newFriend4 = Friends("2ms6e", "RobertBoss", 82637);
        friendslist.Add(newFriend4);

        /* Code for testing getting the length of a list in JSON and looping over it */
        Debug.Log("Starting read operation...");
        using (StreamReader r = new StreamReader("Assets/Scripts/API/friends.json")) {
            string json = r.ReadToEnd();
            Debug.Log(json);
            TranslateToStringList(json);
        }

        // Display the data using the UI
    }

    void CreateRequest(string RequestType, string friendID = "-1") {
        string apiString = "http://localhost:8080/api/Users/";

        if (RequestType == "GET_FriendIDs") { 
            // Target API: apiString/{id}/Friends
            apiString = apiString + User_Data.data.UserID + "/Friends/";
            StartCoroutine(GetRequest(apiString, "Multiple"));

        } else if (RequestType == "GET_User") {
            // Target API: apiString/{id}/Friends
            apiString = apiString + friendID;
            StartCoroutine(GetRequest(apiString, "Single"));

        } else if (RequestType == "CREATE_Friend") {
            // Target API: apiString/{UserId}/Friends/{FriendId}
            apiString = apiString + User_Data.data.UserID + "/Friends/" + friendID;
            FriendLink newFriend = new FriendLink(User_Data.data.UserID, friendID);
            string data = JsonUtility.ToJson(newFriend);
            StartCoroutine(PostRequest(apiString, data, "POST"));

        } else if (RequestType == "DELETE_Friend") {
            // Target API: apiString/{UserId}/Friends/{FriendId}
            apiString = apiString + User_Data.data.UserID + "/Friends/" + friendID;
            StartCoroutine(DeleteRequest(apiString));
        }
    }

    IEnumerator GetRequest(string targetAPI, string GET_type) {
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
            if (GET_type == "Multiple") {
                TranslateToStringList(raw);
            } else if (GET_type == "Single") {
                AddToFriendsList(raw);
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
            Debug.Log("Received: " + raw);
        }
    }

    IEnumerator DeleteRequest(string targetAPI) {
        Debug.Log(targetAPI);
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Delete(targetAPI);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            Debug.Log("Friend Deleted");
        }
    }

    private void TranslateToStringList(string rawJSON){ 
        JSONNode node;
        node = JSON.Parse(rawJSON);

        int list_length = node.Count;
        Debug.Log(list_length);
        
        for (int i=0; i<list_length; i++) {
            string friendID = JSON.Parse(node[i]["friendId"].Value);
            CreateRequest("Get_User", friendID);
        }
    }

    public void AddToFriendsList(string rawJSON) {
        JSONNode node;
        node = JSON.Parse(rawJSON);

        string friendID = JSON.Parse(node["id"].Value);
        string friendUsername = JSON.Parse(node["userName"].Value);
        int friendXP = JSON.Parse(node["totalExp"].Value);

        Friends newFriend = new Friends(friendID, friendUsername);
        friendslist.Add(newFriend);
    }
}

public class Friends {
    string UserId;
    string UserName;
    int totalExp;

    public Friends(string ui, string un, int xp) {
        UserId = ui;
        UserName = un;
        totalExp = xp;
    }
}

[System.Serializable]
public class FriendLink {
    string userID;
    string friendID;

    public FriendLink(string ui, string fi) {
        userID = ui;
        friendID = fi;
    }
}
