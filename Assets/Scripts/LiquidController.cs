using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class LiquidController : MonoBehaviour
{
    private Material liquidMaterial;
    private float currentFill;

    public GameObject attachedWheel;

    private float currentRotation;

    private string FillName = "Vector1_c42c52aff56a419384be3fd9560b4f22";

    //private string BackColorName = "Color_c43493aea85a4fefbf676c4f780a5b5b";

    private string BackColorName = "Color_1e8f8433be814ccaaf40e054928c7e81";

    //private string FrontColorName = "Color_806bd4a2837c4ad2ab48e2bd670d164d";
     private string FrontColorName = "Color_4b6c4bac538044c5b56836a0ed20b851";
    void Start()
    {
        liquidMaterial = GetComponent<Renderer>().material;
        currentFill = liquidMaterial.GetFloat(FillName);
        currentRotation = attachedWheel.GetComponent<CircularDrive>().outAngle;
    }

    void FixedUpdate (){
        alterFillValue();
    }

    public void alterColor(float red, float green, float blue){
        Debug.Log("Changing color to ("+red+","+green+","+blue+")");
        Debug.Log("Currently at: "+ liquidMaterial.GetColor(BackColorName));
        Color passedColor = new Color(red,green,blue, 1f);
        liquidMaterial.SetColor(BackColorName,passedColor);
        red = red - 0.20f;
        if (red < 0f){
            red = 0f;
        }
        green = green - 0.20f;
        if (green < 0f){
            green = 0f;
        }
        blue = blue - 0.20f;
        if (blue < 0f){
            blue = 0f;
        }
        Color passedColoraltered = new Color(red, green, blue, 1f);
        liquidMaterial.SetColor(FrontColorName,passedColoraltered);
    }

    private void alterFillValue(){
        if (attachedWheel.GetComponent<CircularDrive>().outAngle > currentRotation){
            Debug.Log("Increasing");
            Debug.Log(attachedWheel.GetComponent<CircularDrive>().outAngle );
            IncreaseFillValue();
        } else if (attachedWheel.GetComponent<CircularDrive>().outAngle < currentRotation) {
            Debug.Log("Lowering");
            Debug.Log(attachedWheel.GetComponent<CircularDrive>().outAngle );
            LowerFillValue();         
        } else{
            return;
        }
        currentRotation = attachedWheel.GetComponent<CircularDrive>().outAngle;
    }

    private void LowerFillValue(){
        if (currentFill > -5f){
            currentFill = currentFill - 0.01f;
            if (currentFill < -5){
                currentFill = -5f;
            }
            liquidMaterial.SetFloat(FillName,currentFill);
        }
    }

    private void IncreaseFillValue(){
        if (currentFill < 5f){
            currentFill = currentFill +  0.01f;
            if (currentFill > 5){
                currentFill = 5f;
            }
            liquidMaterial.SetFloat(FillName,currentFill);
        }
    }

}
