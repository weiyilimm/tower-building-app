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
    public GameObject PopUpInternetFailure;
    public GameObject StartGameObject;
    public GameObject LoadingBar;
    public Button StartButton;
    public Button PopUpInternetButton;
    public Slider Slider;
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
        StartGameObject.SetActive(false);
        PopUpInternetFailure.SetActive(false);

        UnityWebRequest request = new UnityWebRequest ("http://google.com");
        yield return request.SendWebRequest();
        //Is not connected
        if (request.error != null){
            PopUpInternetFailure.SetActive(true);
            isConnected = false;
        }
        //Is connected
        else{
            LoadingBar.SetActive(true);
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