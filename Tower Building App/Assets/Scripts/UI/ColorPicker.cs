using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorPicker : MonoBehaviour
{   
    public Material[] Matte;
    public Material[] Metallic;
    public Material[] Emissive;
    public Material[] Gradient;
    public Material[] Fancy;
    public MeshRenderer[] meshRenderer;
    
    private Material[] materials;
    private Button button;

    void Start()
    {   
        //get the current colour button
        button = this.GetComponent<Button>();
        for (int i=0; i<meshRenderer.Length; i++){
            // we need a copy of the current index, in order to change color
            var x = i;
            //take the third and fourth digit of the button name
            int colours = int.Parse(button.name.Substring(1,2));
            int elements = int.Parse(button.name[3].ToString());
            //check the first 2 digit of the button name to determine which materials
            if (button.name[0].ToString() == "0"){
                button.onClick.AddListener(() => MatteColor(x,colours,elements));
            }
            if (button.name[0].ToString() == "1"){
                button.onClick.AddListener(() => MetallicColor(x,colours,elements));
            }
            if (button.name[0].ToString() == "2"){
                button.onClick.AddListener(() => EmissiveColor(x,colours,elements));
            }
            if (button.name[0].ToString() == "3"){
                button.onClick.AddListener(() => GradientColor(x,colours,elements));
            }
            if (button.name[0].ToString() == "4"){
                button.onClick.AddListener(() => FancyColor(x,colours,elements));
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
        string subject_name = SceneManager.GetActiveScene().name;
        int index = CodeConverter.codes.subject_map[subject_name];

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
        materials[k] = Metallic[j];
        meshRenderer[i].materials = materials;

        string subject_name = SceneManager.GetActiveScene().name;
        int index = CodeConverter.codes.subject_map[subject_name];
        if (k == 0){
            User_Data.data.temp_primary = 100+j;
        } else {
            User_Data.data.temp_secondary = 100+j;
        }
    }

    //EMISSIVE COLOURS
    public void EmissiveColor(int i,int j, int k){
        materials = meshRenderer[i].materials;
        materials[k] = Emissive[j];
        meshRenderer[i].materials = materials;

        string subject_name = SceneManager.GetActiveScene().name;
        int index = CodeConverter.codes.subject_map[subject_name];
        if (k == 0){
            User_Data.data.temp_primary = 200+j;
        } else {
            User_Data.data.temp_secondary = 200+j;
        }
    }
    
    //GRADIENT COLOURS
    public void GradientColor(int i,int j, int k){
        materials = meshRenderer[i].materials;
        materials[k] = Gradient[j];
        meshRenderer[i].materials = materials;

        string subject_name = SceneManager.GetActiveScene().name;
        int index = CodeConverter.codes.subject_map[subject_name];
        if (k == 0){
            User_Data.data.temp_primary = 300+j;
        } else {
            User_Data.data.temp_secondary = 300+j;
        }
    }

    //FANCY COLOURS
    public void FancyColor(int i,int j, int k){
        materials = meshRenderer[i].materials;
        materials[k] = Fancy[j];
        meshRenderer[i].materials = materials;

        string subject_name = SceneManager.GetActiveScene().name;
        int index = CodeConverter.codes.subject_map[subject_name];
        if (k == 0){
            User_Data.data.temp_primary = 400+j;
        } else {
            User_Data.data.temp_secondary = 400+j;
        }
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
