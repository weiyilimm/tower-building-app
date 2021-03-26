using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.Linq;

[System.Serializable]
public class Friend_API_v2 : MonoBehaviour
{

    // A list that stores the username and userid of each player the current user has marked as a friend
    public static List<Friends> friendslist = new List<Friends>();
    private List<Friends> friendslist_InOrder = new List<Friends>();
    //Use the prefab participant
    public Transform FriendPrefab;
    //Input field from search
    public TMP_InputField SearchInputField;
    //FriendListTransform to be able to store all instances
    public Transform FriendListTransform;
    private TextMeshProUGUI textXP;
    private TextMeshProUGUI textName;
    private TextMeshProUGUI textId;
    private TextMeshProUGUI rankText;
    public GameObject PopUpInternetFailure;
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
        Debug.Log("Finding the users friends...");
        CreateRequest("GET_FriendIDs");
        SearchInputField.onValueChanged.AddListener(delegate {FilterUser(); });
    }

    public void FilterUser() {
        // Loop through each single user in the leaderboard list
        for (int i = 0; i < FriendListTransform.childCount; i++)
        {   
            //Use child as temporary variable for each user
            Transform child = FriendListTransform.GetChild(i);
            //If the user input is empty then show every player
            if(SearchInputField.text == "" ){
                child.gameObject.SetActive(true);
            }
            else{
                //If the user input is not empty, then get the username
                TextMeshProUGUI userName = child.Find("NameText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                /*
                As the default are case sensitive
                Change username to lower and user input to lower
                e.g. If user type "richard", the leader will still show "Richard" user
                */
                string userInputLower = SearchInputField.text.ToLower();
                string userNameLower = userName.text.ToLower();
                //If the username contains the user input
                if (userNameLower.Contains(userInputLower)){
                    //Show the specific user according to the user input
                    child.gameObject.SetActive(true);
                }
                //If the username does not contains the user input
                else{
                    //Hide it
                    child.gameObject.SetActive(false);
                }
                
            }
        }
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
            PopUpInternetFailure.SetActive(true);
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);

            AddToFriendsList(raw);
        }
    }

    public void displayData() {
        friendslist_InOrder  = friendslist.OrderByDescending(x => x.totalExp).ToList();
        // Display the data using the UI
        foreach (Friends data in friendslist_InOrder) {
            int index = friendslist.IndexOf(data);
            //Create instance(user) as each data loop
            var instance = Instantiate(FriendPrefab);
            //Set their parent to FriendList
            instance.SetParent(FriendListTransform, false);
            textName = instance.Find("NameText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            textXP = instance.Find("XPText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            textId = instance.Find("IdText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText = instance.Find("RankingText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText.text = (friendslist_InOrder.IndexOf(data) + 1).ToString() + ".";
            textName.text = data.UserName;
            textId.text = data.UserId;
            textXP.text = data.totalExp.ToString();
        }
    }

    private void AddToFriendsList(string rawJSON) {
        JSONNode node;
        node = JSON.Parse(rawJSON);

        int list_length = node.Count;
        Debug.Log(list_length);

        friendslist.Clear();
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
