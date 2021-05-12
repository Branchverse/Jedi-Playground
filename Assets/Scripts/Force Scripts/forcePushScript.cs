using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcePushScript : MonoBehaviour
{
    public Rigidbody rb;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(-10, 0, 0);
        rb.velocity = direction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
