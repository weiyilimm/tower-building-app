using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class InternetFailure : MonoBehaviour
{   
    public GameObject PopUpInternetFailure;
    public GameObject StartButton;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(checkInternet());
    }

    IEnumerator checkInternet(){
        UnityWebRequest request = new UnityWebRequest ("http://google.com");
        yield return request.SendWebRequest();
        //Is not connected
        if (request.error != null){
            StartCoroutine(loadSceneDelay(5));
            PopUpInternetFailure.SetActive(true);
            StartButton.SetActive(false);
        }
        //Is connected
        else{
            PopUpInternetFailure.SetActive(false);
            StartButton.SetActive(true);
        }
    }

    IEnumerator loadSceneDelay(int i)
    {
        yield return new WaitForSeconds(i);
        SceneManager.LoadScene(0);
    }
}
