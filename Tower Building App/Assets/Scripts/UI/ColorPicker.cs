using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{   
    // public Renderer[] PrimaryObject;
    public Material[] Emissive;
    public Material[] Gradient;
    public Material[] Fancy;
    public Material[] Matte;
    public Material[] Metallic;
    MeshRenderer meshRenderer;
    Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //EMISSIVE PRIMARY COLOURS
    public void BlueEmissivePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Emissive[0];
        meshRenderer.materials = materials;
    }
    public void RedEmissivePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Emissive[1];
        meshRenderer.materials = materials;
    }
    public void GreenEmissivePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Emissive[2];
        meshRenderer.materials = materials;
    }
    public void YellowEmissivePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Emissive[3];
        meshRenderer.materials = materials;
    }
    public void PurpleEmissivePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Emissive[4];
        meshRenderer.materials = materials;
    }
    public void WhiteEmissivePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Emissive[5];
        meshRenderer.materials = materials;
    }


    //EMISSIVE SECONDARY COLOURS
    public void BlueEmissiveSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Emissive[0];
        meshRenderer.materials = materials;
    }
    public void RedEmissiveSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Emissive[1];
        meshRenderer.materials = materials;
    }
    public void GreenEmissiveSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Emissive[2];
        meshRenderer.materials = materials;
    }
    public void YellowEmissiveSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Emissive[3];
        meshRenderer.materials = materials;
    }
    public void PurpleEmissiveSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Emissive[4];
        meshRenderer.materials = materials;
    }
    public void WhiteEmissiveSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Emissive[5];
        meshRenderer.materials = materials;
    }

    
    //GRADIENT PRIMARY COLOURS
    public void BlackWhiteGradientPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Gradient[0];
        meshRenderer.materials = materials;
    }
    public void NavyLightBlueGradientPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Gradient[1];
        meshRenderer.materials = materials;
    }
    public void NavyLightGreenGradientPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Gradient[2];
        meshRenderer.materials = materials;
    }
    public void BlackRedGradientPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Gradient[3];
        meshRenderer.materials = materials;
    }
    public void LightBluePurpleGradientPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Gradient[4];
        meshRenderer.materials = materials;
    }
    public void RedYellowGradientPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Gradient[5];
        meshRenderer.materials = materials;
    }
    public void RainbowGradientPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Gradient[6];
        meshRenderer.materials = materials;
    }


    //GRADIENT SECONDARY COLOURS
    public void BlackWhiteGradientSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Gradient[0];
        meshRenderer.materials = materials;
    }
    public void NavyLightBlueGradientSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Gradient[1];
        meshRenderer.materials = materials;
    }
    public void NavyLightGreenGradientSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Gradient[2];
        meshRenderer.materials = materials;
    }
    public void BlackRedGradientSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Gradient[3];
        meshRenderer.materials = materials;
    }
    public void LightBluePurpleGradientSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Gradient[4];
        meshRenderer.materials = materials;
    }
    public void RedYellowGradientSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Gradient[5];
        meshRenderer.materials = materials;
    }
    public void RainbowGradientSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Gradient[6];
        meshRenderer.materials = materials;
    }
    

    //FANCY PRIMARY COLOURS
    public void BrickFancyPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Fancy[0];
        meshRenderer.materials = materials;
    }
    public void StoneBrickFancyPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Fancy[1];
        meshRenderer.materials = materials;
    }
    public void CamoFancyPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Fancy[2];
        meshRenderer.materials = materials;
    }
    public void ConcreteLeavesFancyPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Fancy[3];
        meshRenderer.materials = materials;
    }
    public void TronBlueFancyPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Fancy[4];
        meshRenderer.materials = materials;
    }
    public void HazardFancyPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Fancy[5];
        meshRenderer.materials = materials;
    }
    public void CamoArcticFancyPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Fancy[6];
        meshRenderer.materials = materials;
    }
    public void TronRedFancyPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Fancy[7];
        meshRenderer.materials = materials;
    }
    public void WaterFancyPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Fancy[8];
        meshRenderer.materials = materials;
    }
    public void LavaFancyPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Fancy[9];
        meshRenderer.materials = materials;
    }


    //FANCY Secondary COLOURS
    public void BrickFancySecondary(){
        materials = meshRenderer.materials;
        materials[1] = Fancy[0];
        meshRenderer.materials = materials;
    }
    public void StoneBrickFancySecondary(){
        materials = meshRenderer.materials;
        materials[1] = Fancy[1];
        meshRenderer.materials = materials;
    }
    public void CamoFancySecondary(){
        materials = meshRenderer.materials;
        materials[1] = Fancy[2];
        meshRenderer.materials = materials;
    }
    public void ConcreteLeavesFancySecondary(){
        materials = meshRenderer.materials;
        materials[1] = Fancy[3];
        meshRenderer.materials = materials;
    }
    public void TronBlueFancySecondary(){
        materials = meshRenderer.materials;
        materials[1] = Fancy[4];
        meshRenderer.materials = materials;
    }
    public void HazardFancySecondary(){
        materials = meshRenderer.materials;
        materials[1] = Fancy[5];
        meshRenderer.materials = materials;
    }
    public void CamoArcticFancySecondary(){
        materials = meshRenderer.materials;
        materials[1] = Fancy[6];
        meshRenderer.materials = materials;
    }
    public void TronRedFancySecondary(){
        materials = meshRenderer.materials;
        materials[1] = Fancy[7];
        meshRenderer.materials = materials;
    }
    public void WaterFancySecondary(){
        materials = meshRenderer.materials;
        materials[1] = Fancy[8];
        meshRenderer.materials = materials;
    }
    public void LavaFancySecondary(){
        materials = meshRenderer.materials;
        materials[1] = Fancy[9];
        meshRenderer.materials = materials;
    }
    

    //MATTE PRIMARY COLOURS
    public void WhiteMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[0];
        meshRenderer.materials = materials;
    }
    public void LightFadedGreenMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[1];
        meshRenderer.materials = materials;
    }
    public void LightBlueMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[2];
        meshRenderer.materials = materials;
    }
    public void RedMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[3];
        meshRenderer.materials = materials;
    }
    public void OrangeMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[4];
        meshRenderer.materials = materials;
    }
    public void PurpleMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[5];
        meshRenderer.materials = materials;
    }
    public void YellowMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[6];
        meshRenderer.materials = materials;
    }
    public void CyanMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[7];
        meshRenderer.materials = materials;
    }
    public void SandMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[8];
        meshRenderer.materials = materials;
    }
    public void LiliacMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[9];
        meshRenderer.materials = materials;
    }
    public void PinkMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[10];
        meshRenderer.materials = materials;
    }
    public void DarkGreenMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[11];
        meshRenderer.materials = materials;
    }
    public void DarkNavyBlueMattePrimary(){
        materials = meshRenderer.materials;
        materials[0] = Matte[12];
        meshRenderer.materials = materials;
    }
    public void BlackMatte(){
        materials = meshRenderer.materials;
        materials[0] = Matte[13];
        meshRenderer.materials = materials;
    }

    
    //MATTE SECONDARY COLOURS
    public void WhiteMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[0];
        meshRenderer.materials = materials;
    }
    public void LightFadedGreenMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[1];
        meshRenderer.materials = materials;
    }
    public void LightBlueMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[2];
        meshRenderer.materials = materials;
    }
    public void RedMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[3];
        meshRenderer.materials = materials;
    }
    public void OrangeMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[4];
        meshRenderer.materials = materials;
    }
    public void PurpleMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[5];
        meshRenderer.materials = materials;
    }
    public void YellowMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[6];
        meshRenderer.materials = materials;
    }
    public void CyanMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[7];
        meshRenderer.materials = materials;
    }
    public void SandMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[8];
        meshRenderer.materials = materials;
    }
    public void LiliacMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[9];
        meshRenderer.materials = materials;
    }
    public void PinkMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[10];
        meshRenderer.materials = materials;
    }
    public void DarkGreenMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[11];
        meshRenderer.materials = materials;
    }
    public void DarkNavyBlueMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[12];
        meshRenderer.materials = materials;
    }
    public void BlackMatteSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Matte[13];
        meshRenderer.materials = materials;
    }


    //METALLIC PRIMARY COLOURS
    public void RedMetallicPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Metallic[0];
        meshRenderer.materials = materials;
    }
    public void BlueMetallicPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Metallic[1];
        meshRenderer.materials = materials;
    }
    public void GreenMetallicPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Metallic[2];
        meshRenderer.materials = materials;
    }
    public void PurpleMetallicPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Metallic[3];
        meshRenderer.materials = materials;
    }
    public void BronzeMetallicPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Metallic[4];
        meshRenderer.materials = materials;
    }
    public void SilverMetallicPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Metallic[5];
        meshRenderer.materials = materials;
    }
    public void GoldMetallicPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Metallic[6];
        meshRenderer.materials = materials;
    }
    public void PlatinumMetallicPrimary(){
        materials = meshRenderer.materials;
        materials[0] = Metallic[7];
        meshRenderer.materials = materials;
    }

    //METALLIC SECONDARY COLOURS
    public void RedMetallicSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Metallic[0];
        meshRenderer.materials = materials;
    }
    public void BlueMetallicSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Metallic[1];
        meshRenderer.materials = materials;
    }
    public void GreenMetallicSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Metallic[2];
        meshRenderer.materials = materials;
    }
    public void PurpleMetallicSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Metallic[3];
        meshRenderer.materials = materials;
    }
    public void BronzeMetallicSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Metallic[4];
        meshRenderer.materials = materials;
    }
    public void SilverMetallicSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Metallic[5];
        meshRenderer.materials = materials;
    }
    public void GoldMetallicSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Metallic[6];
        meshRenderer.materials = materials;
    }
    public void PlatinumMetallicSecondary(){
        materials = meshRenderer.materials;
        materials[1] = Metallic[7];
        meshRenderer.materials = materials;
    }

}
