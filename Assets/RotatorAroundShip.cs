using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorAroundShip : MonoBehaviour
{
    public Vector3 rotationValue;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationValue);
    }
}
