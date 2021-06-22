using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Valve.VR.InteractionSystem;


public class SkyBoxRotationHandler : MonoBehaviour
{
    private HDRISky skybox;

    private float currentSkyOrientation;

    public GameObject attachedWheel;

    private Interactable interactableWheel;

    private ComplexCircularDrive circularDriveWheel;

    private float currentRotation;
    void Init()
    {
        if (GetComponent<Volume>().profile.TryGet(out skybox)){
            Debug.Log("found it");
        } else {
            Debug.Log("didnt find it");
        }
        circularDriveWheel = attachedWheel.GetComponent<ComplexCircularDrive>();
        currentRotation = circularDriveWheel.outAngle;
        currentSkyOrientation = skybox.rotation.value;
        Debug.Log("Current Rotation:" + currentSkyOrientation);
        skybox.rotation.overrideState = true;
        interactableWheel = attachedWheel.GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {       
        if (skybox == null){
            Init();
        }

        if (circularDriveWheel.driving){
            UpdateRotation();
        }
    }

    private void UpdateRotation(){
        Debug.Log("out angle: "+circularDriveWheel.outAngle);
        if (circularDriveWheel.outAngle > currentRotation){
            RotateRight();
            Debug.Log(skybox.rotation.value);
        } else if (circularDriveWheel.outAngle < currentRotation) {     
            RotateLeft();         
            Debug.Log(skybox.rotation.value);
        } else{
            return;
        }
    }

    private void RotateLeft(){
        Debug.Log("steering");
        if (currentSkyOrientation > 1){
            currentSkyOrientation = currentSkyOrientation - 3;
        } else{
            currentSkyOrientation = 360;
        }
        skybox.rotation.value = currentSkyOrientation;
    }

    private void RotateRight(){
        Debug.Log("steering");
        if (currentSkyOrientation < 359){
            currentSkyOrientation = currentSkyOrientation +  3;
        } else {
            currentSkyOrientation = 1;
        }
        skybox.rotation.value = currentSkyOrientation;
    }
}
