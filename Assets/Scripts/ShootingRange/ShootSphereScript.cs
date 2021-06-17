using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootSphereScript : MonoBehaviour
{
    // public GameObject firstDouble;
    // public GameObject secondDouble; 
    private bool active = false;
    private bool first = true;
    private bool blasterMode = false;
    private bool saberMode = false;
    private Animator animator;
    private Vector3 initialPosition = new Vector3(-7.268f,2.389f,-35.315f);
    private float yMax = 6.146f;
    private float yMin = 2.11f;
    private float xMax = -3f;
    private float xMin = -10f;
    private float zMax = -36.358f;
    private float zMin = -44.621f;


    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        gameObject.transform.position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("l")){activateSphereTrainingBlaster();}
        if(Input.GetKeyDown("2")){activateSphereTrainingSaber();}
    }
    public void activateSphereTrainingBlaster()
    {
        Debug.Log("SphereTrainingBlaster has been activated");
        active = !active;
        blasterMode = !blasterMode;
        if(active && saberMode){saberMode = false;}
        if(active && blasterMode)
        {
            markSphereAsHit();
        }
        else{resetSphere();} 
    }
    public void activateSphereTrainingSaber()
    {
        Debug.Log("SphereTrainingSaber has been activated");
        active = !active;
        saberMode = !saberMode;
        if(active && blasterMode){blasterMode = false;}
        if(active)
        {

        }
        else{resetSphere();}
    }

    public void repositionSphere()
    {
        if(active)
        {
        Debug.Log("Sphere-AnimationEvent has been triggered woop woop");
        Vector3 vector  = new Vector3();
        vector.x = Random.Range(xMin,xMax);
        vector.y = Random.Range(yMin,yMax);
        vector.z = Random.Range(zMin,zMax);

        gameObject.transform.position = vector;
        if(first){gameObject.transform.localScale = new Vector3(2,2,2);}
        }
        
    }

    public void markSphereAsHit()
    {
        if(active && blasterMode)
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
