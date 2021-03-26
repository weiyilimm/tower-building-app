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
    
    void Start()
    {
        StartButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
        PopUpInternetButton.onClick.AddListener(() => StartCoroutine(checkInternet()));

    }
    
    IEnumerator checkInternet(){
        PopUpInternetFailure.SetActive(false);
        UnityWebRequest request = new UnityWebRequest ("http://google.com");
        yield return request.SendWebRequest();
        //Is not connected
        if (request.error != null){
            PopUpInternetFailure.SetActive(true);
            LoginPanel.SetActive(false);
        }
        //Is connected
        else
        {   
            PopUpInternetFailure.SetActive(false);
            LoginPanel.SetActive(true);
        }
    }


    

}