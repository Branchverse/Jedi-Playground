using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcePushScript : MonoBehaviour
{
    public Rigidbody rb;

    GameObject player;

    private Vector3 targetVelocity;

    // TODO: change direction v3 to input
    void Start ()
    {  
        /*
        player = GameObject.FindWithTag("Player");
        direction = player.transform.forward;
        rb.velocity = direction * 100;
        */
        
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
        Debug.Log("Collided with "+ collider.transform.root.gameObject.tag);
        Rigidbody bodyOfCollider = collider.gameObject.GetComponent<Rigidbody>();
        if (bodyOfCollider)
        {
            bodyOfCollider.AddForce( GetComponent<Rigidbody>().velocity * 100);
        }

        // don't know if this is needed, ignores collision with player
        if (collider.tag == "Player")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collider.GetComponent<Collider>());
            return;
        }

        //or children of the player gameObject
        if (collider.transform.root.gameObject.tag == "Player"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collider.GetComponent<Collider>());
            return;
        }

        if (collider.gameObject.layer == 3){
             Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collider.GetComponent<Collider>());
            return;
        }

        // kill on wall
        if (collider.GetType() == typeof(MeshCollider)) 
        {
            Destroy(gameObject);
        } 

        Debug.Log("Collided without issues");
    }
}
