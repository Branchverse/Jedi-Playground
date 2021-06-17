using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public GameObject explosion;
    public AudioSource explosionSound;
    private IEnumerable coroutine;
    private GameObject obj;

    private void OnTriggerEnter(Collider other)
    {
        obj = other.gameObject;
        while(obj.GetComponent<Rigidbody>() == null)
        {
            obj = obj.transform.parent.gameObject;
        }
        Debug.Log($"{obj} detected and will be destroyed with a BANG");
        StartCoroutine(Waiter());
        Waiter();
    }
    
    public IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1f);
        explosion.GetComponent<ParticleSystem>().Play(true);
        explosionSound.Play();
        Destroy(obj,0.05f);
    }
}
