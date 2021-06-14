using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_collision_sound : MonoBehaviour
{
    [SerializeField]
    private AudioSource collisionSound;

    private void OnCollisionEnter (Collision collision)
    {
        collisionSound.Play();
    }
}
