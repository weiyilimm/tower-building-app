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
    private bool isAuthenticated = true;
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

    public void Register(){
        postRequest("Register");
    }

    //Post request for either Login or register
    void postRequest(string RequestType) {
        
        if (RequestType == "Login"){ 
            string apiString = "https://uni-builder-database.herokuapp.com/api/Auth/Login/";
            string jsonString = createLoginUserJSON();
            StartCoroutine(PostRequest(apiString, jsonString, RequestType));
        }
        if (RequestType == "Register"){ 
            if(IsEmail(RegisterEmail.text)){
                InvalidEmailPopUP.SetActive(false);
                string apiString = "https://uni-builder-database.herokuapp.com/api/Auth/SignUp/";
                string jsonString = createRegisterUserJSON();
                StartCoroutine(PostRequest(apiString, jsonString, RequestType));
            }
            else{
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
        byte[] rawJson = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest uwr = UnityWebRequest.Put(URL, rawJson);
        uwr.method = "POST";
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError) {
            Debug.Log("An Internal Server Error Was Encountered");
        } 
        else {
            if (type == "Login"){
                //Status code 404 = not found
                if (uwr.responseCode == 404){
                    Debug.Log("Either username or password is wrong");
                    isAuthenticated = false;
                }
                //Status code 201 = authenticated
                if(uwr.responseCode == 201){
                    Debug.Log("Correct credentials");
                    isAuthenticated = true;
                }
            }
            if (type == "Register"){
                //Status 500 code = email or username has already been taken
                if (uwr.responseCode == 500){
                    Debug.Log("Either email or username has already been taken");
                }
                //Status 201 code = created successfully
                if(uwr.responseCode == 201){
                    Debug.Log("Sign up successfully");
                }
            }
        }   
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