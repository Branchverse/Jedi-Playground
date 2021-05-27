using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcePushSpawn : MonoBehaviour
{
    public GameObject forcePush;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: change input to controller
        if (Input.GetKeyDown("space"))
        {
            Instantiate(forcePush, transform.position, transform.rotation);
        }
    }
}
