using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone_collision_sound : MonoBehaviour
{
    [SerializeField]
    private AudioSource collision_sound_stone1;
    [SerializeField]
    private AudioSource collision_sound_stone2;
    [SerializeField]
    private AudioSource collision_sound_stone3;

    private List<AudioSource> stoneSounds;

     // Start is called before the first frame update
    void Start()
    {
        stoneSounds =  new List<AudioSource>() {collision_sound_stone1, collision_sound_stone2, collision_sound_stone3};
    }

    private void OnCollisionEnter(Collision collision) 
    {
        stoneSounds[Random.Range(0, stoneSounds.Count)].Play();
    }
}
