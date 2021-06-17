using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        while(obj.GetComponent<Rigidbody>() == null)
        {
            obj = obj.transform.parent.gameObject;
        }
        Debug.Log($"{obj} detected and will be destroyed with a BANG");
        Destroy(obj,1.1f);

        explosion.GetComponent<ParticleSystem>().Play(true);
        
    }
}
