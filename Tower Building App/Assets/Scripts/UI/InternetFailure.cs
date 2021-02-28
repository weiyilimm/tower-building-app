using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class InternetFailure : MonoBehaviour
{   
    
    public Button StartButton;
    public Button PopUpInternetButton;
    public Slider Slider;
    public GameObject PopUpInternetFailure;
    public GameObject LoadingBarPanel;
    public GameObject LoginPanel;
    public GameObject NavBar;
    private bool isConnected = false;
    private AsyncOperation operation;
    
    void Start()
    {
        StartButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
        if (!isConnected){
            PopUpInternetButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
        }
    }
    
    IEnumerator checkInternet(){
        PopUpInternetFailure.SetActive(false);
        LoginPanel.SetActive(false);
        NavBar.SetActive(false);
        UnityWebRequest request = new UnityWebRequest ("http://google.com");
        yield return request.SendWebRequest();
        //Is not connected
        if (request.error != null){
            PopUpInternetFailure.SetActive(true);
            LoginPanel.SetActive(false);
            isConnected = false;
        }
        //Is connected
        else{
            LoadingBarPanel.SetActive(true);
            isConnected = true;
            StartCoroutine(LoadProgress());
        }
    }


    IEnumerator LoadProgress(){
        operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress/.9f);
            Slider.value = progress;
            yield return null;
        }
    }

}