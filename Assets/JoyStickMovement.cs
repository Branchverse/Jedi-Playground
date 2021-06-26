using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class JoyStickMovement : MonoBehaviour
{

    public SteamVR_Action_Vector2 JoystickInput;

    private bool lockXmove;
    
    public float PlayerSpeed;
    public float PlayerGravity;

    private CharacterController PlayerController;

    void Start(){
        PlayerController = GetComponent<CharacterController>();
    }

    void FixedUpdate(){
        Vector3 PlayerMovement = Vector3.ProjectOnPlane(Player.instance.hmdTransform.TransformDirection(new Vector3(JoystickInput.axis.x,0,JoystickInput.axis.y)), Vector3.up);
        if (lockXmove){
            if (PlayerMovement.z < 0){
                PlayerMovement.z = 0;
            }
        }
        PlayerController.Move(PlayerSpeed * Time.deltaTime * PlayerMovement - new Vector3(0,PlayerGravity,0)* Time.deltaTime);
    }


    void OnTriggerEnter(Collider other){
        //prevent the player from walking out the hangar
        if (other.transform.name == "EnteringCollider"){
            Debug.Log("Locking");
            lockXmove = true;
        }
    }

     void OnTriggerExit(Collider other){
        //prevent the player from walking out the hangar
        if (other.transform.name == "EnteringCollider"){
            Debug.Log("Locking");
            lockXmove = false;
        }
    }
}
