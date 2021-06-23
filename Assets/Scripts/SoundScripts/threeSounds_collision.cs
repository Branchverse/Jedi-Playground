using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class threeSounds_collision : MonoBehaviour
{
    [SerializeField]
    private AudioSource collision_sound1;
    [SerializeField]
    private AudioSource collision_sound2;
    [SerializeField]
    private AudioSource collision_sound3;

    private List<AudioSource> sounds;

     // Start is called before the first frame update
    void Start()
    {
        sounds =  new List<AudioSource>() {collision_sound1, collision_sound2, collision_sound3};
    }

    private void OnCollisionEnter(Collision collision) 
    {
        sounds[Random.Range(0, sounds.Count)].Play();
    }
}
