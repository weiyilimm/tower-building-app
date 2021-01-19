using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{   
    // public Renderer[] PrimaryObject;
    public Material[] Emissive;
    public Material[] Gradient;
    public Material[] ImageTexture;
    public Material[] Matte;
    public Material[] Mettalic;
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

    public void BlueEmissive(){
        foreach (var r in renderers)
        {
            r.material = Emissive[0];
            CurrentColor = r.material;
        }
    }

    public void RedEmissive(){
        foreach (var r in renderers)
        {
            r.material = Emissive[1];
            CurrentColor = r.material;
        }
    }

    public void GreenEmissive(){
        foreach (var r in renderers)
        {
            r.material = Emissive[2];
            CurrentColor = r.material;
        }
    }

    public void YellowEmissive(){
        foreach (var r in renderers)
        {
            r.material = Emissive[3];
            CurrentColor = r.material;
        }
    }

    public void PurpleEmissive(){
        foreach (var r in renderers)
        {
            r.material = Emissive[4];
            CurrentColor = r.material;
        }
    }

    public void WhiteEmissive(){
        foreach (var r in renderers)
        {
            r.material = Emissive[5];
            CurrentColor = r.material;
        }
    }

    public void BlackRedGradient(){
        foreach (var r in renderers)
        {
            r.material = Gradient[0];
            CurrentColor = r.material;
        }
    }

    public void BlackWhiteGradient(){
        foreach (var r in renderers)
        {
            r.material = Gradient[1];
            CurrentColor = r.material;
        }
    }

    public void LightBluePurpleGradient(){
        foreach (var r in renderers)
        {
            r.material = Gradient[2];
            CurrentColor = r.material;
        }
    }

    public void NavyLightBlueGradient(){
        foreach (var r in renderers)
        {
            r.material = Gradient[3];
            CurrentColor = r.material;
        }
    }

    public void NavyLightGreenGradient(){
        foreach (var r in renderers)
        {
            r.material = Gradient[4];
            CurrentColor = r.material;
        }
    }

    public void RainbowGradient(){
        foreach (var r in renderers)
        {
            r.material = Gradient[5];
            CurrentColor = r.material;
        }
    }

    public void RedYellowGradient(){
        foreach (var r in renderers)
        {
            r.material = Gradient[6];
            CurrentColor = r.material;
        }
    }

    public void CamoArcticTexture(){
        foreach (var r in renderers)
        {
            r.material = ImageTexture[0];
            CurrentColor = r.material;
        }
    }

    public void CamoTexture(){
        foreach (var r in renderers)
        {
            r.material = ImageTexture[1];
            CurrentColor = r.material;
        }
    }

    public void LavaTexture(){
        foreach (var r in renderers)
        {
            r.material = ImageTexture[2];
            CurrentColor = r.material;
        }
    }

    public void TronBlueTexture(){
        foreach (var r in renderers)
        {
            r.material = ImageTexture[3];
            CurrentColor = r.material;
        }
    }

    public void TronRedTexture(){
        foreach (var r in renderers)
        {
            r.material = ImageTexture[4];
            CurrentColor = r.material;
        }
    }

    public void WaterTexture(){
        foreach (var r in renderers)
        {
            r.material = ImageTexture[5];
            CurrentColor = r.material;
        }
    }

    public void WhiteMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[0];
            CurrentColor = r.material;
        }
    }

    public void LightFadedGreenMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[1];
            CurrentColor = r.material;
        }
    }

    public void LightBlueMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[2];
            CurrentColor = r.material;
        }
    }

    public void RedMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[3];
            CurrentColor = r.material;
        }
    }

    public void OrangeMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[4];
            CurrentColor = r.material;
        }
    }

    public void PurpleMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[5];
            CurrentColor = r.material;
        }
    }

    public void YellowMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[6];
            CurrentColor = r.material;
        }
    }

    public void CyanMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[7];
            CurrentColor = r.material;
        }
    }

    public void SandMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[8];
            CurrentColor = r.material;
        }
    }

    public void LiliacMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[9];
            CurrentColor = r.material;
        }
    }

    public void PinkMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[10];
            CurrentColor = r.material;
        }
    }

    public void DarkGreenMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[11];
            CurrentColor = r.material;
        }
    }

    public void DarkNavyBlueMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[12];
            CurrentColor = r.material;
        }
    }

    public void BlackMatte(){
        foreach (var r in renderers)
        {
            r.material = Matte[13];
            CurrentColor = r.material;
        }
    }

    public void RedMetallic(){
        foreach (var r in renderers)
        {
            r.material = Mettalic[0];
            CurrentColor = r.material;
        }
    }

    public void BlueMetallic(){
        foreach (var r in renderers)
        {
            r.material = Mettalic[1];
            CurrentColor = r.material;
        }
    }

    public void GreenMetallic(){
        foreach (var r in renderers)
        {
            r.material = Mettalic[2];
            CurrentColor = r.material;
        }
    }

    public void PurpleMetallic(){
        foreach (var r in renderers)
        {
            r.material = Mettalic[3];
            CurrentColor = r.material;
        }
    }

    public void BronzeMetallic(){
        foreach (var r in renderers)
        {
            r.material = Mettalic[4];
            CurrentColor = r.material;
        }
    }

    public void SilverMetallic(){
        foreach (var r in renderers)
        {
            r.material = Mettalic[5];
            CurrentColor = r.material;
        }
    }

    public void GoldMetallic(){
        foreach (var r in renderers)
        {
            r.material = Mettalic[6];
            CurrentColor = r.material;
        }
    }
    
    public void PlatinumMetallic(){
        foreach (var r in renderers)
        {
            r.material = Mettalic[7];
            CurrentColor = r.material;
        }
    }


}
