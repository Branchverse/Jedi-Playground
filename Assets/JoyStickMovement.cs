using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class JoyStickMovement : MonoBehaviour
{

    public SteamVR_Action_Vector2 JoystickInput;
    
    public float PlayerSpeed;
    public float PlayerGravity;

    public float PlayerJiggle = 0;

    private CharacterController PlayerController;

    void Start(){
        PlayerController = GetComponent<CharacterController>();
    }

    void FixedUpdate(){
        Vector3 playerOrientation = Player.instance.hmdTransform.TransformDirection(new Vector3(JoystickInput.axis.x + PlayerJiggle,0,JoystickInput.axis.y+PlayerJiggle));
        PlayerController.Move(PlayerSpeed * Time.deltaTime * Vector3.ProjectOnPlane(playerOrientation, Vector3.up) - new Vector3(0,PlayerGravity,0)* Time.deltaTime);
        if (PlayerJiggle != 0){
            PlayerJiggle = PlayerJiggle * -1;
        }
    }
}
