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
    //User
    public GameObject Friend;
    //Friend not found pop up
    public GameObject PopUp;
    public TextMeshProUGUI textXP;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textId;
    public TextMeshProUGUI rankText;
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
        
        foreach (leaderboard_data data in Leaderboard_API.LB_data_InOrder){
            //Check if the username same as the input field
            if (data.UserName.ToLower() == FindFriendInputField.text.ToLower()){
                rankText.text = "1";
                textName.text = data.UserName;
                textId.text = data.UserId;
                textXP.text = data.TotalExp.ToString();
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
