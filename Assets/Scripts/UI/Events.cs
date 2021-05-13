using UnityEngine;
using Valve.VR.InteractionSystem;

public class Events : MonoBehaviour
{
    public void OnPress(Hand hand)
    {
        Debug.Log("Button Button pressed!");
    }
}