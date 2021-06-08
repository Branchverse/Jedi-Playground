using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootSphereScript : MonoBehaviour
{
    private bool active = false;
    private bool first = true;
    private Animator animator;
    private Vector3 initialPosition = new Vector3(-7.268f,2.389f,-35.315f);
    public float yMax;
    public float yMin;
    public float xMax;
    public float xMin;
    public float zMax;
    public float zMin;


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        gameObject.transform.position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("p")){activateSphereTraining();}
    }
    public void activateSphereTraining()
    {
        Debug.Log("SphereTraining has been activated");
        active = !active;
        if(active){markSphereAsHit();}
        else{resetSphere();}
        
    }

    public void repositionSphere()
    {
        Debug.Log("Sphere-AnimationEvent has been triggered woop woop");
        Vector3 vector  = new Vector3();
        vector.x = Random.Range(xMin,xMax);
        vector.y = Random.Range(yMin,yMax);
        vector.z = Random.Range(zMin,zMax);

        gameObject.transform.position = vector;
        if(first){gameObject.transform.localScale = new Vector3(2,2,2);}
    }

    public void markSphereAsHit()
    {
        if(active)
        {
            animator.SetTrigger("hitTrigger");
        }
        
    }
    private void resetSphere()
    {
        gameObject.transform.position = initialPosition;
        gameObject.transform.localScale = new Vector3(1,1,1);
    }
}
