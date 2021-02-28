using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using SimpleJSON;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{   
    //Login and register navigation bar
    public GameObject LoginRegisterNav;
    //Login panel 
    public GameObject LoginPanel;
    public TMP_InputField Email;
    public TMP_InputField Username;
    public GameObject InvalidUsernamePopUP;
    public GameObject InvalidEmailPopUP;
    public Button RegisterButton;
    public List<leaderboard_data> AllUsers = new List<leaderboard_data>();    
    private bool usernameIsValid = true;
    void Start()
    {   
        CreateRequest("GET_Users");
        RegisterButton.onClick.AddListener(() => Register());
    }

    //Email format using regex 
    public const string EmailFormat =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    //Check if the key in email is valid
    public static bool IsEmail(string email)
        {
            if (email != null){
                return Regex.IsMatch(email, EmailFormat);
            }
            else 
            {
                return false;
            }
        }

    public void Register(){
        foreach (leaderboard_data data in AllUsers){
            //Check if the username already exist
            if (data.UserName.ToLower() == Username.text.ToLower()){
                Debug.Log("Username has been taken");
                usernameIsValid = false;
            }
        }
        //If username can be used
        if (usernameIsValid){
            InvalidUsernamePopUP.SetActive(false);
        }
        //If username has been taken
        else{
            InvalidUsernamePopUP.SetActive(true);
            usernameIsValid = true;
        }
        //If the email syntax is correct
        if(IsEmail(Email.text)){
            InvalidEmailPopUP.SetActive(false);
        }
        //If the emeail syntax is not correct
        else{
            InvalidEmailPopUP.SetActive(true);
        }
    }

    void CreateRequest(string RequestType) {
        string apiString = "https://uni-builder-database.herokuapp.com/api/Users/";

        if (RequestType == "GET_Users"){ 
            StartCoroutine(GetRequest(apiString));
        }
    }

    IEnumerator GetRequest(string targetAPI) {
        Debug.Log(targetAPI);
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);

            // TRANSLATION CODE HERE
            TranslateToLeaderboard(raw);
        }
    }

    private void TranslateToLeaderboard(string rawJSON){ 

        JSONNode node;
        node = JSON.Parse(rawJSON);
        string userid;
        string username;
        int totalExp;
        
        for (int i=0; i<5; i++) {
            userid = JSON.Parse(node[i]["id"].Value);
            username = JSON.Parse(node[i]["userName"].Value);
            totalExp = JSON.Parse(node[i]["totalExp"].Value);
            leaderboard_data data = new leaderboard_data(userid, username, totalExp);
            AllUsers.Add(data);
        }
    }
}
