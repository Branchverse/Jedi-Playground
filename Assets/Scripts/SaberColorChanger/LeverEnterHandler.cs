using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverEnterHandler : MonoBehaviour
{
    private AutomatHandler Automat;
    void Start()
    {
        Automat = transform.parent.GetComponent<AutomatHandler>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other){
        if (other.transform.tag == "Lever"){
            Debug.Log("Lever activated");
            Automat.activateColorChange();
        }
    }
}

