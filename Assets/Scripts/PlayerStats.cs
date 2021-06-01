using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class PlayerStats : MonoBehaviour
{
    private GameObject LastUsedLightSaber;

    public SteamVR_ActionSet m_ActionSet;
    private SteamVR_Action_Boolean isPulling; 

    public SteamVR_Input_Sources leftHand;

    public SteamVR_Input_Sources rightHand;

    public GameObject targetHand;

    private bool isPullingSaber;

    public int HandMode;

    public GameObject ForcePushObject;



    private void Awake() {
            isPulling = SteamVR_Actions._default.GrabPinch;

            isPulling[SteamVR_Input_Sources.LeftHand].onStateDown += useForce;
            isPulling[SteamVR_Input_Sources.RightHand].onStateDown += useForce;
            isPulling[SteamVR_Input_Sources.Any].onStateUp += stopLightsaberPull;

    }

    void start() {
        m_ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)){
            if (HandMode == 1){
                Debug.Log("Changed Hand mode to 0");
                HandMode = 0;
                
            } else {
                Debug.Log("Changed Hand mode to 1");
                HandMode = 1;
            }
        }
        if (isPullingSaber && !targetHand.GetComponent<Hand>().ObjectIsAttached(LastUsedLightSaber)){
            if (targetHand.GetComponent<Hand>().hoverLocked){
                Debug.Log("Is holding object");
                return;
            }
            Debug.Log(targetHand.GetComponent<Hand>().currentAttachedObject);
            Vector3 LightSaberVelocity = targetHand.transform.position - LastUsedLightSaber.transform.position;
            //scale Vector to length 10
            LastUsedLightSaber.GetComponent<Rigidbody>().velocity = Vector3.Scale( Vector3.Normalize(LightSaberVelocity), new Vector3(10,10,10));
            Vector3 SabertoTargetDelta = targetHand.transform.position - LastUsedLightSaber.transform.position;
            if (SabertoTargetDelta.magnitude<0.25){
                Valve.VR.InteractionSystem.Hand Hand =  targetHand.GetComponent<Hand>();
                Debug.Log(Hand);      
                Vector3 offset = LastUsedLightSaber.transform.position - LastUsedLightSaber.transform.GetChild(0).gameObject.transform.position;
                LastUsedLightSaber.transform.position = Hand.transform.position + offset;      
                Hand.AttachObject(LastUsedLightSaber, Hand.GetBestGrabbingType(),LastUsedLightSaber.GetComponent<Throwable>().attachmentFlags, LastUsedLightSaber.GetComponent<Throwable>().attachmentOffset);           
                isPullingSaber = false;
            }
        }
    }


    private void useForce(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
        if (HandMode == 0){
            moveLightsaber(action_Boolean, source);
        } else if (HandMode == 1){
            forcePush(action_Boolean, source);
        }
    }



    private void forcePush(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
        var startHand = GameObject.Find("/Player/SteamVRObjects/RightHand");
        if (source == SteamVR_Input_Sources.LeftHand){
            startHand =  GameObject.Find("/Player/SteamVRObjects/LeftHand");
        }  
        GameObject Push = Instantiate(ForcePushObject, startHand.transform.position, Quaternion.Euler(new Vector3(0,0,0)));
        Vector3 force = startHand.GetComponentInChildren<ForceDirection>().getDirection();
        Push.GetComponent<Rigidbody>().velocity = Vector3.Scale(force, new Vector3(20,20,20));
        Debug.DrawRay(startHand.transform.position, Vector3.Scale(force, new Vector3(20,20,20)), Color.green, 10f);
        Debug.Log("Spawned Forceball");
    }

    private void moveLightsaber(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
        if (LastUsedLightSaber == null){
            return;
        }
        Debug.Log(source);
        Debug.Log("Pulling Object");
        targetHand = GameObject.Find("/Player/SteamVRObjects/RightHand");
        if (source == SteamVR_Input_Sources.LeftHand){
            targetHand =  GameObject.Find("/Player/SteamVRObjects/LeftHand");
        }
        
        if (!targetHand.GetComponent<Hand>().ObjectIsAttached(LastUsedLightSaber)){
            Debug.Log("pulling lightsaber"); 
            isPullingSaber = true;
        } else {
            Debug.Log("not pulling as hand is already holding object");

        }
        
        

    }
    public void setLastLightsaber(GameObject saber) {
        LastUsedLightSaber = saber;
    }

    public GameObject getLastLightsaber() {
        return LastUsedLightSaber;
    }
    

    private void stopLightsaberPull(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
        isPullingSaber = false;
    }
}
