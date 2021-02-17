using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InternetFailure : MonoBehaviour
{   
    public GameObject PopUpInternetFailure;
    public GameObject StartButton;
    // Start is called before the first frame update
    void Update()
    {
        StartCoroutine(checkInternet((isNotConnected) => 
        {
            if(isNotConnected){
                PopUpInternetFailure.SetActive(true);
                StartButton.SetActive(false);
            }
            else{
                PopUpInternetFailure.SetActive(false);
                StartButton.SetActive(true);
            }
        }));
    }

    IEnumerator checkInternet(Action<bool> action){
        WWW www = new WWW("https://www.google.com/");
        yield return www;
        if (www.error != null){
            action(true);
        }
        else{
            action(false);
        }
    }
}
