using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class WeaponManager : MonoBehaviour
{
    [Tooltip("Hello I am a test Tooltip.")]
    public GameObject bladeHolder;


    // Blade is extended or not.
    private bool weaponTurnedOn;


    private float minimumSwordSize = 0.0f;
    
    [Tooltip("The maximum extension of the Blade.")]
    public float maximumSwordSize;

    [Tooltip("The extend speed in seconds.")]
    public float extendSpeed = 0.1f ;

    // Start is called before the first frame update
    public SteamVR_Action_Boolean activateBlade;

    private Interactable interactable;

    void Start() {
        interactable = GetComponent<Interactable>();
    }
    void Awake()
    {
        UpdateWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.attachedToHand != null){
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            Debug.Log(source.ToString());

            if (activateBlade[source].stateDown){
                weaponTurnedOn = !weaponTurnedOn;
                Debug.Log("Weapon state changed");
            }
        }
        UpdateWeapon();
        
    }

    void UpdateWeapon()
    {
        float extendDelta = maximumSwordSize / extendSpeed;
        float currentSize = bladeHolder.transform.localScale.y;

        if(weaponTurnedOn)
        {
            bladeHolder.transform.localScale = new Vector3(bladeHolder.transform.localScale.x, Mathf.Clamp(currentSize + (extendDelta * -Time.deltaTime), minimumSwordSize, maximumSwordSize), bladeHolder.transform.localScale.z);
        }
        else
        {
            bladeHolder.transform.localScale = new Vector3(bladeHolder.transform.localScale.x, Mathf.Clamp(currentSize + (extendDelta * Time.deltaTime), minimumSwordSize, maximumSwordSize), bladeHolder.transform.localScale.z);

        }
    }
}
