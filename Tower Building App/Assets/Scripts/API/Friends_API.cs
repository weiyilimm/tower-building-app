using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends_API : MonoBehaviour {
    
    List<Friends> friendslist = new List<Friends>();

    /*  JSON formatting - NOT YET FINAL!
        {"user":"uibivd,
         "friends": [
            {"id":cbjsfd, "userName":"BobertRoss"},
            ...
            {"id":jdyrit, "userName":"JumJumJr"}
        ]} 
    */

    void Start() {
        // GET request - get the friends list of the current User

        // Translate the data retrieved from the GET request

        // Display the data using the UI
    }

    // Update is called once per frame
    void Update() {
        
    }
}

public class Friends {
    string UserId;
    string UserName;

    public Friends(string ui, string un) {
        UserId = ui;
        UserName = un;
    }
}
