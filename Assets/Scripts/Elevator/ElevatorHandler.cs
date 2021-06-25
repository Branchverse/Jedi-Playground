using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ElevatorHandler : MonoBehaviour
{
    //public AudioSource audioSource;

    public bool movingUp = false;
    public bool movingDown = false;
    private bool isSoundPlaying = false;

    public float lowestPosition;
    public float highestPosition;

    private float openSpeed = 1.3f;

    private bool PlayerInElevator;

    private Rigidbody rb;

    private GameObject PlayerObject;
    // Start is called before the first frame update
    void Start()
    {
       // rb = GetComponent<Rigidbody>();
       // Physics.IgnoreLayerCollision(0,10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingUp)
        {
            moveElevatorUpwards();
        }

        if (movingDown)
        {
            moveElevatorDownwards();
        }

    }

    private void moveElevatorUpwards()
    {
        float currentPosition = gameObject.transform.localPosition.y;
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, Mathf.Clamp(currentPosition + (openSpeed * Time.deltaTime), lowestPosition, highestPosition), gameObject.transform.localPosition.z);
        if (PlayerInElevator){
            PlayerObject.GetComponent<JoyStickMovement>().PlayerGravity = -openSpeed;
            //PlayerObject.transform.position = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.y + (openSpeed * Time.deltaTime), PlayerObject.transform.position.z);
            //PlayerObject.GetComponent<Rigidbody>().useGravity = false;
            //PlayerObject.GetComponent<Rigidbody>().velocity = new Vector3(0,openSpeed,0);
        }
        if(gameObject.transform.localPosition.y == highestPosition)
        {
            if (PlayerInElevator){
                PlayerObject.GetComponent<JoyStickMovement>().PlayerGravity = 9.81f;
            }   
            movingUp = false;
            isSoundPlaying = false;
        }
    }

    private void moveElevatorDownwards()
    {
        float currentPosition = gameObject.transform.localPosition.y;
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, Mathf.Clamp(currentPosition + (openSpeed * -Time.deltaTime), lowestPosition, highestPosition), gameObject.transform.localPosition.z);
        PlayerObject.GetComponent<JoyStickMovement>().PlayerGravity = openSpeed;
        if(gameObject.transform.localPosition.y == lowestPosition)
        {
            if (PlayerInElevator){
                PlayerObject.GetComponent<JoyStickMovement>().PlayerGravity = 9.81f;
            }   
            movingDown = false;
            isSoundPlaying = false;
        }
    }

    public void moveUp()
    {
        movingUp = true;
        movingDown = false;
    }

    public void moveDown()
    {
        movingUp = false;
        movingDown = true;
    }

    public void moveAdapt(){
        Debug.Log("Moving");
        if (gameObject.transform.localPosition.y == lowestPosition){
            movingUp = true;
            movingDown = false;
        } else if (gameObject.transform.localPosition.y == highestPosition){
            movingDown = true;
            movingUp = false;
        }
    }

    public void  OnTriggerEnter(Collider other){
        Debug.Log("Player entered");
        if (other.gameObject.transform.root.GetComponent<Player>() != null){
            PlayerInElevator = true;
            PlayerObject = other.gameObject.transform.root.gameObject;
            //PlayerObject.transform.parent = transform;
        }
    }

     public void  OnTriggerExit(Collider other){
        if (other.gameObject.transform.root.GetComponent<Player>() != null){
            PlayerInElevator = false;
            PlayerObject = null;
            //PlayerObject.transform.parent = null;
        }
    }
}


