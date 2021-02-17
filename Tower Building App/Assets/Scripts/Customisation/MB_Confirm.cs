using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MB_Confirm : MonoBehaviour{
    public void ConfirmButton(){
        // Loop through the 4 int arrays which store the temporary data and update
        // the main buildings persistant data with these values
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
        
        // POST to User
        //User_Data.data.CreateRequest("UPDATE_User");

        SceneManager.LoadScene(1);
    }
}
