using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
// using System.Diagnostics;
using System;
public class Login : MonoBehaviour
{   
    //Login and register navigation bar
    public GameObject LoginRegisterNav;
    //Login panel 
    public GameObject LoginPanel;
    public TMP_InputField RegisterEmail;
    public TMP_InputField RegisterUsername;
    public TMP_InputField RegisterPassword;
    public TMP_InputField LoginUsername;
    public TMP_InputField LoginPassword;
    public GameObject InvalidUsernamePopUP;
    public GameObject InvalidEmailPopUP;
    public GameObject LoadingBarPanel;
    public GameObject LoginPopUp;
    public GameObject NavBarPanel;
    public Button LoginButton;
    public Button RegisterButton;
    public List<User> AllUsers = new List<User>();    
    private bool usernameIsValid = true;
    private bool isAuthenticated = false;
    public Slider Slider;
    private AsyncOperation operation;
    private string apiString = "https://uni-builder-database.herokuapp.com/api/Users/";
    void Start()
    {   
        CreateRequest("GET_Users");
        RegisterButton.onClick.AddListener(() => Register());
        LoginButton.onClick.AddListener(() => Authenticate());
        RegisterButton.onClick.AddListener(() => SignUp());
    }

    IEnumerator LoadProgress(){
        operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress/.9f);
            Slider.value = progress;
            yield return null;
        }
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
    

    public void Authenticate(){
        foreach (User data in AllUsers){
            if(LoginUsername.text.ToLower() == data.Username.ToLower()){
                if(LoginPassword.text == data.Password){
                    isAuthenticated = true;
                }
            }
        }
        if (isAuthenticated){
            LoginPopUp.SetActive(false);
            NavBarPanel.SetActive(false);
            LoadingBarPanel.SetActive(true);
            Debug.Log("correct credentials");
            StartCoroutine(LoadProgress());
        }
        else{
            LoginPopUp.SetActive(true);
            NavBarPanel.SetActive(true);
            Debug.Log("Either username or password is wrong");
        }
    }

    public void Register(){
        foreach (User data in AllUsers){
            //Check if the username already exist
            if (data.Username.ToLower() == RegisterUsername.text.ToLower()){
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
        if(IsEmail(RegisterEmail.text)){
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

    public void SignUp() {
        // Guid myuuid = Guid.NewGuid();
        // string uuidstr = myuuid.ToString();
        //Friends_API.CreateRequest("CREATE_Friend", thisFriendID);
        Debug.Log("POST Request at: " + apiString);

        // Creates dummy data since the request requires some to be built
        newUser user = new newUser(RegisterUsername.text,RegisterEmail.text,RegisterPassword.text,0);
        string data = JsonUtility.ToJson(user);
        StartCoroutine(PostRequest(apiString, data, "POST"));
        Debug.Log("Successfully registered");
    }

    IEnumerator PostRequest(string targetAPI, string data, string type = "POST") {
        byte[] rawData = System.Text.Encoding.UTF8.GetBytes(data);

        UnityWebRequest uwr = UnityWebRequest.Put(targetAPI, rawData);
        uwr.method = type;
        uwr.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("Sending the data ");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } else {
            // The POST request also returns the object it entered into the database.
            string raw = uwr.downloadHandler.text;
        }
    }

    private void TranslateToLeaderboard(string rawJSON){ 

        JSONNode node;
        node = JSON.Parse(rawJSON);
        string Username;
        string Password;
        
        for (int i=0; i<5; i++) {
            Username = JSON.Parse(node[i]["userName"].Value);
            Password = JSON.Parse(node[i]["password"].Value);
            User data = new User(Username, Password);
            AllUsers.Add(data);
        }
    }

}

public class User {
    public string Username;
    public string Password;

    public User(string username, string password) {
        Username = username;
        Password = password;
    }
}

public class newUser
{

    public string Username;
    public string EmailAddress;
    public string Password;
    public int TotalXP;
    public newUser(string username, string emailaddress, string password,int totalXP ){
        Username = username;
        EmailAddress = emailaddress;
        Password = password;
        TotalXP = totalXP;
    }
}