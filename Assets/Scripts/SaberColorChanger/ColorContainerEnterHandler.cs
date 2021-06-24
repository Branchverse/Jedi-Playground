using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.transform.tag != "colorCapsule"){
            return;
        }
        // Check if capsule has been used before
        foreach (Transform eachChild in other.transform.parent){
            if (eachChild.name == "Capsule"){
                if (eachChild.transform.gameObject.GetComponent<Renderer>().material.GetFloat(FillName) < 0){
                    return;
                }else {
                    break;
                }
            }
        }   

        //Only set active if no other capsule is already in the machine
        if (Automat.getBarrel() == null){
            Automat.setBarrel(other.gameObject);
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.eulerAngles = new Vector3(90,0,0);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }               
    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject == Automat.getBarrel()){
            Automat.setBarrel(null);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}

