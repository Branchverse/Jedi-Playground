using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcePushSpawn : MonoBehaviour
{
    public GameObject forcePush;

    void Update()
    {
        // TODO: change input to controller
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(forcePush, transform.position, transform.rotation);
        }
    }
}
