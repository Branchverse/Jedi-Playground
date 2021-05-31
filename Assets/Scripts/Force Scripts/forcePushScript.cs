using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcePushScript : MonoBehaviour
{
    public Rigidbody rb;
    Vector3 direction;
    GameObject player;

    // TODO: change direction v3 to input
    void Start()
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

    private void OnCollisionEnter (Collision collision) 
    {
        if (collision.gameObject == null){
            return;
        }
        // don't know if this is needed, ignores collision with player 
        if (collision.gameObject.tag == "Player")
        {
            print(gameObject.GetComponent<Collider>());
            print(collision.gameObject.GetComponent<Collider>());
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());

        }

        //or children of the player gameObject
        if (collision.gameObject.transform.root.gameObject.tag == "Player"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }

        // kill on wall
        if (collision.collider.GetType() == typeof(MeshCollider)) 
        {
            Destroy(gameObject);
        } 

    }
}
