using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class force_field_sound : MonoBehaviour
{
    [SerializeField]
    private AudioSource hummingSound;

    // Start is called before the first frame update
    void Start()
    {
        hummingSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
