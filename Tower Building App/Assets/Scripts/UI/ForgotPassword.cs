using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class ForgotPassword : MonoBehaviour
{   
    public Button FindAccountButton;
    public Button SubmitOTPButton;
    public Button GoBackButton;
    public Button ChangePasswordButton;
    public TMP_InputField EmailInput;
    public TMP_InputField OTPInput;
    public TMP_InputField PasswordInput1;
    public TMP_InputField PasswordInput2;
    public GameObject EmailPanel;
    public GameObject OTPPanel;
    public GameObject PasswordPanel;
    public GameObject LoginPanel;
    public GameObject InvalidOTPPopUp;
    public GameObject InvalidPasswordPopUp;
    public GameObject NoAccountFoundPopUp;
    public GameObject InvalidEmailPopUp;
    public GameObject WholeForgotPasswordPanel;
    // Start is called before the first frame update
    void Start()
    {
        FindAccountButton.onClick.AddListener(() => PostEmail());
        SubmitOTPButton.onClick.AddListener(() => PostOTP());
        ChangePasswordButton.onClick.AddListener(() => PostPassword());
        GoBackButton.onClick.AddListener(() => GoBackEmail());
    }

    // Return to enter email page
    public void GoBackEmail(){
        EmailPanel.SetActive(true);
        OTPPanel.SetActive(false);
    }

    // Send email to the server
    public void PostEmail(){
        string apiString = "https://uni-builder-database.herokuapp.com/api/Auth/Email/";
        // Create Email Json String to be posted to server
        string jsonString = createEmailJson();
        // Taking the method from Login Sign Up to check whether the email is in correct syntax
        if(LoginSignup.IsEmail(EmailInput.text)){
            StartCoroutine(PostRequest(apiString, jsonString, "Email"));
        }
        else{
            NoAccountFoundPopUp.SetActive(false);
            InvalidEmailPopUp.SetActive(true);
        }
    }

    // Send OTP to the server
    public void PostOTP(){
        string apiString = "https://uni-builder-database.herokuapp.com/api/Auth/validateOTP/";
        // Create OTP Json String to be posted to server
        string jsonString = createOTPJson();
        StartCoroutine(PostRequest(apiString, jsonString, "OTP"));
    }

    // Send new password to the server
    public void PostPassword(){
        string apiString = "https://uni-builder-database.herokuapp.com/api/Auth/resetPassword/";
        // Create Password Json String to be posted to server
        string jsonString = createPasswordJSON();
        // Check if both keyed in password are identical
        if (PasswordInput1.text == PasswordInput2.text){
            StartCoroutine(PostRequest(apiString, jsonString, "Password"));
        }
        else{
            InvalidPasswordPopUp.SetActive(true);
        }
    }

    /*
    Get the email input
    Convert it into Json String format and return it 
    */
    private string createEmailJson() {
        // Create EmailUser object which has email string to be posted 
        EmailUser EmailData = new EmailUser(EmailInput.text);
        string EmailDataJson = JsonUtility.ToJson(EmailData);
        return EmailDataJson;
    }

    /*
    Get the email and otp input
    Convert it into Json String format and return it 
    */
    private string createOTPJson() {
        // Create OTPUser object which has both email and otp strings to be posted 
        OTPUser OTPData = new OTPUser(EmailInput.text, OTPInput.text);
        string OTPDataJson = JsonUtility.ToJson(OTPData);
        return OTPDataJson;
    }

    /*
    Get the email, otp and password input
    Convert it into Json String format and return it 
    */
    private string createPasswordJSON() {
        // Create PasswordUser object which has email, OTP and password string to be posted 
        PasswordUser PasswordData = new PasswordUser(EmailInput.text, OTPInput.text, PasswordInput1.text);
        string PasswordDataJson = JsonUtility.ToJson(PasswordData);
        return PasswordDataJson;
    }




    /*
    Unity web request post method
    Send the json data to the specific URL
    URL = API url which returns JSON content
    json = json string 
    type = to decide whether this method is for posting which data
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
            if (type == "Email"){
                //Status code 200 = found email successfully 
                if(uwr.responseCode == 200){
                    Debug.Log("OTP has been sent to email");
                    /*
                    Hide everythign and show OTPpanel which require user to enter OTP code
                    */
                    EmailPanel.SetActive(false);
                    OTPPanel.SetActive(true);
                    InvalidOTPPopUp.SetActive(false);
                    NoAccountFoundPopUp.SetActive(false);
                    InvalidEmailPopUp.SetActive(false);
                }
                //Status code 500 = user associated with the email not found
                if (uwr.responseCode == 500){
                    Debug.Log("Email not found");
                    // If the account cannot be found, show the popup to indicate user
                    NoAccountFoundPopUp.SetActive(true);
                    InvalidEmailPopUp.SetActive(false);
                }
            }
            if (type == "OTP"){
                //Status 200 code = correct OTP
                if (uwr.responseCode == 200){
                    // Show the password panel which require user to key in new password
                    Debug.Log("Correct OTP");
                    OTPPanel.SetActive(false);
                    PasswordPanel.SetActive(true);
                }
                //Status 401 code = wrong OTP
                if(uwr.responseCode == 401){
                    // Show the pop up indicate user the OTP is incorrect
                    Debug.Log("Wrong OTP"); 
                    InvalidOTPPopUp.SetActive(true);
                }
            }
            if (type == "Password"){
                //Status 200 code = changed password successfully
                if (uwr.responseCode == 200){
                    // Hide the whole forgot password panel and show login panel for user to login
                    WholeForgotPasswordPanel.SetActive(false);
                    LoginPanel.SetActive(true);
                    Debug.Log("Changed password successfully");
                }
            }
        }   
    }
}


public class EmailUser {
    public string email;

    public EmailUser(string mail) {
        email = mail;
    }
}

public class OTPUser {
    public string email;
    public string OTP;

    public OTPUser(string mail, string otp) {
        email = mail;
        OTP = otp;
    }
}

public class PasswordUser {
    public string email;
    public string OTP;
    public string password;
    

    public PasswordUser(string mail, string otp, string pass) {
        email = mail;
        OTP = otp;
        password = pass;
    }
}
