using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabActionScript : MonoBehaviour
{

    public SteamVR_Action_Boolean input;    
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;
    void Start()
    {
        input.AddOnStateDownListener(grab, inputSource);
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    private void grab (SteamVR_Action_Boolean action, SteamVR_Input_Sources sources) {
        Debug.Log("grabbed");
    }

}
