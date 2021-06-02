using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class Climber : MonoBehaviour
{
    public ClimberHand RightHand;
    public ClimberHand LeftHand;
    public SteamVR_Action_Boolean ToggleGripButton;
    public SteamVR_Action_Pose position;
    public ConfigurableJoint ClimberHandle;

    private bool Climbing;
    private ClimberHand ActiveHand;

    private Vector3 Startposition;


    void FixedUpdate()
    {
        updateHand(RightHand);
        updateHand(LeftHand);
        if (Climbing)
        {
            Debug.Log("Now moving to " +ActiveHand.transform.localPosition);       
            GameObject.Find("Player").transform.position = -ActiveHand.transform.localPosition + Startposition;
        }
    }

    void updateHand(ClimberHand Hand)
    {
        if (Climbing && Hand == ActiveHand)//if is the hand used for climbing check if we are letting go.
        {
            if (ToggleGripButton.GetStateUp(Hand.Hand))
            {
                ClimberHandle.connectedBody = null;
                Climbing = false;
                GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else
        {
            if (ToggleGripButton.GetStateDown (Hand.Hand)||Hand.grabbing)
            {            
                Hand.grabbing = true;
                if (Hand.TouchedCount > 0)
                {
                    Startposition = GameObject.Find("Player").transform.position;
                    ActiveHand = Hand;
                    Climbing = true;
                    ClimberHandle.transform.position = Hand.transform.position;
                    GetComponent<Rigidbody>().useGravity = false;
                    ClimberHandle.connectedBody = GetComponent<Rigidbody>();
                    Hand.grabbing = false;
                }
            }
        }
    }
}