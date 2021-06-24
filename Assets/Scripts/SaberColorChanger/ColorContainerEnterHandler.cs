using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ColorContainerEnterHandler : MonoBehaviour
{
    private AutomatHandler Automat;

    private string FillName = "Vector1_c42c52aff56a419384be3fd9560b4f22";
    void Start()
    {
        Automat = transform.parent.GetComponent<AutomatHandler>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other){
        if (other.transform.tag != "ColorContainer"){
            return;
        }
        Debug.Log("Container found");
        // Check if capsule has been used before
        foreach (Transform eachChild in other.transform){
            if (eachChild.name == "liquid"){
                if (eachChild.transform.gameObject.GetComponent<Renderer>().material.GetFloat(FillName) < 0){
                    return;
                }else {
                    break;
                }
            }
        }   

         Debug.Log("Container full");
        //Only set active if no other capsule is already in the machine
        if (Automat.getBarrel() == null){
            Debug.Log("Setting container");        
            Automat.setBarrel(other.gameObject);
        }               
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.GetComponent<HandPhysics>() != null){
            other.gameObject.GetComponent<HandPhysics>().collisionsEnabled = true;
            return;
        }
        if (other.gameObject == Automat.getBarrel()){
            Automat.setBarrel(null);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}

