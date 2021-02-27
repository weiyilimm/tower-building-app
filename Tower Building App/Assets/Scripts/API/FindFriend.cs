using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FindFriend : MonoBehaviour
{
    //Search bar Find Friend Input Field
    public TMP_InputField FindFriendInputField;
    //Search Button next to the bar
    public Button Search;
    //Username
    public TextMeshProUGUI TextName;
    //UserXP
    public TextMeshProUGUI TextXP;
    //User
    public GameObject Friend;
    //Friend not found pop up
    public GameObject PopUp;
    private bool found = false;
    void Start()
    {   
        Search.onClick.AddListener(() => GetUserName());
    }

    void GetUserName()
    {   
        /*
        Loop through the list from leaderboard
        As the leaderboard is getting all users, thus we can loop through all users
        Might expect Leaderboard_API.LB_data only store 50 users in the future
        */
        foreach (leaderboard_data data in Leaderboard_API.LB_data){
            //Check if the username same as the input field
            if (data.UserName.ToLower() == FindFriendInputField.text.ToLower()){
                TextName.text = data.UserName;
                TextXP.text = data.TotalExp.ToString();
                found = true;
            }
        }
        if (found){
            found = false;
            Friend.SetActive(true);
            PopUp.SetActive(false);
        }
        else{
            Friend.SetActive(false);
            PopUp.SetActive(true);
        }
        
    }
}
