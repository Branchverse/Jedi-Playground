using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The blade object")]
    private GameObject _blade;
     
    [SerializeField]
    [Tooltip("The empty game object located at the tip of the blade")]
    private GameObject _tip = null;

    [SerializeField]
    [Tooltip("The empty game object located at the base of the blade")]
    private GameObject _base = null;
    
    [SerializeField]
    [Tooltip("The amount of force applied to each side of a slice")]
    private float _forceAppliedToCut = 3f;

    [SerializeField]
    [Tooltip("The extend speed in seconds.")]
    private float extendSpeed = 0.05f ;

    [SerializeField]
    [Tooltip("The maximum extension of the Blade.")]
    private float maximumSwordSize;

    private float minimumSwordSize = 0.0f;
    private bool weaponTurnedOn;
    public SteamVR_Action_Boolean activateBlade;
    private Interactable interactable;
    private Vector3 _triggerEnterTipPosition;
    private Vector3 _triggerEnterBasePosition;
    private Vector3 _triggerExitTipPosition; 

    [SerializeField]
    private AudioSource turnOnSound, turnOffSound, hummingSound, cuttingSound;
    
    // Start is called before the first frame update   
    void Start() 
    {
        interactable = GetComponent<Interactable>();
    }

    void Awake()
    {
        UpdateWeapon();
    }

//here are testing variables not to be used in the final produkt
int i = 0;
Vector3 vector = new Vector3(-1.0f,0f);
    // Update is called once per frame
    void Update()
    {   
        //if(i%120 == 0){weaponTurnedOn = !weaponTurnedOn;Debug.Log("Weapon state changed");}i++; //for blade function tests without input. 
        if (interactable == null){
            return;
        }
         if (interactable.attachedToHand != null){
             SteamVR_Input_Sources source = interactable.attachedToHand.handType;
             FindObjectOfType<PlayerStats>().setLastLightsaber(transform.gameObject);
             if (activateBlade[source].stateDown){
                 weaponTurnedOn = !weaponTurnedOn;
                 Debug.Log("Weapon state changed");
             }
         }
        UpdateWeapon();
    }

    private void OnTriggerEnter(Collider other)
    {
        // FindObjectOfType<AudioManager>().StopPlaying("saber_humming");
        // FindObjectOfType<AudioManager>().Play("saber_cut");
        hummingSound.Stop();
        cuttingSound.Play();
        // cuttingSound.loop = true;

        Debug.Log("Enter Triggered");
        Debug.Log(other);
        _triggerEnterTipPosition = _tip.transform.position;
        _triggerEnterBasePosition = _base.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Sliceable>() == null){
            return;
        }
        Debug.Log("Exit Triggered");
        _triggerExitTipPosition = _tip.transform.position;

        //Create a triangle between the tip and base so that we can get the normal
        Vector3 side1 = _triggerExitTipPosition - _triggerEnterTipPosition;
        Vector3 side2 = _triggerExitTipPosition - _triggerEnterBasePosition;

        //Get the point perpendicular to the triangle above which is the normal
        //https://docs.unity3d.com/Manual/ComputingNormalPerpendicularVector.html
        Vector3 normal = Vector3.Cross(side1, side2).normalized;

        //Transform the normal so that it is aligned with the object we are slicing's transform.
        Vector3 transformedNormal = ((Vector3)(other.gameObject.transform.localToWorldMatrix.transpose * normal)).normalized;

        //Get the enter position relative to the object we're cutting's local transform
        Vector3 transformedStartingPoint = other.gameObject.transform.InverseTransformPoint(_triggerEnterTipPosition);

        Plane plane = new Plane();

        plane.SetNormalAndPosition(
                transformedNormal,
                transformedStartingPoint);

        var direction = Vector3.Dot(Vector3.up, transformedNormal);

        //Flip the plane so that we always know which side the positive mesh is on
        if (direction < 0)
        {
            plane = plane.flipped;
        }

        GameObject[] slices = Slicer.Slice(plane, other.gameObject);
        Destroy(other.gameObject);

        // FindObjectOfType<AudioManager>().StopPlaying("saber_cut");
        // FindObjectOfType<AudioManager>().Play("saber_humming");
        cuttingSound.Stop();
        hummingSound.Play();
        hummingSound.loop = true;

        Rigidbody rigidbody = slices[1].GetComponent<Rigidbody>();
        Vector3 newNormal = transformedNormal + Vector3.up * _forceAppliedToCut;
        rigidbody.AddForce(newNormal, ForceMode.Impulse);
    }

    void UpdateWeapon()
    {
        float extendDelta = maximumSwordSize / extendSpeed;
        float currentSize = _blade.transform.localScale.y;

        if(weaponTurnedOn)
        {
            _blade.transform.localScale = new Vector3(_blade.transform.localScale.x, Mathf.Clamp(currentSize + (extendDelta * -Time.deltaTime), minimumSwordSize, maximumSwordSize), _blade.transform.localScale.z);
        
            if(_blade.transform.localScale.y <= 0.1)
            {
                // FindObjectOfType<AudioManager>().StopPlaying("saber_humming");
                // FindObjectOfType<AudioManager>().Play("saber_turn_off");
                hummingSound.Stop();
                turnOffSound.Play();
                _blade.SetActive(false);
            }
            foreach (Transform eachChild in transform){
                if (eachChild.name == "DeflectHitbox"){
                    eachChild.GetComponent<Reflecting>().IsActive = true;
                    break;
                }
            }
        }
        else
        {
            if(_blade.activeSelf==false)
            {
                // FindObjectOfType<AudioManager>().Play("saber_turn_on");
                // FindObjectOfType<AudioManager>().Play("saber_humming");
                turnOnSound.Play();
                hummingSound.Play();
                hummingSound.loop = true;
                _blade.SetActive(true);
            }
            _blade.transform.localScale = new Vector3(_blade.transform.localScale.x, Mathf.Clamp(currentSize + (extendDelta * Time.deltaTime), minimumSwordSize, maximumSwordSize), _blade.transform.localScale.z);
            
            foreach (Transform eachChild in transform){
                if (eachChild.name == "DeflectHitbox"){
                    eachChild.GetComponent<Reflecting>().IsActive = false;
                }
            }
        }
    }
}
