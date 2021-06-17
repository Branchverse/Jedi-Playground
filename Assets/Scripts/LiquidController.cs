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
    void Start()
    {
        liquidMaterial = GetComponent<Renderer>().material;
        currentFill = liquidMaterial.GetFloat(FillName);
        currentRotation = attachedWheel.GetComponent<CircularDrive>().outAngle;
    }

    void FixedUpdate (){
        alterFillValue();
    }

    void alterColor(){
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
