using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public Material blackMatter;
    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        Destroy(other.gameObject,3f);
        other.gameObject.GetComponent<MeshRenderer>().material = blackMatter;
    }
}
