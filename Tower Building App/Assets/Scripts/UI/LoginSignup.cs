using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginSignup : MonoBehaviour
{   
    //Login and register navigation bar
    public GameObject LoginRegisterNav;
    //Login panel 
    public GameObject LoginPanel;
    public GameObject RegisterPanel;
    public GameObject LoginButtonObject;
    public GameObject RegisterButtonObject; 
    public TMP_InputField LoginUsername;
    public TMP_InputField LoginPassword;
    public TMP_InputField RegisterEmail;
    public TMP_InputField RegisterUsername;
    public TMP_InputField RegisterPassword;
    // A pop up to indicate the username is taken
    public GameObject InvalidUsernamePopUP;
    // A pop up to indicate the email is in wrong syntax
    public GameObject InvalidEmailPopUP;
    // A login button to authenticate user
    public Button LoginButton;
    // A sign up button to register as a new user
    public Button RegisterButton; 
    //Loading bar panel
    public GameObject LoadingBarPanel;
    //The slider in the loading bar
    public Slider Slider;
    private AsyncOperation operation;
    //To check whether the user is authenticated or not
    private bool isAuthenticated = false;
    void Start()
    {   
        LoginButton.onClick.AddListener(() => Login());
        RegisterButton.onClick.AddListener(() => Register());
    }

    /*
    A method for loading bar to make the loading bar looks good
    It will show the progress of the loading bar
    */
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

    //Check if the input email is in valid syntax
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

    public void Login(){
        //Trigger post request method for Login
        postRequest("Login");
    }

    public void Register(){
        postRequest("Register");
    }

    //To decide which post request 
    void postRequest(string RequestType) {
        //If the post request is for login
        if (RequestType == "Login"){ 
            //The API URL for sending login username and password
            string apiString = "https://uni-builder-database.herokuapp.com/api/Auth/Login/";
            //Call a method which convert username and password into json format
            string jsonString = createLoginUserJSON();
            //Post the specific json file to the URL
            StartCoroutine(PostRequest(apiString, jsonString, RequestType));
        }
        if (RequestType == "First_Login"){
            string apiString = "https://uni-builder-database.herokuapp.com/api/Auth/Login/";
            string jsonString = createLoginUserJSON();
            StartCoroutine(PostRequest(apiString, jsonString, RequestType));
        }
        //If the post request is for register
        if (RequestType == "Register"){ 
            //Check if the email is in valid syntax 
            if(IsEmail(RegisterEmail.text)){
                //Hide the invalid email pop up 
                InvalidEmailPopUP.SetActive(false);
                string apiString = "https://uni-builder-database.herokuapp.com/api/Auth/SignUp/";
                /*
                Convert the input to json
                e.g. username, email, password, totalXP(default)
                */
                string jsonString = createRegisterUserJSON();
                //Post the converted json to the URL
                StartCoroutine(PostRequest(apiString, jsonString, RequestType));
            }
            else{
                //If the email is invalid, show the pop up
                InvalidEmailPopUP.SetActive(true);
            }
        }
    }

    /*
    Get the Login Username and Password input
    Convert it into Json String format and return it 
    */
    private string createLoginUserJSON() {
        LoginUser LoginUserData = new LoginUser(LoginUsername.text, LoginPassword.text);
        string LoginUserJson = JsonUtility.ToJson(LoginUserData);
        return LoginUserJson;
    }

    /*
    Get the register username, password and email input
    Convert it into Json String format and return it
    */
    private string createRegisterUserJSON() {
        RegisterUser RegisterUserData = new RegisterUser(RegisterUsername.text, RegisterEmail.text, RegisterPassword.text, 0);
        string RegisterUserJson = JsonUtility.ToJson(RegisterUserData);
        return RegisterUserJson;
    }

    /*
    Unity web request post method
    Send the json data to the specific URL
    URL = API url which returns JSON content
    json = json string 
    type = to decide whether this method is for Login or register, only accept "Login" or "Register"
    */
    IEnumerator PostRequest(string URL, string json, string type) {
        //Convert json file to byte format inorder to be sent
        byte[] rawJson = System.Text.Encoding.UTF8.GetBytes(json);
        //Use unity put request to send the data to the URL
        UnityWebRequest uwr = UnityWebRequest.Put(URL, rawJson);
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } 
        else {
            string raw = uwr.downloadHandler.text;
            Debug.Log(raw);
            if (type == "Login"){
                //Status code 404 = not found
                if (uwr.responseCode == 404){
                    Debug.Log("Either username or password is wrong");
                    isAuthenticated = false;
                }
                //Status code 201 = authenticated
                if(uwr.responseCode == 200){
                    Debug.Log("Correct credentials");
                    isAuthenticated = true;
                    Initialise_UserData(raw);
                }
                checkAuthentication();
            }
            if (type == "Register"){
                //Status 500 code = email or username has already been taken
                if (uwr.responseCode == 500){
                    Debug.Log("Either email or username has already been taken");
                    InvalidUsernamePopUP.SetActive(true);
                }
                //Status 201 code = created successfully
                if(uwr.responseCode == 201){
                    Debug.Log("Sign up successfully");
                    InvalidUsernamePopUP.SetActive(false);
                    LoginPanel.SetActive(true);
                    RegisterPanel.SetActive(false);
                    LoginButtonObject.SetActive(false);
                    RegisterButtonObject.SetActive(true);
                    FirstTimeLogin();
                }
            }
            if (type == "First_Login") {
                yield return StartCoroutine(Populate_UserBuildings(raw));
                Initialise_UserData(raw);
            }
        }   
    }

    public void checkAuthentication() {
        /*
        If the user is authenticated,
        hide the Login Panel and Login/SignUp navigation bar
        show the loading bar and start loading to main scene
        */
        if (isAuthenticated){
            LoginPanel.SetActive(false);
            LoginRegisterNav.SetActive(false);
            LoadingBarPanel.SetActive(true);
            StartCoroutine(LoadProgress());
        }
        /*
        If the user is not authenticated
        show both Login Panel and Login/SignUp navigation bar
        */
        else{
            LoginPanel.SetActive(true);
            LoginRegisterNav.SetActive(true);
        }
    }

    // If the user is registering then proceed with generating their default buildings
    public void FirstTimeLogin() {
        LoginUsername.text = RegisterUsername.text;
        LoginPassword.text = RegisterPassword.text;
        postRequest("First_Login");
    }

    // Loops over the 12 default buildings in Unity and sends an 'instance' of them to the database
    // so that the newly registered user ha a list of their 12 buildings
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

    //Once the database has recognised the login attempt is the UUID of the user is sent
    // in a request to get the rest of the users profile data and their building data
    public void Initialise_UserData(string rawJSON) {
        JSONNode node;
        node = JSON.Parse(rawJSON);
        string userId = JSON.Parse(node["id"].Value);
        User_Data.data.UserID = userId;

        User_Data.data.TranslateUserJSON(rawJSON);

        User_Data.data.CreateRequest("GET_User");
        User_Data.data.CreateRequest("GET_Friends");
        User_Data.data.CreateRequest("GET_Leaderboard");
    }

}

public class LoginUser {
    public string username;
    public string password;

    public LoginUser(string name, string pass) {
        username = name;
        password = pass;
    }
}

public class RegisterUser {
    public string userName;
    public string password;
    public string email;
    public int totalexp;

    public RegisterUser(string name, string mail, string pass, int xp) {
        userName = name;
        email = mail;
        password = pass;
        totalexp = xp;
    }
}