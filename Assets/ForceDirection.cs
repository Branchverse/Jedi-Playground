using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDirection : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 getDirection() {
        Vector3 start =  new Vector3(0,0,0);
        Vector3 end = new Vector3(0,0,0);
        for (int i = 0; i < this.gameObject.transform.childCount - 1; i++){
            if (this.gameObject.transform.GetChild(i).transform.name == "Startpoint"){
                start = this.gameObject.transform.GetChild(i).transform.localPosition;
            } 
            if (this.gameObject.transform.GetChild(i).transform.name == "Endpoint"){
                end = this.gameObject.transform.GetChild(i).transform.localPosition;
            }
        }
        return end-start;
    }
}
