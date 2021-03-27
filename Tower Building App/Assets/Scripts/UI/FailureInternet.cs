using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class FailureInternet : MonoBehaviour
{   
    
    public Button MainBuildingButton;
    public Button StopWatchButton;
    public Button FriendButton;
    public Button LeaderboardButton;
    
    public Button PopUpInternetButton;
    public GameObject PopUpInternetFailure;
    public static bool isConnected = true;
    
    void Start()
    {
        MainBuildingButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
        StopWatchButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
        FriendButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
        LeaderboardButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
        PopUpInternetButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
    }
    
    IEnumerator checkInternet(){
        PopUpInternetFailure.SetActive(false);
        UnityWebRequest request = new UnityWebRequest ("http://google.com");
        yield return request.SendWebRequest();
        //Is not connected
        if (request.error != null){
            PopUpInternetFailure.SetActive(true);
            isConnected = false;
        }
        //Is connected
        else
        {   
            PopUpInternetFailure.SetActive(false);
            isConnected = true;
        }
    }


    

}