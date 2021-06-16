using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public Material mat;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject + " " + other.gameObject.GetInstanceID());
        
        other.gameObject.AddComponent<BurnMthrFkrBURN>();
        other.gameObject.GetComponent<BurnMthrFkrBURN>().initiateBurn(mat);
        GameObject obj = other.gameObject;
        while(obj.GetComponent<Rigidbody>() == null)
        {
            obj = obj.transform.parent.gameObject;
        }
        Destroy(obj,3f);
    }
}
