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


    private void Awake() {
            isPulling = SteamVR_Actions._default.GrabPinch;

            isPulling[SteamVR_Input_Sources.Any].onStateDown += moveLightsaber;

    }

    void start() {
        m_ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
    }

    void Update() {
        if (isPullingSaber && !this.targetHand.GetComponent<Hand>().ObjectIsAttached(this.LastUsedLightSaber)){
            Vector3 LightSaberVelocity = this.targetHand.transform.position - this.LastUsedLightSaber.transform.position;
            //scale Vector to length 10
            this.LastUsedLightSaber.GetComponent<Rigidbody>().velocity = Vector3.Scale( Vector3.Normalize(LightSaberVelocity), new Vector3(10,10,10));
            Vector3 SabertoTargetDelta = this.targetHand.transform.position - this.LastUsedLightSaber.transform.position;
            if (SabertoTargetDelta.magnitude<0.25){
                Valve.VR.InteractionSystem.Hand Hand =  this.targetHand.GetComponent<Hand>();
                Hand.AttachObject(this.LastUsedLightSaber, Hand.GetBestGrabbingType());
                this.isPullingSaber = false;
            }
        }
    }

    private void moveLightsaber(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
        if (this.LastUsedLightSaber == null){
            return;
        }
        Debug.Log("Pulling Object");
        this.targetHand = GameObject.Find("/Player/SteamVRObjects/RightHand");
        if (source == SteamVR_Input_Sources.LeftHand){
            this.targetHand =  GameObject.Find("/Player/SteamVRObjects/LeftHand");
        }
        if (!this.targetHand.GetComponent<Hand>().ObjectIsAttached(this.LastUsedLightSaber)){
            Debug.Log("pulling lightsaber"); 
            this.isPullingSaber = true;
        } else {
            Debug.Log("not pulling as hand is already holding object");

        }
        
        //this.LastUsedLightSaber.transform.position = Vector3.MoveTowards(, , 1);
        

    }
    public void setLastLightsaber(GameObject saber) {
        this.LastUsedLightSaber = saber;
    }

    public GameObject getLastLightsaber() {
        return this.LastUsedLightSaber;
    }
    
}
