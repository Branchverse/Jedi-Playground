using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDirection : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 getDirection() {
        Vector3 start =  this.gameObject.transform.GetChild(0).transform.position;
        Vector3 end = this.gameObject.transform.GetChild(1).transform.position;
        /*
        for (int i = 0; i < this.gameObject.transform.childCount - 1; i++){
            if (this.gameObject.transform.GetChild(i).transform.name == "Startpoint"){
                start = this.gameObject.transform.GetChild(i).transform.localPosition;
            } 
            if (this.gameObject.transform.GetChild(i).transform.name == "Endpoint"){
                end = this.gameObject.transform.GetChild(i).transform.localPosition;
            }
        }
        */
        //Debug.Log("Startposition: "+ this.gameObject.transform.GetChild(0).transform.position);
        //Debug.DrawRay(this.gameObject.transform.GetChild(0).transform.position, (end-start) * 100 , Color.green, 15f);
        return end-start;
    }
}
