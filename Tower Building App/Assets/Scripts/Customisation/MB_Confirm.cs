using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MB_Confirm : MonoBehaviour{
    public void ConfirmButton(){
        // Gets the index from the array and appends it to the subjectname to make the name of the tower for look up in the dictionary
        // with the result of the look up, an integer index can be used on the user_data to get the corresponding entry for the building in the building_stats array
        // the entry is then updated to store the values currently in the temp variables

        for (int i=0; i<4; i++){
            User_Data.data.building_stats[i].primary_colour = User_Data.data.temp_data[i][0];
            User_Data.data.building_stats[i].secondary_colour = User_Data.data.temp_data[i][1];
            User_Data.data.building_stats[i].model = User_Data.data.temp_data[i][2];
            User_Data.data.building_stats[i].m_height = User_Data.data.temp_data[i][3];

            User_Data.data.temp_data[i][0] = -1;
            User_Data.data.temp_data[i][1] = -1;
            User_Data.data.temp_data[i][2] = 0;
            User_Data.data.temp_data[i][3] = 0;
        }

        SceneManager.LoadScene(1);
    }
}
