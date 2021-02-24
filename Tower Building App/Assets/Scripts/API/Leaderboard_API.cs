using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Leaderboard_API : MonoBehaviour {
    
    //An ordered list which stores the username, userid and global_xp of a user
    public List<leaderboard_data> LB_data = new List<leaderboard_data>();
    //Use the prefab participant
    public Transform Participant;
    //Leaderboardlist to be able to store all instances
    public Transform LeaderBoardList;
    private GameObject firstTrophy;
    private GameObject secondTrophy;
    private GameObject thirdTrophy;
    private GameObject rankingText;
    private TextMeshProUGUI textXP;
    private TextMeshProUGUI textName;
    private TextMeshProUGUI rankText;

    void Start() {
        // GET Request - Top 50 users by totalExp then
        // Translate the data retrieved from the GET request

        /* CreateRequest("GET_Leaderboard"); COMMENTED OUT FOR NOW TO TEST DISPLAYING DATA */
        CreateRequest("GET_Leaderboard");
    }

    void CreateRequest(string RequestType) {
        string apiString = "https://uni-builder-database.herokuapp.com/api/Users/";

        if (RequestType == "GET_Leaderboard") { 
            StartCoroutine(GetRequest(apiString));
        }
    }

    IEnumerator GetRequest(string targetAPI) {
        Debug.Log(targetAPI);
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        Debug.Log("Got the data");
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
        //using (StreamReader r = new StreamReader(rawJSON)) {
        //    //read in the json
        //    Debug.Log("Passed the stream reader");
        //    string json = r.ReadToEnd();
        //    Debug.Log("Passed the readToEnd");
        //    //reformat the json into dictionary style convention
        //    node = JSON.Parse(json);
        //}

        node = JSON.Parse(rawJSON);
        Debug.Log(node);

        string userid;
        string username;
        int totalExp;
        
        for (int i=0; i<5; i++) {
            userid = JSON.Parse(node[i]["id"].Value);
            username = JSON.Parse(node[i]["userName"].Value);
            totalExp = JSON.Parse(node[i]["totalExp"].Value);

            leaderboard_data data = new leaderboard_data(userid, username, totalExp);
            LB_data.Add(data);
        }
        displayData();
    }

    public void displayData() {
        //Print out the data for the five users in the leaderboard
        foreach (leaderboard_data data in LB_data)
        {
            int index = LB_data.IndexOf(data);
            firstTrophy = Participant.Find("TrophyFirst").gameObject;
            secondTrophy = Participant.Find("TrophySecond").gameObject;
            thirdTrophy = Participant.Find("TrophyThird").gameObject;
            rankingText = Participant.Find("RankingText").gameObject;
            if (index == 0)
            {
                firstTrophy.SetActive(true);
                secondTrophy.SetActive(false);
                thirdTrophy.SetActive(false);
                rankingText.SetActive(false);
            }
            else if (index == 1)
            {
                firstTrophy.SetActive(false);
                secondTrophy.SetActive(true);
                thirdTrophy.SetActive(false);
                rankingText.SetActive(false);
            }
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
            textXP = instance.Find("XPText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText = instance.Find("RankingText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText.text = (LB_data.IndexOf(data) + 1).ToString() + ".";
            textName.text = data.UserName;
            textXP.text = data.TotalExp.ToString();
            Debug.Log(LB_data.IndexOf(data));
            Debug.Log(data.UserName + " " + data.TotalExp);
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
