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
    //To store each color buttons
    public Button[] Buttons;
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
    void Start()
    {   
        //Get the current scene name that user in
        currentSceneName = SceneManager.GetActiveScene().name;
        //Assign the localXP to be specific building's XP
        switch (currentSceneName)
        {
            case "Main":
                localXP = Scoring.MainXP;
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
            User_Data.data.temp_primary = j;
            materials = find_mat(Matte,materials,"1",j);
        } else {
            User_Data.data.temp_secondary = j;
            materials = find_mat(Matte,materials,"2",j);
        }

        MeshRenderer[i].materials = materials;
    }

    //METALLIC COLOURS
    public void MetallicColor(int i,int j, int k){
        materials = MeshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_primary = 100+j;
            materials = find_mat(Metallic,materials,"1",j);
        } else {
            User_Data.data.temp_secondary = 100+j;
            materials = find_mat(Metallic,materials,"2",j);
        }

        MeshRenderer[i].materials = materials;
    }

    //EMISSIVE COLOURS
    public void EmissiveColor(int i,int j, int k){
        materials = MeshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_primary = 200+j;
            materials = find_mat(Emissive,materials,"1",j);
        } else {
            User_Data.data.temp_secondary = 200+j;
            materials = find_mat(Emissive,materials,"2",j);
        }

        MeshRenderer[i].materials = materials;
    }
    
    //GRADIENT COLOURS
    public void GradientColor(int i,int j, int k){
        materials = MeshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_primary = 300+j;
            materials = find_mat(Gradient,materials,"1",j);
        } else {
            User_Data.data.temp_secondary = 300+j;
            materials = find_mat(Gradient,materials,"2",j);
        }

        MeshRenderer[i].materials = materials;
    }

    //FANCY COLOURS
    public void FancyColor(int i,int j, int k){
        materials = MeshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_primary = 400+j;
            materials = find_mat(Fancy,materials,"1",j);
        } else {
            User_Data.data.temp_secondary = 400+j;
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

}
