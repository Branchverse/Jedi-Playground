using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SaberEnterHandler : MonoBehaviour
{
    private AutomatHandler Automat;
    void Start()
    {
        Automat = transform.parent.GetComponent<AutomatHandler>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other){
        if (other.transform.root.GetComponent<Player>() != null){
            return;
        }
        if (other.GetComponent<WeaponManager>() != null){   
            Debug.Log("Saber entered");       
            //Turn off saber
            other.GetComponent<WeaponManager>().weaponTurnedOff = true;
            if (Automat.getLightsaber() == null){
                Debug.Log("Set Lightsaber");
                Automat.setLightsaber(other.gameObject);
            }               
        }
    }

    private void OnTriggerExit(Collider other){     
        if(other.gameObject.GetComponent<HandPhysics>() != null){
            other.gameObject.GetComponent<HandPhysics>().collisionsEnabled = true;
            return;
        }     
    }
}
