using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PistolController : MonoBehaviour
{
    private Interactable interactable;
    public SteamVR_Action_Boolean shoot;

    [SerializeField]
    [Tooltip("The Bullet emerges from this point.")]
    private Transform bulletSpawner;

    [SerializeField]
    [Tooltip("Bullet flight speed.")]
    private float bulletSpeed = 30f; //make sure to also change bulletSpeed in BulletHandler

    [SerializeField]
    [Tooltip("Bullet will disappear after this time in seconds.")]
    private float bulletLifeTime;

    [SerializeField]
    private GameObject bulletPrefab;

    private Vector3 shootDirection;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    //int i = 0;
    void Update()
    {
        //i++;
        //if(i > 30){Shoot(); i = 1;}
        if (Input.GetKeyDown("space")) { Shoot(); } //until VR tester implements the correct key this will be the way to go
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;


            if (shoot[source].stateDown)
            {
                Shoot();
                Debug.Log("Bullet has been shot.");
            }
        }
    }

    void Shoot()
    {
        // Create bullet with proper rotation
        GameObject bulletPrefabClone = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
        bulletPrefabClone.transform.Rotate(90f, 0, 0); //unclean methode but I didn't manage to spawn it with the correct rotation
        Rigidbody bulletPrefabCloneRB = bulletPrefabClone.GetComponent<Rigidbody>();
        // Give bullet speed
        bulletPrefabCloneRB.AddForce(bulletSpawner.forward * bulletSpeed, ForceMode.Impulse);
        // Destroy bullet after Lifetime ends
        Destroy(bulletPrefabClone, bulletLifeTime);
    }

}
