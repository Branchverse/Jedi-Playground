using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeHandler : MonoBehaviour
{
    public GameObject sphere1;
    public GameObject sphere2;
    public GameObject sphere3;

    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        sphere2.SetActive(false);
        sphere3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateSphereTrainingBlaster()
    {
        active = !active;
        if(active)
        {
            Debug.Log("Handler activates BlasterTraining");
            sphere2.SetActive(true);
            sphere3.SetActive(true);
            sphere1.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
            sphere2.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
            sphere3.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
        }
        else
        {
            sphere1.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
            sphere2.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
            sphere3.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
            sphere2.SetActive(false);
            sphere3.SetActive(false);
        }
        
    }
    public void activateSphereTrainingSaber()
    {

    }
}
