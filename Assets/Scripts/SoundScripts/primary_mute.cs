using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class primary_mute : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = 0f;
        Invoke("Unmute", 5f);
    }

    void Unmute () 
    {
        AudioListener.volume = 1f;
    }
}
