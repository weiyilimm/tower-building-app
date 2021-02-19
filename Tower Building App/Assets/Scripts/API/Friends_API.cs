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
        // GET request - Given a userID return all entries in the FRIENDS table with that userID in the 'USER' column

        // Translate the data retrieved from the GET request

        // Display the data using the UI
    }

    // DELETE Request - Given two userIDs - delete the entry where 'MY' id is in the 'USER' column 
    // and 'THEIR' id is in the 'FRIEND' column
}

public class Friends {
    string UserId;
    string UserName;

    public Friends(string ui, string un) {
        UserId = ui;
        UserName = un;
    }
}
