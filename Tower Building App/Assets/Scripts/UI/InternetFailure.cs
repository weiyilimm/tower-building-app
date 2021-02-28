using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class InternetFailure : MonoBehaviour
{   
    
    public Button StartButton;
    public Button PopUpInternetButton;
    public GameObject PopUpInternetFailure;
    public GameObject LoginPanel;
    public GameObject NavBar;
    private bool isConnected = true;
    
    
    void Start()
    {
        StartButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
        if (!isConnected){
            PopUpInternetButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
        }
    }
    
    IEnumerator checkInternet(){
        PopUpInternetFailure.SetActive(false);
        UnityWebRequest request = new UnityWebRequest ("http://google.com");
        yield return request.SendWebRequest();
        //Is not connected
        if (request.error != null){
            PopUpInternetFailure.SetActive(true);
            LoginPanel.SetActive(false);
            NavBar.SetActive(false);
            isConnected = false;
        }
        //Is connected
        else
        {   
            PopUpInternetFailure.SetActive(false);
            isConnected = true;
            LoginPanel.SetActive(true);
            NavBar.SetActive(true);
        }
    }


    

}