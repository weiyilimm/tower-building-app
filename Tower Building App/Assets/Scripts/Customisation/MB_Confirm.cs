using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MB_Confirm : MonoBehaviour{
    public void ConfirmButton(){
        string[] indexes = {"1","2","3","4"};
        int tindex = 0;
        
        // Gets the index from the array and appends it to the subjectname to make the name of the tower for look up in the dictionary
        // with the result of the look up, an integer index can be used on the user_data to get the corresponding entry for the building in the building_stats array
        // the entry is then updated to store the values currently in the temp variables

        foreach (string index in indexes){
            string subject_name = SceneManager.GetActiveScene().name;
            subject_name = subject_name + index;
            int intdex = CodeConverter.codes.subject_map[subject_name];

            User_Data.data.building_stats[intdex].primary_colour = User_Data.data.temp_primary;
            User_Data.data.building_stats[intdex].secondary_colour = User_Data.data.temp_secondary;
            User_Data.data.building_stats[intdex].model = User_Data.data.temp_model;
            User_Data.data.building_stats[intdex].m_height = User_Data.data.temp_height;

            tindex += 1;
        }
        SceneManager.LoadScene(1);
    }
}
