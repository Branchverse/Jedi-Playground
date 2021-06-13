using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_music : MonoBehaviour
{
    [SerializeField]
    private AudioSource background_drone;

    // Start is called before the first frame update
    void Start()
    {
        background_drone.Play();
        background_drone.loop = true;
    }
}
