using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Confirm : MonoBehaviour{
    public void ConfirmButton()
    {
        // Assigns the temp values chosen by the user to the persistant data structure and returns 
        // to the main scene
        string subject_name = SceneManager.GetActiveScene().name;
        int index = CodeConverter.codes.subject_map[subject_name];

        User_Data.data.building_stats[index].primary_colour = User_Data.data.temp_data[0][0];
        User_Data.data.building_stats[index].secondary_colour = User_Data.data.temp_data[0][1];
        User_Data.data.building_stats[index].model = User_Data.data.temp_data[0][2];

        User_Data.data.temp_data[0][0] = -1;
        User_Data.data.temp_data[0][1] = -1;
        User_Data.data.temp_data[0][2] = 0;

        // Send a singular update request for the building that has just been customised
        User_Data.data.CreateRequest("UPDATE_User_Building", index);

        //sound stuff
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turning the filter off and setting its frequency
        FindObjectOfType<ListenerPersist>().toggleFilterOn(false);
        FindObjectOfType<ListenerPersist>().setFilterFrequency(500);


        SceneManager.LoadScene(1);
    }
}
