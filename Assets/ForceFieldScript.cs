using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldScript : MonoBehaviour
{
    public bool hasGravityOnLeave;

    void OnTriggerExit(Collider other){
        if (other.GetComponent<Rigidbody>() == null){
            return;
        }
       other.GetComponent<Rigidbody>().useGravity = hasGravityOnLeave;
    }
}
