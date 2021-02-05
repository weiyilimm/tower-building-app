using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ColorPicker : MonoBehaviour
{   
    public TextMeshProUGUI XPText;
    public Material[] Matte;
    public Material[] Metallic;
    public Material[] Emissive;
    public Material[] Gradient;
    public Material[] Fancy;
    public MeshRenderer[] meshRenderer;
    private Material[] materials;
    private Button button;
    
    private double XP;
    //No need study to get matte color
    private int MatteXP = 500;
    //18 hourss of study to get metallic color
    private int MetallicXP = 1000;
    //36 hours of study to get emissive color
    private int EmissiveXP = 1500;
    //72 hours of study to get gradient color
    private int GradientXP = 2000;
    //144 hours of study to get fancy color
    private int FancyXP = 2500;
    private GameObject lockIcon;

    void Start()
    {   
        XP = StopWatch.GlobalXP;
        XPText.text = XP.ToString() + "XP";
        //get the current colour button
        button = this.GetComponent<Button>();
        lockIcon = this.transform.GetChild(0).gameObject;
        for (int i=0; i<meshRenderer.Length; i++){
            // we need a copy of the current index, in order to change color
            var x = i;
            //take the third and fourth digit of the button name
            int colours = int.Parse(button.name.Substring(1,2));
            int elements = int.Parse(button.name[3].ToString());


            if (XP >= colours * MatteXP){
                if (button.name[0].ToString() == "0"){
                    lockIcon.SetActive(false);
                    button.onClick.AddListener(() => MatteColor(x,colours,elements));
                }
            }
            if (XP >= ((colours * MetallicXP) + 500)){
                if (button.name[0].ToString() == "1"){
                    lockIcon.SetActive(false);
                    button.onClick.AddListener(() => MetallicColor(x,colours,elements));
                }
            }
            if (XP >= ((colours * EmissiveXP) + 500)){
                if (button.name[0].ToString() == "2"){
                    lockIcon.SetActive(false);
                    button.onClick.AddListener(() => EmissiveColor(x,colours,elements));
                }
            }
            if (XP >= ((colours * GradientXP) + 500)){
                if (button.name[0].ToString() == "3"){
                    lockIcon.SetActive(false);
                    button.onClick.AddListener(() => GradientColor(x,colours,elements));
                }
            }
            if (XP >= ((colours * FancyXP) + 500)){
                if (button.name[0].ToString() == "4"){
                    lockIcon.SetActive(false);
                    button.onClick.AddListener(() => FancyColor(x,colours,elements));
                }
            }

        }
    }
    
    /*
    i indicates which building's meshRenderer
    j indicates which the colours of specific material e.g. Emissive[0] is Blue 
    k indicates primary key or secondary key e.g. 0 is primary key, 1 is secondary key
    */

    //MATTE COLOURS
    public void MatteColor(int i,int j, int k){
        materials = meshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_primary = j;
            materials = find_mat(Matte,materials,"1",j);
        } else {
            User_Data.data.temp_secondary = j;
            materials = find_mat(Matte,materials,"2",j);
        }

        meshRenderer[i].materials = materials;
    }

    //METALLIC COLOURS
    public void MetallicColor(int i,int j, int k){
        materials = meshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_primary = 100+j;
            materials = find_mat(Metallic,materials,"1",j);
        } else {
            User_Data.data.temp_secondary = 100+j;
            materials = find_mat(Metallic,materials,"2",j);
        }

        meshRenderer[i].materials = materials;
    }

    //EMISSIVE COLOURS
    public void EmissiveColor(int i,int j, int k){
        materials = meshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_primary = 200+j;
            materials = find_mat(Emissive,materials,"1",j);
        } else {
            User_Data.data.temp_secondary = 200+j;
            materials = find_mat(Emissive,materials,"2",j);
        }

        meshRenderer[i].materials = materials;
    }
    
    //GRADIENT COLOURS
    public void GradientColor(int i,int j, int k){
        materials = meshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_primary = 300+j;
            materials = find_mat(Gradient,materials,"1",j);
        } else {
            User_Data.data.temp_secondary = 300+j;
            materials = find_mat(Gradient,materials,"2",j);
        }

        meshRenderer[i].materials = materials;
    }

    //FANCY COLOURS
    public void FancyColor(int i,int j, int k){
        materials = meshRenderer[i].materials;

        if (k == 0){
            User_Data.data.temp_primary = 400+j;
            materials = find_mat(Fancy,materials,"1",j);
        } else {
            User_Data.data.temp_secondary = 400+j;
            materials = find_mat(Fancy,materials,"2",j);
        }

        meshRenderer[i].materials = materials;
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
