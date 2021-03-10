using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DummyUserRegister : MonoBehaviour
{   
    private List<string> DummyJsonString = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        postDummyUser();
    }

    
    private void postDummyUser(){
        string apiString = "https://uni-builder-database.herokuapp.com/api/Auth/SignUp/";
        createDummyUserJSON();
        foreach (string dummy in DummyJsonString){
            StartCoroutine(DummyUserPostRequest(apiString, dummy));
        }
    }

    private void createDummyUserJSON() {
        RegisterUser dummy1 = new RegisterUser("Rueben", "2383199M@student.gla.ac.uk", "ReallySafe", 0);
        RegisterUser dummy2 = new RegisterUser("Joe", "2377990S@student.gla.ac.uk", "HeyWhyNot", 1000);
        RegisterUser dummy3 = new RegisterUser("Vedant", "2367329d@student.gla.ac.uk", "HeyYa", 10000);
        RegisterUser dummy4 = new RegisterUser("Cameron", "2395521M@student.gla.ac.uk", "BadPasword", 100000);
        RegisterUser dummy5 = new RegisterUser("Wei", "2474554L@student.gla.ac.uk", "OkieDokie", 1000000);
        DummyJsonString.Add(JsonUtility.ToJson(dummy1));
        DummyJsonString.Add(JsonUtility.ToJson(dummy2));
        DummyJsonString.Add(JsonUtility.ToJson(dummy3));
        DummyJsonString.Add(JsonUtility.ToJson(dummy4));
        DummyJsonString.Add(JsonUtility.ToJson(dummy5));
    }

    IEnumerator DummyUserPostRequest(string URL, string json) {
        byte[] rawJson = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest uwr = UnityWebRequest.Put(URL, rawJson);
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } 
        else {
            if (uwr.responseCode == 500){
                Debug.Log("Dummy users have already been created");
            }
            else{
                Debug.Log("Dummy users have been created successfully");
            }
        }
    }
}
