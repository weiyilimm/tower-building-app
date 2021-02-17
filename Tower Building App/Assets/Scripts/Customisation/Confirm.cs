using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Confirm : MonoBehaviour{
    public void ConfirmButton()
    {
        // Assigns the temp value chosen by the user to the persistant data structure and returns 
        // to the main scene
        string subject_name = SceneManager.GetActiveScene().name;
        int index = CodeConverter.codes.subject_map[subject_name];

        User_Data.data.building_stats[index].primary_colour = User_Data.data.temp_data[0][0];
        User_Data.data.building_stats[index].secondary_colour = User_Data.data.temp_data[0][1];
        User_Data.data.building_stats[index].model = User_Data.data.temp_data[0][2];

        User_Data.data.temp_data[0][0] = -1;
        User_Data.data.temp_data[0][1] = -1;

        // POST to User
        //User_Data.data.CreateRequest("UPDATE_User");

        SceneManager.LoadScene(1);
    }
}
