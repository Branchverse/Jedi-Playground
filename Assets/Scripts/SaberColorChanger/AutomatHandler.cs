using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class AutomatHandler : MonoBehaviour
{
    private GameObject SaberinMachine = null;

    private GameObject BarrelinSlot = null;

    private bool SaberIsAligned;

    private Hand holdingHand;
    public Transform SaberPosition;

    public Transform ContainerPosition;

    public Transform OutPoint;

    private string BackColorName = "Color_1e8f8433be814ccaaf40e054928c7e81";

    private string FillName = "Vector1_c42c52aff56a419384be3fd9560b4f22";

    private bool emptyBarrel;

    private bool BarrelIsAligned;

    private Material BarrelFluid;

    
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SaberinMachine != null && !SaberIsAligned){
            if (SaberinMachine.GetComponent<Interactable>().attachedToHand == null){         
                SaberinMachine.gameObject.transform.position = SaberPosition.transform.position;
                SaberinMachine.gameObject.transform.eulerAngles = new Vector3(0,0,0);
                SaberinMachine.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log("Set to kinematic");
                SaberIsAligned = true;
            }
        }

        if (BarrelinSlot != null && !BarrelIsAligned){
            if (BarrelinSlot.GetComponent<Interactable>().attachedToHand == null){ 
                BarrelinSlot.gameObject.transform.position = ContainerPosition.position;
                BarrelinSlot.gameObject.transform.eulerAngles = new Vector3(90,0,0);
                BarrelinSlot.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                BarrelIsAligned = true;
            }
        }  

        //Empty Barrel if necessary and BarrelFluid exists
        if (emptyBarrel &&  BarrelFluid != null){
            float currentFill = BarrelFluid.GetFloat(FillName);
            if (currentFill <= -4.9f){
                emptyBarrel = false;
                BarrelinSlot.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                BarrelinSlot = null;
                BarrelFluid = null;
            } else{
                BarrelFluid.SetFloat(FillName, currentFill - 0.001f);
            }
        }
    }


    public void activateColorChange(){
        if (SaberinMachine == null){
            Debug.Log("No saber in machine");
            return;
        }
        if (SaberinMachine != null && BarrelinSlot != null){
            //Default value
            Color barrelColor = Color.blue;
            foreach (Transform eachChild in BarrelinSlot.transform){
            if (eachChild.name == "liquid"){
                    BarrelFluid = eachChild.GetComponent<Renderer>().material;
                    barrelColor = BarrelFluid.GetColor(BackColorName);
                }
            }      
            alterSaberColor(barrelColor, SaberinMachine);
            emptyBarrel = true;
            SaberinMachine.transform.position = OutPoint.position;
            SaberinMachine.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            SaberinMachine = null;
            SaberIsAligned = true;
        }   
    }

    public GameObject getLightsaber(){
        return SaberinMachine;
    }

    public void setLightsaber(GameObject saber){
        Debug.Log("Saber set to"+saber.transform.name);
        if (saber == null){
            SaberinMachine = null;
            SaberIsAligned = true;
        }
        SaberinMachine = saber;
        SaberIsAligned = false;
    }

    public void setBarrel(GameObject barrel){
        if (barrel == null){
            BarrelinSlot = barrel;
            BarrelIsAligned = true;
        }
        BarrelinSlot = barrel;
        BarrelIsAligned = false;
    }

    public GameObject getBarrel(){
        return BarrelinSlot;
    }


    private void alterSaberColor(Color newColor, GameObject PassedSaber){
        if (PassedSaber == null){
            return;
        }
        GameObject blade = null;
        foreach (Transform eachChild in PassedSaber.transform){
            if (eachChild.name == "Blade"){
                Debug.Log("Found blade");
                blade = eachChild.transform.gameObject;
            }
        }  

        if (blade == null){
            return;
        }
            
        Material material = blade.GetComponent<Renderer>().material;
      
        //This does black Voodoo magic...please don´t touch it if you don´t absolutely have to
        //Change values of shader
        material.EnableKeyword("_EMISSION");
        material.EnableKeyword("_EMISSIVE");
        material.EnableKeyword("_EmissiveColor");
        //The multipliers here change the emmission intensity. They might have to be altered to mimic the value of the base blue saber
        material.SetColor("_EmissiveColorLDR", newColor * 5f);
        material.SetVector("_EmissiveColor", newColor * 8f);
        material.SetFloat("_EmissiveIntensity",7f);
        RendererExtensions.UpdateGIMaterials(blade.GetComponent<Renderer>());
        DynamicGI.UpdateEnvironment();  

        //Recalculate Shader based on passed values 
        UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(material); 

        //Adjust Point Light color so the enviroment lighting has the right color
        foreach (Transform eachChild in blade.transform){
            if (eachChild.name == "Point Light"){
                Debug.Log("Found blade");
                eachChild.transform.gameObject.GetComponent<Light>().color = newColor;
                break;
            }
        }    

    }

    //This is a method stolen from the depths of the unity source code...just don´t touch it
    public static void UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(Material material)
    {
        const string kEmissiveColorLDR = "_EmissiveColorLDR";
        const string kEmissiveColor = "_EmissiveColor";
        const string kEmissiveIntensity = "_EmissiveIntensity";
 
        if (material.HasProperty(kEmissiveColorLDR) && material.HasProperty(kEmissiveIntensity) && material.HasProperty(kEmissiveColor))
        {
            // Important: The color picker for kEmissiveColorLDR is LDR and in sRGB color space but Unity don't perform any color space conversion in the color
            // picker BUT only when sending the color data to the shader... So as we are doing our own calculation here in C#, we must do the conversion ourselves.
            Color emissiveColorLDR = material.GetColor(kEmissiveColorLDR);
            Color emissiveColorLDRLinear = new Color(Mathf.GammaToLinearSpace(emissiveColorLDR.r), Mathf.GammaToLinearSpace(emissiveColorLDR.g), Mathf.GammaToLinearSpace(emissiveColorLDR.b));
            material.SetColor(kEmissiveColor, emissiveColorLDRLinear * material.GetFloat(kEmissiveIntensity));
        }
    }
}


