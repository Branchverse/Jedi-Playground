using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Lightsaber : MonoBehaviour
{

    public SteamVR_Action_Boolean activateBlade;
    public bool bladeIsActive;

    private Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.attachedToHand != null){
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            Debug.Log(source.ToString());
            if (activateBlade[source].stateDown){
                wroom();
            }
        }
    }

    public void wroom () {
        Debug.Log("wow this is a lightsaber");
    }
}
