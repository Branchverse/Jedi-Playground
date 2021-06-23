using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverBox : MonoBehaviour
{
    public bool isInBox;
    // Start is called before the first frame update
    void Start()
    {
        isInBox = false;
        gameObject.tag = "leverBox";
        gameObject.name = "leverBox";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider target){
        if(target.tag == "lever" ){ 
            isInBox = true;
        }
    }

    private void OnTriggerExit(Collider target){
        if(target.tag == "lever" ){ 
            isInBox = false;
        }
    }
}
