using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour
{
    private GameObject saber;
    private GameObject lever;
    //private GameObject container;
    private Color newColor;
    private bool saberInBox;
    private bool leverInBox;

    // Start is called before the first frame update
    void Start()
    {
        saber = GameObject.Find("saber");
        //leverBox = GameObject.Find("leverBox");
        //leverInBox = leverBox.GetComponent<leverBox>().isInBox;
        newColor = new Color(255, 255, 255);
        //container = GameObject.Find("container");
    }

    // Update is called once per frame
    void Update()
    {
        saberInBox = saber.GetComponent<moveSaberInBox>().isInBox;
        Debug.Log("saber: " + saber);
        if(saberInBox == true){  //&& leverInBox == true
            alterSaberColor(newColor, saber);
        }
    }

    private void alterSaberColor(Color newColor, GameObject PassedSaber){
        if (PassedSaber == null){
            return;
        }
        GameObject blade = null;
        foreach (Transform eachChild in PassedSaber.transform){
            if (eachChild.name == "blade"){
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
        material.SetColor("_EmissiveColorLDR", newColor * 6f);
        material.SetVector("_EmissiveColor", newColor * 9f);
        material.SetFloat("_EmissiveIntensity",8f);
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
