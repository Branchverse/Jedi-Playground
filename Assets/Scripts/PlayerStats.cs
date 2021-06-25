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

    private SteamVR_Action_Boolean SwapHandMode; 

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

    private LineRenderer targetIndicator;



    private void Awake() {
            isPulling = SteamVR_Actions._default.GrabPinch;

            isUsingSecondary = SteamVR_Actions._default.GrabGrip;

            SwapHandMode = SteamVR_Actions._default.ActivateMenu;

            //Primary Action
            isPulling[SteamVR_Input_Sources.LeftHand].onStateDown += useForce;
            isPulling[SteamVR_Input_Sources.RightHand].onStateDown += useForce;
            isPulling[SteamVR_Input_Sources.Any].onStateUp += stopLightsaberPull;

            //Secondary Action
            isUsingSecondary[SteamVR_Input_Sources.LeftHand].onStateDown += useSecondary; 
            isUsingSecondary[SteamVR_Input_Sources.RightHand].onStateDown += useSecondary;
            isUsingSecondary[SteamVR_Input_Sources.RightHand].onStateUp += stopSecondary;
            isUsingSecondary[SteamVR_Input_Sources.LeftHand].onStateUp += stopSecondary;


           SwapHandMode[SteamVR_Input_Sources.Any].onStateDown += changeForce;

           targetIndicator = GetComponent<LineRenderer>();
           targetIndicator.positionCount = 0; 

    }

    void start() {
        m_ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
    }

    void FixedUpdate() {

        //Check if Primary is active
        if (isPullingSaber && !targetHandPrimary.GetComponent<Hand>().ObjectIsAttached(LastUsedLightSaber)){
            if (targetHandPrimary.GetComponent<Hand>().hoverLocked){
                Debug.Log("Is holding object");
                return;
            }
            Vector3 LightSaberVelocity = targetHandPrimary.transform.position - LastUsedLightSaber.transform.position;
            //scale Vector to length 10
            LastUsedLightSaber.GetComponent<Rigidbody>().velocity = Vector3.Scale( Vector3.Normalize(LightSaberVelocity), new Vector3(10,10,10));
            Vector3 SabertoTargetDelta = targetHandPrimary.transform.position - LastUsedLightSaber.transform.position;
            if (SabertoTargetDelta.magnitude<0.25){
                Valve.VR.InteractionSystem.Hand Hand =  targetHandPrimary.GetComponent<Hand>();     
                Vector3 offset = LastUsedLightSaber.transform.position - LastUsedLightSaber.transform.GetChild(0).gameObject.transform.position;
                LastUsedLightSaber.transform.position = Hand.transform.position + offset;      
                Hand.AttachObject(LastUsedLightSaber, Hand.GetBestGrabbingType(),LastUsedLightSaber.GetComponent<Throwable>().attachmentFlags, LastUsedLightSaber.GetComponent<Throwable>().attachmentOffset);           
                isPullingSaber = false;
            }
        }

        if (secondaryIsActive && targetHandSecondary.GetComponent<Hand>().currentAttachedObjectInfo == null ){   
            var targetPoint = targetHandSecondary.transform.position + targetHandSecondary.GetComponentInChildren<ForceDirection>().getDirection().normalized * SecondaryGameObjectDistance;
            //Speedmodifier to make object feel more 
            ObjectForSecondary.GetComponent<Rigidbody>().velocity = (targetPoint - ObjectForSecondary.transform.position) * 5;
        }
    }


    private void useForce(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
         targetHandPrimary = GameObject.Find("/Player/SteamVRObjects/RightHand");
        if (source == SteamVR_Input_Sources.LeftHand){
            targetHandPrimary =  GameObject.Find("/Player/SteamVRObjects/LeftHand");
        }  
        if (HandMode == 0){
            moveLightsaber(action_Boolean, source);
        } else if (HandMode == 1){
            forcePush(action_Boolean, source);
        }
    }

    private void changeForce(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
        if (HandMode == 0){
            HandMode = 1;
            return;
        }
        HandMode = 0;
    }

    private void useSecondary(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){
        targetHandSecondary = GameObject.Find("/Player/SteamVRObjects/RightHand");
        if (source == SteamVR_Input_Sources.LeftHand){
             targetHandSecondary =  GameObject.Find("/Player/SteamVRObjects/LeftHand");
        }  
        Debug.Log("Now secondary with"+targetHandSecondary.transform.name);
        if (targetHandSecondary.GetComponent<Hand>().currentAttachedObjectInfo != null){
            Debug.Log("is holding Object");
            return;
        }
        RaycastHit hit;
        Debug.Log("Looking for secondary target");
        //Debug.DrawRay(targetHandSecondary.transform.position, targetHandSecondary.GetComponentInChildren<ForceDirection>().getDirection(), Color.green);
        if(Physics.Raycast( targetHandSecondary.transform.position, targetHandSecondary.GetComponentInChildren<ForceDirection>().getDirection(), out hit)){


            Debug.Log("hit: "+hit.transform.gameObject.name);

            targetIndicator.positionCount = 2;
            targetIndicator.SetPosition(0,targetHandSecondary.gameObject.transform.position);
            targetIndicator.SetPosition(1,hit.point);

            //cannot move non rigidbody Object 
            
            if (hit.transform.gameObject.GetComponent<Rigidbody>() == null){
                Debug.Log("Non Rigidbody: "+hit.transform.gameObject.name);
                return;
            }


            ObjectForSecondary = hit.transform.gameObject;
            secondaryIsActive = true;
             targetIndicator.positionCount = 0;
            SecondaryGameObjectDistance = (ObjectForSecondary.transform.position - targetHandSecondary.transform.position).magnitude;           
            Debug.Log("set secondary target");
            return;
        }
        Debug.Log("No target found");
    }

    private void forcePush(SteamVR_Action_Boolean action_Boolean, SteamVR_Input_Sources source){       
        GameObject Push = Instantiate(ForcePushObject, targetHandPrimary.transform.position, Quaternion.Euler(new Vector3(0,0,0)));
        Physics.IgnoreCollision(ForcePushObject.GetComponent<Collider>(), targetHandPrimary.GetComponent<Collider>());
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
        targetIndicator.positionCount = 0;
    }
}
