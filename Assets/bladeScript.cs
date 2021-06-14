using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeScript : MonoBehaviour
{
    public GameObject Saber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Saber.GetComponent<WeaponManager>().OnTriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        Saber.GetComponent<WeaponManager>().OnTriggerExit(other);
    }
}
