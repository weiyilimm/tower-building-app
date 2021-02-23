using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

//  Friend List Url 
// "Users/{id}/Friends" 
// "Users/{userId}/Friends/{friendId}"

[System.Serializable]
public class Friends_API : MonoBehaviour {
    
    // A list that stores the username and userid of each player the current user has marked as a friend
    public List<Friends> friendslist = new List<Friends>();
    //Use the prefab participant
    public Transform FriendPrefab;
    //Leaderboardlist to be able to store all instances
    public Transform FriendListTransform;
    private TextMeshProUGUI textXP;
    private TextMeshProUGUI textName;
    private TextMeshProUGUI rankText;
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

        User_Data.data.UserID = "c5db6db8-d979-4feb-abb3-395747cd9196";

        /* CreateRequest("Get_FriendIDs"); */
        CreateRequest("GET_FriendIDs");
        
        //Friends newFriend = new Friends("2gh4e", "JumJumJr", 2562);
        //friendslist.Add(newFriend);
        //Friends newFriend2 = new Friends("2guse", "JohnJohnSr", 462735);
        //friendslist.Add(newFriend2);
        //Friends newFriend3 = new Friends("8xh4e", "BobertRoss", 94);
        //friendslist.Add(newFriend3);
        //Friends newFriend4 = new Friends("2ms6e", "RobertBoss", 82637);
        //friendslist.Add(newFriend4);

        /* Code for testing getting the length of a list in JSON and looping over it */
        Debug.Log("Starting read operation...");
        using (StreamReader r = new StreamReader("Assets/Scripts/API/friends.json")) {
            string json = r.ReadToEnd();
            Debug.Log(json);
            TranslateToStringList(json);
        }


        //// Display the data using the UI
        //foreach (Friends data in friendslist){
        //    int index = friendslist.IndexOf(data);

        //    //Create instance(user) as each data loop
        //    var instance = Instantiate(FriendPrefab);
        //    //Set their parent to FriendList
        //    instance.SetParent(FriendListTransform, false);
        //    textName = instance.Find("NameText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        //    textXP = instance.Find("XPText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        //    rankText = instance.Find("RankingText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        //    rankText.text = (friendslist.IndexOf(data) + 1).ToString() + ".";
        //    textName.text = data.UserName;
        //    textXP.text = data.totalExp.ToString();
        //    Debug.Log(friendslist.IndexOf(data));
        //    Debug.Log(data.UserName + " " + data.totalExp);
        //}
    }

    int CreateRequest(string RequestType, string friendID = "-1") {
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

        return 1;
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
                Debug.Log("Found Multiple");
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
        
        //for (int i=0; i<list_length; i++) {
        //    string friendID = JSON.Parse(node[i]["friendId"].Value);
        //    CreateRequest("Get_User", friendID);
        //    Debug.Log(friendslist[i].UserName);
        //}

        StartCoroutine(requestFriendData(node, list_length));

    }

    IEnumerator requestFriendData(JSONNode node, int list_length)
    {
        for (int i = 0; i < list_length; i++)
        {
            string friendID = JSON.Parse(node[i]["friendId"].Value);
            yield return CreateRequest("GET_User", friendID);;
        }
        displayData();
    }

    void displayData()
    { 
        // Display the data using the UI
        foreach (Friends data in friendslist)
        {
            int index = friendslist.IndexOf(data);

            //Create instance(user) as each data loop
            var instance = Instantiate(FriendPrefab);
            //Set their parent to FriendList
            instance.SetParent(FriendListTransform, false);
            textName = instance.Find("NameText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            textXP = instance.Find("XPText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText = instance.Find("RankingText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText.text = (friendslist.IndexOf(data) + 1).ToString() + ".";
            textName.text = data.UserName;
            textXP.text = data.totalExp.ToString();
            Debug.Log(friendslist.IndexOf(data));
            Debug.Log(data.UserName + " " + data.totalExp);
        }

    }

    public void AddToFriendsList(string rawJSON) {
        JSONNode node;
        node = JSON.Parse(rawJSON);

        string friendID = JSON.Parse(node["id"].Value);
        string friendUsername = JSON.Parse(node["userName"].Value);
        int friendXP = JSON.Parse(node["totalExp"].Value);

        Friends newFriend = new Friends(friendID, friendUsername, friendXP);
        friendslist.Add(newFriend);

        //// Display the data using the UI
        //foreach (Friends data in friendslist)
        //{
        //    int index = friendslist.IndexOf(data);

        //    //Create instance(user) as each data loop
        //    var instance = Instantiate(FriendPrefab);
        //    //Set their parent to FriendList
        //    instance.SetParent(FriendListTransform, false);
        //    textName = instance.Find("NameText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        //    textXP = instance.Find("XPText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        //    rankText = instance.Find("RankingText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        //    rankText.text = (friendslist.IndexOf(data) + 1).ToString() + ".";
        //    textName.text = data.UserName;
        //    textXP.text = data.totalExp.ToString();
        //    Debug.Log(friendslist.IndexOf(data));
        //    Debug.Log(data.UserName + " " + data.totalExp);
        //}

    }

}

public class Friends {
    public string UserId;
    public string UserName;
    public int totalExp;

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
