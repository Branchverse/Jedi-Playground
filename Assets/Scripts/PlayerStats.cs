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

    private SteamVR_Action_Boolean isUsingSecondary; 

    public SteamVR_Input_Sources leftHand;

    public SteamVR_Input_Sources rightHand;

    public GameObject targetHandPrimary;

     public GameObject targetHandSecondary;

    private bool isPullingSaber;

    private bool secondaryIsActive;

    public int HandMode;

    public GameObject ForcePushObject;

    private GameObject ObjectForSecondary;

    private float SecondaryGameObjectDistance;



    private void Awake() {
            isPulling = SteamVR_Actions._default.GrabPinch;

            isUsingSecondary = SteamVR_Actions._default.GrabGrip;

            //Primary Action
            isPulling[SteamVR_Input_Sources.LeftHand].onStateDown += useForce;
            isPulling[SteamVR_Input_Sources.RightHand].onStateDown += useForce;
            isPulling[SteamVR_Input_Sources.Any].onStateUp += stopLightsaberPull;

            //Secondary Action
            isUsingSecondary[SteamVR_Input_Sources.LeftHand].onStateDown += useSecondary;
            isUsingSecondary[SteamVR_Input_Sources.RightHand].onStateDown += useSecondary;
            isUsingSecondary[SteamVR_Input_Sources.RightHand].onStateUp += stopSecondary;

    }

    void start() {
        m_ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
    }

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.P)){
            if (HandMode == 1){
                Debug.Log("Changed Hand mode to 0");
                HandMode = 0;
                
            } else {
                Debug.Log("Changed Hand mode to 1");
                HandMode = 1;
            }
        }

        //Check if Primary is active
        if (isPullingSaber && !targetHandPrimary.GetComponent<Hand>().ObjectIsAttached(LastUsedLightSaber)){
            if (targetHandPrimary.GetComponent<Hand>().hoverLocked){
                Debug.Log("Is holding object");
                return;
            }
            Debug.Log(targetHandPrimary.GetComponent<Hand>().currentAttachedObject);
            Vector3 LightSaberVelocity = targetHandPrimary.transform.position - LastUsedLightSaber.transform.position;
            //scale Vector to length 10
            LastUsedLightSaber.GetComponent<Rigidbody>().velocity = Vector3.Scale( Vector3.Normalize(LightSaberVelocity), new Vector3(10,10,10));
            Vector3 SabertoTargetDelta = targetHandPrimary.transform.position - LastUsedLightSaber.transform.position;
            if (SabertoTargetDelta.magnitude<0.25){
                Valve.VR.InteractionSystem.Hand Hand =  targetHandPrimary.GetComponent<Hand>();
                Debug.Log(Hand);      
                Vector3 offset = LastUsedLightSaber.transform.position - LastUsedLightSaber.transform.GetChild(0).gameObject.transform.position;
                LastUsedLightSaber.transform.position = Hand.transform.position + offset;      
                Hand.AttachObject(LastUsedLightSaber, Hand.GetBestGrabbingType(),LastUsedLightSaber.GetComponent<Throwable>().attachmentFlags, LastUsedLightSaber.GetComponent<Throwable>().attachmentOffset);           
                isPullingSaber = false;
            }
        }

        if (secondaryIsActive && targetHandSecondary.GetComponent<Hand>().currentAttachedObjectInfo == null ){   
            var targetPoint = targetHandSecondary.transform.position + targetHandSecondary.GetComponentInChildren<ForceDirection>().getDirection().normalized * SecondaryGameObjectDistance;
            //Speedmodifier to make object feel more powerful
            ObjectForSecondary.GetComponent<Rigidbody>().velocity = (targetPoint - ObjectForSecondary.transform.position) * 5;
            //ObjectForSecondary.GetComponent<Rigidbody>().AddForce( (targetPoint - ObjectForSecondary.transform.position) * 5);
            Debug.DrawLine(targetPoint, ObjectForSecondary.transform.position, Color.red, 15f);
            Debug.Log("new velocty:" + (targetPoint - ObjectForSecondary.transform.position));
        }
    }


    private void useForce(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
         targetHandPrimary = GameObject.Find("/Player/SteamVRObjects/RightHand");
        if (source == SteamVR_Input_Sources.LeftHand){
            targetHandPrimary =  GameObject.Find("/Player/SteamVRObjects/LeftHand");
        }  
        if (targetHandPrimary.GetComponent<ClimberHand>().TouchedCount > 0) {
            return;
        }
        if (HandMode == 0){
            moveLightsaber(action_Boolean, source);
        } else if (HandMode == 1){
            forcePush(action_Boolean, source);
        }
    }

    private void useSecondary(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
        targetHandSecondary = GameObject.Find("/Player/SteamVRObjects/RightHand");
        if (source == SteamVR_Input_Sources.LeftHand){
             targetHandSecondary =  GameObject.Find("/Player/SteamVRObjects/LeftHand");
        }  
        if (targetHandSecondary.GetComponent<Hand>().currentAttachedObjectInfo != null){
            Debug.Log("is holding Object");
            return;
        }
        RaycastHit hit;
        Debug.Log("Looking for secondary target");
        //Debug.DrawRay(targetHandSecondary.transform.position, targetHandSecondary.GetComponentInChildren<ForceDirection>().getDirection(), Color.green);
        if(Physics.Raycast( targetHandSecondary.transform.position, targetHandSecondary.GetComponentInChildren<ForceDirection>().getDirection(), out hit)){
            //cannot move non rigidbody Object 
            Debug.Log("Found secondary target");
            if (hit.transform.gameObject.GetComponent<Rigidbody>() == null){
                Debug.Log("Non Rigidbody: "+hit.transform.gameObject.name);
                return;
            }
            ObjectForSecondary = hit.transform.gameObject;
            secondaryIsActive = true;
            SecondaryGameObjectDistance = (ObjectForSecondary.transform.position - targetHandSecondary.transform.position).magnitude;           
            ObjectForSecondary.GetComponent<Material>().SetColor("test",Color.green);
            Debug.Log("set secondary target");
            return;
        }
        Debug.Log("No target found");
    }

    private void forcePush(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){       
        GameObject Push = Instantiate(ForcePushObject, targetHandPrimary.transform.position, Quaternion.Euler(new Vector3(0,0,0)));
        Physics.IgnoreCollision(ForcePushObject.GetComponent<Collider>(), targetHandPrimary.GetComponent<Collider>());
        //Push.transform.position = targetHandPrimary.transform.position;
        Vector3 force = targetHandPrimary.GetComponentInChildren<ForceDirection>().getDirection();
        Push.GetComponent<Rigidbody>().velocity = force * 300;
        Debug.DrawRay(targetHandPrimary.transform.position, Vector3.Scale(force, new Vector3(20,20,20)), Color.green, 10f);
        Debug.Log("Spawned Forceball");
    }

    private void moveLightsaber(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
        if (LastUsedLightSaber == null){
            return;
        }
        Debug.Log(source);
        Debug.Log("Pulling Object");
        targetHandPrimary = GameObject.Find("/Player/SteamVRObjects/RightHand");
        if (source == SteamVR_Input_Sources.LeftHand){
            targetHandPrimary =  GameObject.Find("/Player/SteamVRObjects/LeftHand");
        }
        
        if (!targetHandPrimary.GetComponent<Hand>().ObjectIsAttached(LastUsedLightSaber)){
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

    private void stopSecondary(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
        secondaryIsActive = false;
    }
}
