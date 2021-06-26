using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShootingRangeHandler : MonoBehaviour
{
    public GameObject sphere1;
    public GameObject sphere2;
    public GameObject sphere3;

    private bool blasterActive = false;
    private bool saberActive = false;
    private float floatTimer = 0f;

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
    void FixedUpdate()
    {
        if (saberActive)
        {
            floatTimer += Time.fixedDeltaTime;
            if (floatTimer >= 19)
            {
                floatTimer = 0f; 
                activateSphereTrainingSaber();
            }
        }
    }

    public void activateSphereTrainingBlaster()
    {
        blasterActive = !blasterActive;
        if (blasterActive)
        {
            Debug.Log("Handler activates BlasterTraining");
            if (saberActive) { activateSphereTrainingSaber(); }
            sphere2.SetActive(true);
            sphere3.SetActive(true);
            sphere1.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
            sphere2.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
            sphere3.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
        }
        else
        {
            Debug.Log("Handler deactivates SaberTraining");
            sphere1.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
            sphere2.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
            sphere3.GetComponent<ShootSphereScript>().activateSphereTrainingBlaster();
            sphere2.SetActive(false);
            sphere3.SetActive(false);
        }

    }
    public void activateSphereTrainingSaber()
    {
        floatTimer = 0f;
        saberActive = !saberActive;

        if (saberActive)
        {
            Debug.Log("Handler activates SaberTraining");
            if (blasterActive) { activateSphereTrainingBlaster(); }
            sphere1.GetComponent<ShootSphereScript>().activateSphereTrainingSaber();
        }
        else
        {
            Debug.Log("Handler deactivates SaberTraining");
            sphere1.GetComponent<ShootSphereScript>().activateSphereTrainingSaber();
        }
    }
}
