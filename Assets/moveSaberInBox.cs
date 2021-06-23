using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class machine : MonoBehaviour
{
    private GameObject box;
    private float speed;
    public bool isInBox;
    //private float speedEx;

    // Start is called before the first frame update
    void Start()
    {
        isInBox = false;
        speed = 0.1f;
        //speedEx = 0.5f;
        box = GameObject.Find("box");
        gameObject.name = "saber";
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, box.transform.position, Time.deltaTime * speedEx);
    }
    private void OnTriggerEnter (Collider target){
            if(target.tag == "box" ){ 
                transform.position = Vector3.MoveTowards(transform.position, box.transform.position, Time.deltaTime * speed);
                transform.Rotate(90, 0, 0);
                isInBox = true;
            }
    } 

    private void OnTriggerExit (Collider target){
        if(target.tag == "box"){
            transform.Rotate(-90, 0, 0);
            isInBox = false;
        }
    }
}