using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crate_collision_sound : MonoBehaviour
{
    [SerializeField]
    private AudioSource collision_sound_crate1;
    [SerializeField]
    private AudioSource collision_sound_crate2;


    private List<AudioSource> crateSounds;

     // Start is called before the first frame update
    void Start()
    {
        crateSounds =  new List<AudioSource>() {collision_sound_crate1, collision_sound_crate2};
    }

    private void OnCollisionEnter(Collision collision) 
    {
        crateSounds[Random.Range(0, crateSounds.Count)].Play();
    }
}
