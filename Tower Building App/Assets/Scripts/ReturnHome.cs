using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnHome : MonoBehaviour {
    
    public Sprite normalImage;
    public Sprite clickedImage;
    public Button icon;

    public CameraMovePhone movement;


    void Start() {
        icon = GetComponent<Button>();
    }

    public void Building() {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            icon.image.overrideSprite = clickedImage;
        } else {
            User_Data.data.CreateRequest("GET_User");
            SceneManager.LoadScene(1);
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
            User_Data.data.CreateRequest("GET_User");
            SceneManager.LoadScene(2);
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
            User_Data.data.CreateRequest("GET_User");
            SceneManager.LoadScene(3);
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
            User_Data.data.CreateRequest("GET_User");
            SceneManager.LoadScene(4);
            icon.image.overrideSprite = clickedImage;
        }

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }
}
