using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class AddDeleteFriend : MonoBehaviour {
    
    // Hardcoded Friend (till UUID can be pulled from button).
    public string otherID = "53aa0679-4a23-4287-ae97-1b0cdac376d7";

    public void AddFriend() {
        //Friends_API.CreateRequest("CREATE_Friend", thisFriendID);
        string apiString = "http://localhost:8080/api/Users/";
        apiString = apiString + User_Data.data.UserID + "/Friends/" + otherID;

        //FriendLink newFriend = new FriendLink(User_Data.data.UserID, otherID);
        //string data = JsonUtility.ToJson(newFriend);
        //StartCoroutine(PostRequest(apiString, data, "POST"));
    }

    public void DeleteFriend() {
        //Friends_API.CreateRequest("DELETE_Friend", thisFriendID);
        string apiString = "http://localhost:8080/api/Users/";
        apiString = apiString + User_Data.data.UserID + "/Friends/" + otherID;
        StartCoroutine(DeleteRequest(apiString));
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
            Debug.Log("You've added a new friend");
        }
    }

    IEnumerator DeleteRequest(string targetAPI) {
        Debug.Log(targetAPI);
        // Constructs and sends a DELETE request to the database to remove data
        UnityWebRequest uwr = UnityWebRequest.Delete(targetAPI);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            Debug.Log("Friend Deleted");
        }
    }
}
