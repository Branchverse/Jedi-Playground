using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_bullet_collision : MonoBehaviour
{
    [SerializeField]
    private AudioSource collision_sound;

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.collider.tag == "Player")
        {
            collision_sound.Play();
        }
    }
}
