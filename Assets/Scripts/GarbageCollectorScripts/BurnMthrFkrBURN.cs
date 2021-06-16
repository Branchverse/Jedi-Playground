using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnMthrFkrBURN : MonoBehaviour
{
    private bool canBeBurned = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        if (canBeBurned)
        {
            gameObject.GetComponent<MeshRenderer>().materials[1].SetFloat("_Cutoff", gameObject.GetComponent<MeshRenderer>().materials[1].GetFloat("_Cutoff") - 0.01f);
        }
    }
    public void initiateBurn(Material mat)
    {
        var mats = gameObject.GetComponent<MeshRenderer>().materials;
        Debug.Log(mats);
        mats[1] = mat;
        gameObject.GetComponent<MeshRenderer>().materials = mats;
        canBeBurned = true;
    }
}
