using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{   
    public Material[] Matte;
    public Material[] Metallic;
    public Material[] Emissive;
    public Material[] Gradient;
    public Material[] Fancy;

    MeshRenderer[] meshRenderer;
    Material[] materials;
    GameObject building;
    Button button;
    
    void Start()
    {   
        //get the current colour button
        button = this.GetComponent<Button>();
        //find the buildings object which consist of all buildings as children
        building = GameObject.Find("Buildings");
        //get the number of buildings
        int childrenSize = building.transform.childCount;
        if (childrenSize >0){
            meshRenderer = new MeshRenderer[childrenSize];
            for (int i=0; i<childrenSize; i++){
                // we need a copy of the current index, in order to change color
                var x = i;
                meshRenderer[x] = building.transform.GetChild(x).GetComponent<MeshRenderer>();
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
    }
    
    /*
    i indicates which building's meshRenderer
    j indicates which the colours of specific material e.g. Emissive[0] is Blue 
    k indicates primary key or secondary key e.g. 0 is primary key, 1 is secondary key
    */

    //MATTE COLOURS
    public void MatteColor(int i,int j, int k){
        materials = meshRenderer[i].materials;
        materials[k] = Matte[j];
        meshRenderer[i].materials = materials;
    }

    //METALLIC COLOURS
    public void MetallicColor(int i,int j, int k){
        materials = meshRenderer[i].materials;
        materials[k] = Metallic[j];
        meshRenderer[i].materials = materials;
    }

    //EMISSIVE COLOURS
    public void EmissiveColor(int i,int j, int k){
        materials = meshRenderer[i].materials;
        materials[k] = Emissive[j];
        meshRenderer[i].materials = materials;
    }
    
    //GRADIENT COLOURS
    public void GradientColor(int i,int j, int k){
        materials = meshRenderer[i].materials;
        materials[k] = Gradient[j];
        meshRenderer[i].materials = materials;
    }

    //FANCY COLOURS
    public void FancyColor(int i,int j, int k){
        materials = meshRenderer[i].materials;
        materials[k] = Fancy[j];
        meshRenderer[i].materials = materials;
    }

}
