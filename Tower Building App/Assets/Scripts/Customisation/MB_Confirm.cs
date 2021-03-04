using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MB_Confirm : MonoBehaviour{
    public void ConfirmButton(){
        // Loop through the 4 int arrays which store the temporary data and update
        // the main buildings persistant data with these values
        int model_offset = 0;
        for (int i=0; i<4; i++){
            User_Data.data.building_stats[i].primary_colour = User_Data.data.temp_data[i][0];
            User_Data.data.building_stats[i].secondary_colour = User_Data.data.temp_data[i][1];
            User_Data.data.building_stats[i].model = User_Data.data.temp_data[i][2];
            User_Data.data.building_stats[i].m_height = User_Data.data.temp_data[i][3];

            User_Data.data.temp_data[i][0] = -1;
            User_Data.data.temp_data[i][1] = -1;
            User_Data.data.temp_data[i][2] = model_offset;
            User_Data.data.temp_data[i][3] = 0;
            model_offset += 1;
        }
        
        // Update all 4 main building towers in the database
        for (int j=0; j<4; j++) {
            User_Data.data.CreateRequest("UPDATE_User_Building", j);
        }

        SceneManager.LoadScene(1);
    }
}
