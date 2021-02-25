using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ColorPicker : MonoBehaviour
{   
    public Material[] Matte;
    public Material[] Metallic;
    public Material[] Emissive;
    public Material[] Gradient;
    public Material[] Fancy;
    //To store buildings' renderer
    public MeshRenderer[] MeshRenderer;
    public MeshRenderer[] MainBuilding1;
    public MeshRenderer[] MainBuilding2;
    public MeshRenderer[] MainBuilding3;
    public MeshRenderer[] MainBuilding4;
    //To store each color buttons
    public Button[] Buttons;
    public Button[] MainBuildings;
    private Material[] materials;
    
    //Get the specific building XP by accessing Scoring.cs 
    private double localXP;
    //15000XP required to to next matte color
    private int matteXP = 15000;
    //30000XP required to to next metallic color
    private int metallicXP = 30000;
    //45000XP required to to next eemissive color
    private int emissiveXP = 45000;
    //60000XP required to to next gradient color
    private int gradientXP = 60000;
    //75000XP required to to next fancy color
    private int fancyXP = 75000;
    private GameObject lockIcon;
    //Get the current scene that user in
    private string currentSceneName;
    
    // This index is used to determine which of the 4 temp_data arrays to store the temp data in
    // All normal buildings will use 0, whereas the Main building will change between 0-3 based on
    // which tower is beign customised
    private int main_index = 0;

    void Start()
    {   
        //Get the current scene name that user in
        currentSceneName = SceneManager.GetActiveScene().name;
        
        //Initialize the temp variables with tha data stored for the chosen building before accesing the menu
        ApplyAtStart(currentSceneName);

        //Assign the localXP to be specific building's XP
        switch (currentSceneName)
        {   
            case "Main":
                MeshRenderer = new MeshRenderer[9];
                localXP = Scoring.MainXP;
                MainBuildings[0].onClick.AddListener(() => WhichBuildings(1));
                MainBuildings[1].onClick.AddListener(() => WhichBuildings(2));
                MainBuildings[2].onClick.AddListener(() => WhichBuildings(3));
                MainBuildings[3].onClick.AddListener(() => WhichBuildings(4));
                break;
            case "Arts":
                localXP = Scoring.ArtsXP;
                break;
            case "BioChe":
                localXP = Scoring.BioCheXP;
                break;
            case "ComSci":
                localXP = Scoring.ComSciXP;
                break;
            case "Eng":
                localXP = Scoring.EngXP;
                break;
            case "Geo":
                localXP = Scoring.GeoXP;
                break;
            case "Lan":
                localXP = Scoring.LanXP;
                break;
            case "LawPol":
                localXP = Scoring.LawPolXP;
                break;
            case "PhyMath":
                localXP = Scoring.PhyMathXP;
                break;
        }

        for (int i = 0; i<Buttons.Length; i++){
            //get the current colour button
            lockIcon = Buttons[i].transform.GetChild(0).gameObject;
            for (int j=0; j<MeshRenderer.Length; j++){
                // we need a copy of the current index, in order to change color
                var x = j;
                //take the third and fourth digit of the button name
                int colours = int.Parse(Buttons[i].name.Substring(1,2));
                int elements = int.Parse(Buttons[i].name[3].ToString());

                if (localXP >= colours * matteXP){
                    if (Buttons[i].name[0].ToString() == "0"){
                        lockIcon.SetActive(false);
                        Buttons[i].onClick.AddListener(() => MatteColor(x,colours,elements));
                    }
                }
                if (localXP >= ((colours * metallicXP) + 500)){
                    if (Buttons[i].name[0].ToString() == "1"){
                        lockIcon.SetActive(false);
                        Buttons[i].onClick.AddListener(() => MetallicColor(x,colours,elements));
                    }
                }
                if (localXP >= ((colours * emissiveXP) + 500)){
                    if (Buttons[i].name[0].ToString() == "2"){
                        lockIcon.SetActive(false);
                        Buttons[i].onClick.AddListener(() => EmissiveColor(x,colours,elements));
                    }
                }
                if (localXP >= ((colours * gradientXP) + 500)){
                    if (Buttons[i].name[0].ToString() == "3"){
                        lockIcon.SetActive(false);
                        Buttons[i].onClick.AddListener(() => GradientColor(x,colours,elements));
                    }
                }
                if (localXP >= ((colours * fancyXP) + 500)){
                    if (Buttons[i].name[0].ToString() == "4"){
                        lockIcon.SetActive(false);
                        Buttons[i].onClick.AddListener(() => FancyColor(x,colours,elements));
                    }
                }
            }
        }
    }
    
    /*
    i indicates which building's MeshRenderer
    j indicates which the colours of specific material e.g. Emissive[0] is Blue 
    k indicates primary key or secondary key e.g. 0 is primary key, 1 is secondary key
    */

    //MATTE COLOURS
    public void MatteColor(int i,int j, int k){
        materials = MeshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_data[main_index][0] = j;
            materials = find_mat(Matte,materials,"1",j);
        } else {
            User_Data.data.temp_data[main_index][1] = j;
            materials = find_mat(Matte,materials,"2",j);
        }

        MeshRenderer[i].materials = materials;
    }

    //METALLIC COLOURS
    public void MetallicColor(int i,int j, int k){
        materials = MeshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_data[main_index][0] = 100+j;
            materials = find_mat(Metallic,materials,"1",j);
        } else {
            User_Data.data.temp_data[main_index][1] = 100+j;
            materials = find_mat(Metallic,materials,"2",j);
        }

        MeshRenderer[i].materials = materials;
    }

    //EMISSIVE COLOURS
    public void EmissiveColor(int i,int j, int k){
        materials = MeshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_data[main_index][0] = 200+j;
            materials = find_mat(Emissive,materials,"1",j);
        } else {
            User_Data.data.temp_data[main_index][1] = 200+j;
            materials = find_mat(Emissive,materials,"2",j);
        }

        MeshRenderer[i].materials = materials;
    }
    
    //GRADIENT COLOURS
    public void GradientColor(int i,int j, int k){
        materials = MeshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_data[main_index][0] = 300+j;
            materials = find_mat(Gradient,materials,"1",j);
        } else {
            User_Data.data.temp_data[main_index][1] = 300+j;
            materials = find_mat(Gradient,materials,"2",j);
        }

        MeshRenderer[i].materials = materials;
    }

    //FANCY COLOURS
    public void FancyColor(int i,int j, int k){
        materials = MeshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_data[main_index][0] = 400+j;
            materials = find_mat(Fancy,materials,"1",j);
        } else {
            User_Data.data.temp_data[main_index][1] = 400+j;
            materials = find_mat(Fancy,materials,"2",j);
        }

        MeshRenderer[i].materials = materials;
    }

    public Material[] find_mat(Material[] mat_type, Material[] materials, string identifier, int j){
        for (int iter=0; iter<materials.Length; iter++){
            if (materials[iter].name.Substring(0,1) == identifier){
                Material swap = Instantiate(mat_type[j] as Material);
                swap.name = identifier + " " + swap.name;
                materials[iter] = swap;
            }
        }
        return materials;
    }

    /*
    Method used for main building UI
    It select the building that user want 
    */
    public void WhichBuildings(int num){
        if (num == 1){
            MeshRenderer = MainBuilding1;
            main_index = 0;
        }
        if (num == 2){
            MeshRenderer = MainBuilding2;
            main_index = 1;
        }
        if (num == 3){
            MeshRenderer = MainBuilding3;
            main_index = 2;
        }
        if (num == 4){
            MeshRenderer = MainBuilding4;
            main_index = 3;
        }

    }

    public void ApplyAtStart(string currentSceneName) {
        int subject_index = CodeConverter.codes.subject_map[currentSceneName];
        if (currentSceneName != "Main") {
            User_Data.data.temp_data[0][0] = User_Data.data.building_stats[subject_index].primary_colour;
            User_Data.data.temp_data[0][1] = User_Data.data.building_stats[subject_index].secondary_colour;
            User_Data.data.temp_data[0][2] = User_Data.data.building_stats[subject_index].model;
            ApplyCheck(0,User_Data.data.temp_data[0][0],User_Data.data.temp_data[0][2]);
            ApplyCheck(1,User_Data.data.temp_data[0][1],User_Data.data.temp_data[0][2]);
        } else {
            for (int i=0; i<4; i++) {
                User_Data.data.temp_data[i][0] = User_Data.data.building_stats[i].primary_colour;
                User_Data.data.temp_data[i][1] = User_Data.data.building_stats[i].secondary_colour;
                User_Data.data.temp_data[i][2] = User_Data.data.building_stats[i].model;
            }
            ApplyCheckMain(0);
            ApplyCheckMain(1);
        } 
    }
    
    public void ApplyCheck(int index, int value, int model) {
        int colour_index;
        if (value != -1) {
            GameObject parent = GameObject.Find("BuildingModels");
            int iterations = parent.transform.childCount;
            for (int k=0; k<iterations; k++) {
                if (model == k) {
                    Transform child = parent.transform.GetChild(model);
                    child.gameObject.SetActive(true);
                } else {
                    Transform child = parent.transform.GetChild(k);
                    child.gameObject.SetActive(false);
                }
                if (value < 100) {
                    colour_index = value;
                    //do matte
                    MatteColor(k,colour_index,index);
                } else if (value < 200) {
                    colour_index = value - 100;
                    // do metallic
                    MetallicColor(k,colour_index,index);
                } else if (value < 300) {
                    colour_index = value - 200;
                    //do emmissive
                    EmissiveColor(k,colour_index,index);
                } else if (value < 400) {
                    colour_index = value - 300;
                    //do gradient
                    GradientColor(k,colour_index,index);
                } else if (value >= 400) {
                    colour_index = value - 400;
                    //do fancy
                    FancyColor(k,colour_index,index);
                }
            }
        }
    }

    public void ApplyCheckMain(int index) {
        //apply to main buildings
        for (int i=1; i<5; i++) {
            string strindex = i.ToString();
            string buildingName = "BuildingModel" + strindex;
            GameObject parent = GameObject.Find(buildingName);
            WhichBuildings(i);
            int value = User_Data.data.temp_data[i-1][index];
            int model = User_Data.data.temp_data[i-1][2];

            for (int j=0; j<9; j++){
                Transform child = parent.transform.GetChild(j);
                if (int.Parse(child.name.Substring(child.name.Length - 1)) == model+1) {
                    child.gameObject.SetActive(true);
                } else if (child.name.Substring(0,4) == "Base"){
                    child.gameObject.SetActive(true);
                } else {
                    child.gameObject.SetActive(false);
                }
                int colour_index;
                if (value != -1) {
                    if (value < 100) {
                        colour_index = value;
                        //do matte
                        MatteColor(j,colour_index,index);
                    } else if (value < 200) {
                        colour_index = value - 100;
                        // do metallic
                        MetallicColor(j,colour_index,index);
                    } else if (value < 300) {
                        colour_index = value - 200;
                        //do emmissive
                        EmissiveColor(j,colour_index,index);
                    } else if (value < 400) {
                        colour_index = value - 300;
                        //do gradient
                        GradientColor(j,colour_index,index);
                    } else if (value >= 400) {
                        colour_index = value - 400;
                        //do fancy
                        FancyColor(j,colour_index,index);
                    }
                }
            }
        }
    }
}
