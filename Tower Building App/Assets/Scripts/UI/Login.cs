using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
public class Login : MonoBehaviour
{   
    //Login and register navigation bar
    public GameObject LoginRegisterNav;
    //Login panel 
    public GameObject LoginPanel;
    public TMP_InputField Email;
    public GameObject InvalidUsernamePopUP;
    public GameObject InvalidEmailPopUP;
    public Button RegisterButton;
    private bool usernameValid = true;
    void Start()
    {
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
        foreach (leaderboard_data data in Leaderboard_API.LB_data_InOrder){
            //Check if the username already exist
            if (data.UserName.ToLower() == Email.text.ToLower()){
                usernameValid = false;
            }
        }
        //If username can be used
        if (usernameValid){
            InvalidUsernamePopUP.SetActive(false);
        }
        //If username has already taken
        else{
            InvalidUsernamePopUP.SetActive(true);
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
}
