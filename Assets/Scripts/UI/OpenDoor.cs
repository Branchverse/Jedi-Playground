/* using UnityEngine;
using Valve.VR.InteractionSystem;

/*
 * This class is attached to a door handle. The door handle is child of a door.
 *
public class OpenDoor : MonoBehaviour
{
    private Vector3 force;
    private Vector3 cross;
    private bool holdingHandle;
    private float angle;
    private const float forceMultiplier = 150f;

    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetStandardInteractionButton())
        {
            holdingHandle = true;

            // Direction vector from the door's pivot point to the hand's current position
            Vector3 doorPivotToHand = hand.transform.position - transform.parent.position;

            // Ignore the y axis of the direction vector
            doorPivotToHand.y = 0;

            // Direction vector from door handle to hand's current position
            force = hand.transform.position - transform.position;

            // Cross product between force and direction. 
            cross = Vector3.Cross(doorPivotToHand, force);
            angle = Vector3.Angle(doorPivotToHand, force);
        }
        else if (hand.GetStandardInteractionButtonUp())
        {
            holdingHandle = false;
        }
    }

    void Update()
    {
        if (holdingHandle)
        {
            // Apply cross product and calculated angle to
            GetComponentInParent<Rigidbody>().angularVelocity = cross * angle * forceMultiplier;
        }
    }

    private void OnHandHoverEnd()
    {
        // Set angular velocity to zero if the hand stops hovering
        GetComponentInParent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
*/ 