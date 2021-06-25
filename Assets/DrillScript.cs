using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DrillScript : MonoBehaviour
{
    private Animator animator;
    private Interactable interactable;
    public SteamVR_Action_Boolean activateDrill;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable == null) { return; }

        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            if (activateDrill[source].state || Input.GetKey("space"))
            {
                if(!animator.GetBool("isPressed")){animator.SetBool("isPressed",true);Debug.Log("turning the drill on");}
            }
            else if(animator.GetBool("isPressed")){animator.SetBool("isPressed",false);Debug.Log("turning the drill off");}
        }
    }
}
