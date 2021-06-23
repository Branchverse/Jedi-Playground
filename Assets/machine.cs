using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class machine : MonoBehaviour
{
    //[SerializeField] private Vector3 boxV;
    private GameObject blade;
    private GameObject box;
    private float speed;
    private float speedEx;

    private Color color;
    //private Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        //interactable = GetComponent<Interactable>();
        //boxV = new Vector3(-18.4f, 2.65f, -27.465f);
        speed = 0.1f;
        speedEx = 0.5f;
        blade = GameObject.Find("blade");
        box = GameObject.Find("box");
        //color = blade.GetComponent<Renderer>().Material.Color;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("boooox   " + box.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, box.transform.position, Time.deltaTime * speedEx);
    }
    private void OnTriggerEnter (Collider target){
            if(target.tag == "box" ){ //&& target.Collider.bounds.Contains(transform.position) oder interactable.attachedToHand == null ?! pls send help
                //Debug.Log();
                transform.position = Vector3.MoveTowards(transform.position, box.transform.position, Time.deltaTime * speed);
                transform.Rotate(90, 0, 0);
            }
    } 

    private void OnTriggerExit (Collider target){
        if(target.tag == "box"){
            transform.Rotate(-90, 0, 0);
        }
    }
}