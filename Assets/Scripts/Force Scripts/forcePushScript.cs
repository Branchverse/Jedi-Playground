using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcePushScript : MonoBehaviour
{
    public Rigidbody rb;
    Vector3 direction;
    GameObject player;

    // TODO: change direction v3 to input
    void Start ()
    {  
        player = GameObject.FindWithTag("Player");
        direction = player.transform.forward;
        rb.velocity = direction * 10;

        StartCoroutine("KillAfterTime");
    }

    // kill after 1 second
    private IEnumerator KillAfterTime ()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter (Collider collider)
    {
        Rigidbody bodyOfCollider = collider.gameObject.GetComponent<Rigidbody>();
        if (bodyOfCollider)
        {
            bodyOfCollider.AddForce(direction * 3000);
        }

        // don't know if this is needed, ignores collision with player
        if (collider.tag == "Player")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collider.GetComponent<Collider>());
        }

        // kill on wall
        if (collider.GetType() == typeof(MeshCollider)) 
        {
            Destroy(gameObject);
        } 
    }
}
