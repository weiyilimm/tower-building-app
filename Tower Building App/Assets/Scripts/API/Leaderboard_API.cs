using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.Linq;

[System.Serializable]
public class Leaderboard_API : MonoBehaviour {
    
    //An ordered list which stores the username, userid and global_xp of a user
    public static List<leaderboard_data> LB_data = new List<leaderboard_data>();
    public static List<leaderboard_data> LB_data_InOrder = new List<leaderboard_data>();        
    //Use the prefab participant
    public Transform Participant;
    //Leaderboardlist to be able to store all instances
    //Input field from search
    public TMP_InputField SearchInputField;
    //Leaderboard transform to get the children objects
    public Transform LeaderBoardList;
    private GameObject firstTrophy;
    private GameObject secondTrophy;
    private GameObject thirdTrophy;
    private GameObject rankingText;
    //User XP for each instances 
    private TextMeshProUGUI textXP;
    //User name for each instances
    private TextMeshProUGUI textName;
    private TextMeshProUGUI textId;
    //User rank for each instances
    private TextMeshProUGUI rankText;
    
    void Start() {
        // GET Request - Top 50 users by totalExp then
        // Translate the data retrieved from the GET request

        CreateRequest("GET_Leaderboard");
        /* 
        If the user type something on the search bar 
        Trigger the FilterUser function
        */
        SearchInputField.onValueChanged.AddListener(delegate {FilterUser(); });
    }
    public void FilterUser() {
        // Loop through each single user in the leaderboard list
        for (int i = 0; i < LeaderBoardList.childCount; i++)
        {   
            //Use child as temporary variable for each user
            Transform child = LeaderBoardList.GetChild(i);
            //If the user input is empty then show every player
            if(SearchInputField.text == "" ){
                child.gameObject.SetActive(true);
            }
            else{
                //If the user input is not empty, then get the username
                TextMeshProUGUI userName = child.Find("NameText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                /*
                As the default are case sensitive
                Change username to lower and user input to lower
                e.g. If user type "richard", the leader will still show "Richard" user
                */
                string userInputLower = SearchInputField.text.ToLower();
                string userNameLower = userName.text.ToLower();
                //If the username contains the user input
                if (userNameLower.Contains(userInputLower)){
                    //Show the specific user according to the user input
                    child.gameObject.SetActive(true);
                }
                //If the username does not contains the user input
                else{
                    //Hide it
                    child.gameObject.SetActive(false);
                }
                
            }
        }
    }

    public void CreateRequest(string RequestType) {
        string apiString = "https://uni-builder-database.herokuapp.com/api/Users/";

        if (RequestType == "GET_Leaderboard") { 
            StartCoroutine(GetRequest(apiString));
        }
    }

    IEnumerator GetRequest(string targetAPI) {
        Debug.Log(targetAPI);
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        Debug.Log("Sending Request");
        yield return uwr.SendWebRequest();
        Debug.Log("Reuqest returned");

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
        //using (StreamReader r = new StreamReader(rawJSON)) {
        //    //read in the json
        //    Debug.Log("Passed the stream reader");
        //    string json = r.ReadToEnd();
        //    Debug.Log("Passed the readToEnd");
        //    //reformat the json into dictionary style convention
        //    node = JSON.Parse(json);
        //}

        node = JSON.Parse(rawJSON);
        int NUM_USERS = node.Count;
        
        string userid;
        string username;
        int totalExp;
        
        LB_data.Clear();

        for (int i=0; i<NUM_USERS; i++) {
            userid = JSON.Parse(node[i]["id"].Value);
            username = JSON.Parse(node[i]["userName"].Value);
            totalExp = JSON.Parse(node[i]["totalExp"].Value);

            leaderboard_data data = new leaderboard_data(userid, username, totalExp);
            LB_data.Add(data);
        }
        displayData();
    }

    public void displayData() {
        //Create another list which sort based on the total XP
        LB_data_InOrder  = LB_data.OrderByDescending(x => x.TotalExp).ToList();
        foreach (leaderboard_data data in LB_data_InOrder)
        {   
            int index = LB_data_InOrder.IndexOf(data);
            firstTrophy = Participant.Find("TrophyFirst").gameObject;
            secondTrophy = Participant.Find("TrophySecond").gameObject;
            thirdTrophy = Participant.Find("TrophyThird").gameObject;
            rankingText = Participant.Find("RankingText").gameObject;
            //If user is the champion
            if (index == 0)
            {
                firstTrophy.SetActive(true);
                secondTrophy.SetActive(false);
                thirdTrophy.SetActive(false);
                rankingText.SetActive(false);
            }
            //If user is second place
            else if (index == 1)
            {
                firstTrophy.SetActive(false);
                secondTrophy.SetActive(true);
                thirdTrophy.SetActive(false);
                rankingText.SetActive(false);
            }
            //If user is third place
            else if (index == 2)
            {
                firstTrophy.SetActive(false);
                secondTrophy.SetActive(false);
                thirdTrophy.SetActive(true);
                rankingText.SetActive(false);
            }
            else
            {
                firstTrophy.SetActive(false);
                secondTrophy.SetActive(false);
                thirdTrophy.SetActive(false);
                rankingText.SetActive(true);

            }
            //Create instance(user) as each data loop
            var instance = Instantiate(Participant);
            //Set their parent to leaderboardlist
            instance.SetParent(LeaderBoardList, false);
            textName = instance.Find("NameText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            textId = instance.Find("IdText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            textXP = instance.Find("XPText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText = instance.Find("RankingText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText.text = (LB_data_InOrder.IndexOf(data) + 1).ToString() + ".";
            textName.text = data.UserName;
            textId.text = data.UserId;
            textXP.text = data.TotalExp.ToString();

            checkForFriend(instance, data);
            // Debug.Log(LB_data.IndexOf(data));
            // Debug.Log(data.UserName + " " + data.TotalExp);
        }
    }

    public void checkForFriend(Transform instance, leaderboard_data data) {
        //check if the data is in the friends list or not
        foreach (Friends friend_data in Friend_API_v2.friendslist) {
            if (friend_data.UserName == data.UserName) {
                Debug.Log("Player was in your friends list");
                GameObject plus = instance.Find("RawImage").gameObject;
                GameObject tick = instance.Find("RawImage (1)").gameObject;
                plus.SetActive(false);
                tick.SetActive(true);
            }
        }
        if (data.UserName == User_Data.data.Username) {
            /*
            if the leaderboard showing the current user
            assign their IdText to be empty
            so the current wont be able to visit his/her own world
            */
            instance.Find("IdText").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "";
            GameObject plus = instance.Find("RawImage").gameObject;
            GameObject tick = instance.Find("RawImage (1)").gameObject;
            plus.SetActive(false);
            tick.SetActive(false);
        }
    }
}

public class leaderboard_data {
    public string UserId;
    public string UserName;
    public int TotalExp;

    public leaderboard_data(string ui, string un, int xp) {
        UserId = ui;
        UserName = un;
        TotalExp = xp;
    }
}
