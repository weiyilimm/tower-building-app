using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{   

    public Material[] PrimaryColor;
    Material CurrentColor;
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RedMetallic(){
        renderer.material = PrimaryColor[0];
        CurrentColor = renderer.material;
    }

    public void BlueMetallic(){
        renderer.material = PrimaryColor[1];
        CurrentColor = renderer.material;
    }

    public void GreenMetallic(){
        renderer.material = PrimaryColor[2];
        CurrentColor = renderer.material;
    }

    public void PurpleMetallic(){
        renderer.material = PrimaryColor[3];
        CurrentColor = renderer.material;
    }

    public void BronzeMetallic(){
        renderer.material = PrimaryColor[4];
        CurrentColor = renderer.material;
    }

    public void SilverMetallic(){
        renderer.material = PrimaryColor[5];
        CurrentColor = renderer.material;
    }

    public void GoldMetallic(){
        renderer.material = PrimaryColor[6];
        CurrentColor = renderer.material;
    }
    
    public void PlatinumMetallic(){
        renderer.material = PrimaryColor[7];
        CurrentColor = renderer.material;
    }

    
}
