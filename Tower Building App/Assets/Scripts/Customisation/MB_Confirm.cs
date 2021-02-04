using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MB_Confirm : MonoBehaviour{
    public void ConfirmButton(){
        string[] indexes = {"1","2","3","4"};
        int tindex = 0;
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
