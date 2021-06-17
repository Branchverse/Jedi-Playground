using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class colorBlade : MonoBehaviour 
{
    public Color color;
    public float rFloat;
    public float gFloat;
    public float bFloat;
    public float aFloat;
    public Renderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
     aFloat = 1;   
     renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("fuck off");
        if(Input.GetKey(KeyCode.R)){
            if(rFloat < 1){
                rFloat += 0.1f;
            }else{
                rFloat = 0.0f;
            }
        }

        if(Input.GetKey(KeyCode.G)){
            if(gFloat < 1){
                gFloat += 0.1f;
            }else{
                gFloat = 0.0f;
            }
        }

        if(Input.GetKey(KeyCode.B)){
            if(bFloat < 1){
                bFloat += 0.1f;
            }else{
                bFloat = 0.0f;
            }
        }

        if(Input.GetKey(KeyCode.A)){
            if(aFloat < 1){
                aFloat += 0.1f;
            }else{
                aFloat = 0.0f;
            }
        }

        color = new Color(rFloat, gFloat, bFloat, aFloat);
        renderer.material.color = color;
    }
}
