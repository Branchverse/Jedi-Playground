using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class machine : MonoBehaviour
{
    [SerializeField] private Vector3 box;
    private float speed;
    private float speedEx;
    //private Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        //interactable = GetComponent<Interactable>();
        box = new Vector3(-18.4f, 2.65f, -27.465f);
        speed = 0.1f;
        speedEx = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
      transform.position = Vector3.MoveTowards(transform.position, box, Time.deltaTime * speedEx);
    }
    private void OnTriggerEnter (Collider target){
            if(target.tag == "box" ){ //&& target.Collider.bounds.Contains(transform.position) oder interactable.attachedToHand == null ?! pls send help
                Debug.Log(box);
                transform.position = Vector3.MoveTowards(transform.position, box, Time.deltaTime * speed);
                transform.Rotate(90, 0, 0);
            }
    } 

    private void OnTriggerExit (Collider target){
        if(target.tag == "box"){
            transform.Rotate(-90, 0, 0);
        }
    }
}