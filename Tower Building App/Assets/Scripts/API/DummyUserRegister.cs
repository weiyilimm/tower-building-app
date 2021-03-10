using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class DummyUserRegister : MonoBehaviour
{   
    private List<string> DummyJsonString = new List<string>();
    private List<string> DummyLoginJsonString = new List<string>();

    // Start is called before the first frame update
    void Start() {
        postDummyUser();
    }

    
    private void postDummyUser(){
        string apiString = "https://uni-builder-database.herokuapp.com/api/Auth/SignUp/";
        createDummyRegisterJSON();
        createDummyLoginJSON();
        // Counter for identifing which index of DummyJsonString we are on so we 
        // can retrive the matching entry in DummyLoginJsonString
        int counter_index = 0;
        foreach (string dummy in DummyJsonString){
            StartCoroutine(DummyUserPostRequest(apiString, dummy, counter_index));
            counter_index += 1;
        }
    }

    private void createDummyRegisterJSON() {
        RegisterUser dummy1 = new RegisterUser("Rueben", "2383199M@student.gla.ac.uk", "ReallySafe", 0);
        RegisterUser dummy2 = new RegisterUser("Joe", "2377990S@student.gla.ac.uk", "HeyWhyNot", 1000);
        RegisterUser dummy3 = new RegisterUser("Vedant", "2367329d@student.gla.ac.uk", "HeyYa", 10000);
        RegisterUser dummy4 = new RegisterUser("Cameron", "2395521M@student.gla.ac.uk", "BadPassword", 100000);
        RegisterUser dummy5 = new RegisterUser("Wei", "2474554L@student.gla.ac.uk", "OkieDokie", 1000000);
        DummyJsonString.Add(JsonUtility.ToJson(dummy1));
        DummyJsonString.Add(JsonUtility.ToJson(dummy2));
        DummyJsonString.Add(JsonUtility.ToJson(dummy3));
        DummyJsonString.Add(JsonUtility.ToJson(dummy4));
        DummyJsonString.Add(JsonUtility.ToJson(dummy5));
    }

    private void createDummyLoginJSON() {
        LoginUser dummy1 = new LoginUser("Rueben", "ReallySafe");
        LoginUser dummy2 = new LoginUser("Joe", "HeyWhyNot");
        LoginUser dummy3 = new LoginUser("Vedant", "HeyYa");
        LoginUser dummy4 = new LoginUser("Cameron", "BadPassword");
        LoginUser dummy5 = new LoginUser("Wei", "OkieDokie");
        DummyLoginJsonString.Add(JsonUtility.ToJson(dummy1));
        DummyLoginJsonString.Add(JsonUtility.ToJson(dummy2));
        DummyLoginJsonString.Add(JsonUtility.ToJson(dummy3));
        DummyLoginJsonString.Add(JsonUtility.ToJson(dummy4));
        DummyLoginJsonString.Add(JsonUtility.ToJson(dummy5));
    }

    IEnumerator DummyUserPostRequest(string URL, string json, int index) {
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
                // If this is the first time these users have been added to the database then
                // we need to generate a set of default buildings for each user
                StartCoroutine(DummyUserLoginRequest(DummyLoginJsonString[index]));
            }
        }
    }

    IEnumerator DummyUserLoginRequest(string json) {
        yield return new WaitForSeconds(3);
        
        string apiString = "https://uni-builder-database.herokuapp.com/api/Auth/Login/";
        byte[] rawJson = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest uwr = UnityWebRequest.Put(apiString, rawJson);
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } 
        else {
            if (uwr.responseCode == 404){
                Debug.Log("Dummy user not found");
            }
            else{
                Debug.Log("Dummy user found");
                string raw = uwr.downloadHandler.text;
                //The user was found in the database so proceed with generating the default buildings like in
                // the login/sign up scene wher each user loops over the 12 default buildings in Unity and adds an
                // 'instance' of it to their own list of buildings
                yield return StartCoroutine(Populate_UserBuildings(raw));
            }
        }
    }

    IEnumerator Populate_UserBuildings(string rawJSON) {
        JSONNode node;
        node = JSON.Parse(rawJSON);
        string userId = JSON.Parse(node["id"].Value);
        User_Data.data.UserID = userId;

        for (int index=0; index<12; index++) {
            User_Data.data.CreateRequest("UPDATE_User_Building", index);
        }
        yield return new WaitForSeconds(1);
        yield return null;
    }
}
