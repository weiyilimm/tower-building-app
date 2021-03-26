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
    // Loading bar slider
    public Slider Slider;
    private AsyncOperation operation;
    public GameObject LoadingBarPanel;
    // Start is called before the first frame update
    void Start()
    {   
        
        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "LeaderBoard"){
            // Get the main panel which just under canvas
            Transform Panel = FriendEntry.transform.parent.parent.parent.parent;
            // Get the loading panel
            LoadingBarPanel = Panel.Find("LoadingPanel").gameObject;
            // Get the slider inside loading panel
            Slider = LoadingBarPanel.transform.GetChild(0).gameObject.GetComponent<Slider>();
        }
        // Because the hierarychy is different from leaderboard scene
        else if(currentSceneName == "FriendList"){
            Transform Panel = FriendEntry.transform.parent.parent.parent.parent.parent;
            LoadingBarPanel = Panel.Find("LoadingPanel").gameObject;
            Slider = LoadingBarPanel.transform.GetChild(0).gameObject.GetComponent<Slider>();
        }

        Transform FriendList = FriendEntry.transform.parent;
        FriendName.onClick.AddListener(() => OthersWorld(FriendList));
    }

    IEnumerator LoadProgress(){
        operation = SceneManager.LoadSceneAsync(16);
        while (!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress/.9f);
            Slider.value = progress;
            yield return null;
        }
    }

    public void OthersWorld(Transform FriendList)
    {   
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
            StartCoroutine(GetRequest(apiString));
        }
    }

    IEnumerator GetRequest(string targetAPI) {
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            string raw = uwr.downloadHandler.text;
            User_Data.data.TranslateUserProfileJSON(raw);
            User_Data.data.TranslateBuildingJSON(raw);
            LoadingBarPanel.SetActive(true);
            StartCoroutine(LoadProgress());
        }
    }
}
