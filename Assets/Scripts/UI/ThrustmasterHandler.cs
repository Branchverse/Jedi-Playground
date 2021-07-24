using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class ThrustmasterHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Lever")
        {
            Debug.Log("Reset-Lever activated");

            var Scenename = SceneManager.GetActiveScene().name;

            //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            SteamVR_LoadLevel.Begin(Scenename);
        }
    }
}
