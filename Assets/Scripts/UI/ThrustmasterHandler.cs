using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThrustmasterHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Lever")
        {
            Debug.Log("Reset-Lever activated");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
