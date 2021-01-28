using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Confirm : MonoBehaviour{
    public void ConfirmButton(){
        // Assigns the temp value chosen by the user to the persistant data structure and returns 
        // to the main scene
        string subject_name = SceneManager.GetActiveScene().name;
        int index = CodeConverter.codes.subject_map[subject_name];
        
        User_Data.data.building_stats[index].primary_colour = User_Data.data.temp_primary;
        User_Data.data.building_stats[index].secondary_colour = User_Data.data.temp_secondary;
        User_Data.data.building_stats[index].model = User_Data.data.temp_model;
        
        SceneManager.LoadScene(1);
    }
}
