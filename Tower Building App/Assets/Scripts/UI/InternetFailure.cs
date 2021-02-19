using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InternetFailure : MonoBehaviour
{   
    public GameObject PopUpInternetFailure;
    public GameObject StartGameObject;
    public GameObject LoadingText;
    public Button StartButton;
    public Button PopUpInternetButton;
    private bool isConnected = false;
    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
        if (!isConnected){
            PopUpInternetButton.onClick.AddListener(() => StartCoroutine(checkInternet()));
        }
    }

    IEnumerator checkInternet(){
        StartGameObject.SetActive(false);
        LoadingText.SetActive(true);
        PopUpInternetFailure.SetActive(false);

        UnityWebRequest request = new UnityWebRequest ("http://google.com");
        yield return request.SendWebRequest();
        //Is not connected
        if (request.error != null){
            PopUpInternetFailure.SetActive(true);
            LoadingText.SetActive(false);
            isConnected = false;
        }
        //Is connected
        else{
            isConnected = true;
            SceneManager.LoadScene(1);
        }
    }

}
