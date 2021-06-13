using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class machine : MonoBehaviour
{
    [SerializeField] private Vector3 destination = new Vector3(0.00012f, -0.00196f, 0.0117f);
    [SerializeField] private float speed = 0.5f;

    private Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider target){
            if( target.tag == "box" && interactable.attachedToHand == null){
                transform.position = Vector3.MoveTowards(transform.position, target.bounds.center, speed * Time.deltaTime);
            }
        }
}