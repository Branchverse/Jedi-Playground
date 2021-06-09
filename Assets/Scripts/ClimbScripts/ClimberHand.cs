using UnityEngine;
using Valve.VR;
public class ClimberHand : MonoBehaviour
{
    public SteamVR_Input_Sources Hand;
    public int TouchedCount;
    public bool grabbing;
    
    void OnTriggerEnter(Collider other)
    {     
        if (other.CompareTag("Climbable"))
        {
            Debug.Log("Touching Climbable");
            TouchedCount++;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            TouchedCount--;
        }
    }
}