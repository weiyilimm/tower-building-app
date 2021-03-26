using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ReturnHome : MonoBehaviour {
    
    public Sprite normalImage;
    public Sprite clickedImage;
    public Button icon;

    public CameraMovePhone movement;
    public string apiString = "https://uni-builder-database.herokuapp.com/api/Users/";

    //When one of the navigation buttons is clicked the code will generate a GET
    // request from the database so that the current users data is loaded once again
    // then the screen will switch to the desired scene
    void Start() {
        icon = GetComponent<Button>();
    }

    public void Building() {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            icon.image.overrideSprite = clickedImage;
        } else {
            CreateRequest(1);
            icon.image.overrideSprite = clickedImage;
        }

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");

        //turn filter off
        FindObjectOfType<ListenerPersist>().toggleFilterOn(false);
    }

    //Timing Clock Footer
    public void TimingClock() {
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            icon.image.overrideSprite = clickedImage;
        } else{
            CreateRequest(2);
            icon.image.overrideSprite = clickedImage;
        }

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");

        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }

    //FriendList Footer
    public void FriendList() {
        if (SceneManager.GetActiveScene().buildIndex == 3) {
            icon.image.overrideSprite = clickedImage;
        } else {
            CreateRequest(3);
            icon.image.overrideSprite = clickedImage;
        }

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }

    //LeaderBoard Footer
    public void LeaderBoard() {
        if (SceneManager.GetActiveScene().buildIndex == 4) {
            icon.image.overrideSprite = clickedImage;
        } else {
            CreateRequest(4);
            icon.image.overrideSprite = clickedImage;
        }

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }

    public void CreateRequest(int sceneID) {
        apiString = apiString + User_Data.data.UserID + "/Buildings/";
        //Debug.Log("Returning to home: " + apiString);
        StartCoroutine(GetRequest(apiString, sceneID));
    }

    IEnumerator GetRequest(string targetAPI, int sceneID) {
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        Debug.Log("Sending request...");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            string raw = uwr.downloadHandler.text;
            //Debug.Log("Received: " + raw);
            User_Data.data.TranslateUserProfileJSON(raw);
            User_Data.data.TranslateBuildingJSON(raw);
            SceneManager.LoadScene(sceneID);
        }
    }
}
