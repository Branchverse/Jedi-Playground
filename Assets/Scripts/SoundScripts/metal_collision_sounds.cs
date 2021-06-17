using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metal_collision_sounds : MonoBehaviour
{
    [SerializeField]
    private AudioSource collision_sound_metal1;
    [SerializeField]
    private AudioSource collision_sound_metal2;
    [SerializeField]
    private AudioSource collision_sound_metal3;

    private List<AudioSource> metalSounds;

     // Start is called before the first frame update
    void Start()
    {
        metalSounds =  new List<AudioSource>() {collision_sound_metal1, collision_sound_metal2, collision_sound_metal3};
    }

    private void OnCollisionEnter(Collision collision) 
    {
        metalSounds[Random.Range(0, metalSounds.Count)].Play();
    }
}
