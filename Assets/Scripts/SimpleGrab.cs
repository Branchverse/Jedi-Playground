using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SimpleGrab : MonoBehaviour
{
    private Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    private void HandHoverBegin (Hand hand){
        hand.ShowGrabHint();
    }

    private void HandHoverEnd (Hand hand){
        hand.HideGrabHint();
    }

    private void HandHoverUpdate(Hand hand){
        GrabTypes grabType = hand.GetBestGrabbingType();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);


        if (interactable.attachedToHand == null && grabType != GrabTypes.None){
            hand.AttachObject(gameObject, grabType);
            hand.HoverLock(interactable);
            hand.HideGrabHint();      
        }
        else if (isGrabEnding) {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);
        }
    }

}
