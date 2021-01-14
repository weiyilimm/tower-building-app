using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{   
    // public Renderer[] PrimaryObject;
    public Material[] PrimaryColor;
    Material CurrentColor;
    Renderer[] renderers;
    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RedMetallic(){
        foreach (var r in renderers)
        {
            r.material = PrimaryColor[0];
            CurrentColor = r.material;
        }
    }

    public void BlueMetallic(){
        foreach (var r in renderers)
        {
            r.material = PrimaryColor[1];
            CurrentColor = r.material;
        }
    }

    public void GreenMetallic(){
        foreach (var r in renderers)
        {
            r.material = PrimaryColor[2];
            CurrentColor = r.material;
        }
    }

    public void PurpleMetallic(){
        foreach (var r in renderers)
        {
            r.material = PrimaryColor[3];
            CurrentColor = r.material;
        }
    }

    public void BronzeMetallic(){
        foreach (var r in renderers)
        {
            r.material = PrimaryColor[4];
            CurrentColor = r.material;
        }
    }

    public void SilverMetallic(){
        foreach (var r in renderers)
        {
            r.material = PrimaryColor[5];
            CurrentColor = r.material;
        }
    }

    public void GoldMetallic(){
        foreach (var r in renderers)
        {
            r.material = PrimaryColor[6];
            CurrentColor = r.material;
        }
    }
    
    public void PlatinumMetallic(){
        foreach (var r in renderers)
        {
            r.material = PrimaryColor[7];
            CurrentColor = r.material;
        }
    }

    
}
