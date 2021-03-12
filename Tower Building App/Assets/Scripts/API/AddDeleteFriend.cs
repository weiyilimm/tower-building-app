using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class AddDeleteFriend : MonoBehaviour {
    
    public GameObject addFriend, removeFriend;
    public TextMeshProUGUI friendId;
    public TextMeshProUGUI friendUserName;
    public TextMeshProUGUI friendExp;
    public string apiString = "https://uni-builder-database.herokuapp.com/api/Users/";
    public string otherID;

    public void typeCheck() {
        otherID = friendId.text;
        if (removeFriend.activeSelf) {
            DeleteFriend();
        } else if (addFriend.activeSelf) {
            AddFriend();
        }
    }

    public void AddFriend() {
        //Friends_API.CreateRequest("CREATE_Friend", thisFriendID);
        apiString = "https://uni-builder-database.herokuapp.com/api/Users/";
        apiString = apiString + User_Data.data.UserID + "/Friends/" + otherID;
        Debug.Log("POST Request at: " + apiString);

        // Creates dummy data since the request requires some to be built
        FriendLink newFriend = new FriendLink(User_Data.data.UserID, otherID);
        string data = JsonUtility.ToJson(newFriend);

        StartCoroutine(PostRequest(apiString, data, "POST"));
    }

    public void DeleteFriend() {
        //Friends_API.CreateRequest("DELETE_Friend", thisFriendID);
        apiString = "https://uni-builder-database.herokuapp.com/api/Users/";
        apiString = apiString + User_Data.data.UserID + "/Friends/" + otherID;
        Debug.Log("DELETE Request at: " + apiString);
        StartCoroutine(DeleteRequest(apiString));
    }

    IEnumerator PostRequest(string targetAPI, string data, string type = "POST") {
        byte[] rawData = System.Text.Encoding.UTF8.GetBytes(data);

        UnityWebRequest uwr = UnityWebRequest.Put(targetAPI, rawData);
        uwr.method = type;
        uwr.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("Sending the data ");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            // The POST request also returns the object it entered into the database.
            string raw = uwr.downloadHandler.text;
            Debug.Log("You've added a new friend");
            AddToFriendsList(friendId.text, friendUserName.text, int.Parse(friendExp.text));
            addFriend.SetActive(false);
            removeFriend.SetActive(true);
        }
    }

    IEnumerator DeleteRequest(string targetAPI) {
        // Constructs and sends a DELETE request to the database to remove data
        UnityWebRequest uwr = UnityWebRequest.Delete(targetAPI);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            Debug.Log("Friend Deleted");
            DeleteFromFriendsList();
            removeFriend.SetActive(false);
            addFriend.SetActive(true);
        }
    }

    /* A method which removes a given player from the users friendslist - this is a QOL method which 
    lets the user immediately see that they have unfriended someone when they next visit the leaderboard as
    without it the friends list would not update until the next time the user accessed the friends scene */
    public void DeleteFromFriendsList() {
        int NumFriends = Friend_API_v2.friendslist.Count;
        int indexToRemove = 0;
        for (int i=0;i<NumFriends;i++) {
            if (otherID == Friend_API_v2.friendslist[i].UserId) {
                indexToRemove = i;
            }
        }
        Friend_API_v2.friendslist.RemoveAt(indexToRemove);
    }

    public void AddToFriendsList(string friendID, string friendUsername, int friendXP) {
        Friends newFriend = new Friends(friendID, friendUsername, friendXP);
        Friend_API_v2.friendslist.Add(newFriend);
    }
}
