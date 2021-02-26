using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Friend_API_v2 : MonoBehaviour
{

    // A list that stores the username and userid of each player the current user has marked as a friend
    public List<Friends> friendslist = new List<Friends>();
    //Use the prefab participant
    public Transform FriendPrefab;
    //Leaderboardlist to be able to store all instances
    public Transform FriendListTransform;
    private TextMeshProUGUI textXP;
    private TextMeshProUGUI textName;
    private TextMeshProUGUI textId;
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

        User_Data.data.UserID = "1da1b562-7f05-401d-9e69-70e82a1bf188";

        /* CreateRequest("Get_FriendIDs"); */
        Debug.Log("Finding the users friends...");
        CreateRequest("GET_FriendIDs");
    }

    private void CreateRequest(string RequestType) {
        string apiString = "https://uni-builder-database.herokuapp.com/api/Users/";
        // Needs refactoring
        apiString = apiString + User_Data.data.UserID + "/Friends/";

        StartCoroutine(GetRequest(apiString));
    }

    IEnumerator GetRequest(string targetAPI) {
        Debug.Log(targetAPI);
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);

            AddToFriendsList(raw);
        }
    }

    public void displayData() {
        // Display the data using the UI
        foreach (Friends data in friendslist) {
            int index = friendslist.IndexOf(data);

            //Create instance(user) as each data loop
            var instance = Instantiate(FriendPrefab);
            //Set their parent to FriendList
            instance.SetParent(FriendListTransform, false);
            textName = instance.Find("NameText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            textXP = instance.Find("XPText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            textId = instance.Find("IdText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText = instance.Find("RankingText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText.text = (friendslist.IndexOf(data) + 1).ToString() + ".";
            textName.text = data.UserName;
            textId.text = data.UserId;
            textXP.text = data.totalExp.ToString();
            //Debug.Log(friendslist.IndexOf(data));
            //Debug.Log(data.UserName + " " + data.totalExp);
        }
    }

    private void AddToFriendsList(string rawJSON) {
        JSONNode node;
        node = JSON.Parse(rawJSON);

        int list_length = node.Count;
        Debug.Log(list_length);

        Debug.Log("Adding to friends list");
        for (int i = 0; i < list_length; i++) {
            string friendID = JSON.Parse(node[i]["id"].Value);
            string friendUsername = JSON.Parse(node[i]["userName"].Value);
            int friendXP = JSON.Parse(node[i]["totalExp"].Value);

            Friends newFriend = new Friends(friendID, friendUsername, friendXP);
            friendslist.Add(newFriend);
        }
        displayData();
    }
}

public class Friends
{
    public string UserId;
    public string UserName;
    public int totalExp;

    public Friends(string ui, string un, int xp)
    {
        UserId = ui;
        UserName = un;
        totalExp = xp;
    }
}

[System.Serializable]
public class FriendLink
{
    string userID;
    string friendID;

    public FriendLink(string ui, string fi)
    {
        userID = ui;
        friendID = fi;
    }
}
