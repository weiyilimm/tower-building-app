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


    void Start()
    {
        icon = GetComponent<Button>();
    }
    public void LoadPersonalBuildingData()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1){
            icon.image.overrideSprite = clickedImage;
        }
        else{
            icon.image.overrideSprite = clickedImage;
            User_Data.data.CreateRequest("GET_User");
            SceneManager.LoadScene(1);
        }

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");

        //turn filter off
        FindObjectOfType<ListenerPersist>().toggleFilterOn(false);
    }
}
