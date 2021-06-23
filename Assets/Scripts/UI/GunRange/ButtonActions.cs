using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActions : MonoBehaviour
{
    [SerializeField]
    private AudioSource press_sound;

    public enum Usage
    {
        ElevatorToggle,
        ElevatorCall,
        RangeSaber,
        RangeBlaster,
        MixerStart,
        RollingGateToggle
    }

    public enum Direction
    {
        none,
        up,
        down
    }

    public Usage UseAs;
    public Direction direction;

    public virtual void OnTrigger()
    {
        if (press_sound)
        {
            press_sound.Play();
        }
        
        switch(UseAs)
        {
            case Usage.ElevatorToggle:
                // check Elevator Level, move accordingly
                break;

            case Usage.ElevatorCall:
                break;

            case Usage.RangeSaber:
                Debug.Log("Me iz SaberButton");
                break;

            case Usage.RangeBlaster:
                Debug.Log("Me iz BlasterButton");
                break;

            case Usage.MixerStart:
                break;
                
            case Usage.RollingGateToggle:
                Debug.Log("Me iz RollingGate");
                break;
                /*
                case Usage.ElevatorToggle:
                    break;

                case Usage.ElevatorToggle:
                    break;
                */

        }


    }
}
