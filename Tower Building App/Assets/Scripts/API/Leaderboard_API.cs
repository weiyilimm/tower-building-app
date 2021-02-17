using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard_API : MonoBehaviour {
    
    public List<leaderboard_data> LB_data = new List<leaderboard_data>();
    
    /*  JSON formatting - NOT YET FINAL!
        {"users": [
            {"id":sgisvi, "userName":"BobertRoss", totalExp: 25000},
            {"id":fsiufb, "userName":"RobertBoss", totalExp: 16000},
            {"id":oncsbd, "userName":"JimJimSr", totalExp: 12000},
            {"id":mbcuse, "userName":"JumJumJr", totalExp: 5000},
            ...
            {"id":sgisvi, "userName":"user1", totalExp: 1000},
        ]}
    */

    void Start() {
        // GET Request - Top 50 users by totalExp
        // CreateRequest("GET_Leaderboard");

        // Translate the data retrieved from the GET request

        // Display the data using the UI
    }

    // Update is called once per frame
    void Update() {
        
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
