using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class ViewOther : MonoBehaviour
{   
    public GameObject FriendEntry;
    //public GameObject FriendList;
    //public GameObject LoadingText;
    public Button FriendName; 
    public string apiString = "https://uni-builder-database.herokuapp.com/api/Users/";
    public TextMeshProUGUI friendId;

    // Start is called before the first frame update
    void Start()
    {
        Transform FriendList = FriendEntry.transform.parent;
        //LoadingText = gameObject.Find("LoadingText");
        FriendName.onClick.AddListener(() => OthersWorld(FriendList));
    }

    public void OthersWorld(Transform FriendList)
    {   
        //LoadingText.SetActive(true);
        //FriendList.gameObject.SetActive(false);

        CreateRequest();
    }

    public void CreateRequest() {
        string friendID = friendId.text;
        /*
        If the friendID is empty which means is the user himself
        User can only visit other people world not his world
        */
        if(friendID != ""){
            apiString = apiString + friendID + "/Buildings/";
            Debug.Log(apiString);
            StartCoroutine(GetRequest(apiString));
        }
    }

    IEnumerator GetRequest(string targetAPI) {
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        Debug.Log("Sending request...");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);
            User_Data.data.TranslateUserProfileJSON(raw);
            User_Data.data.TranslateBuildingJSON(raw);
            SceneManager.LoadScene(16);
        }
    }
}
