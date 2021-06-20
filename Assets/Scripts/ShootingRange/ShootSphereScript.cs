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
    private float saberYMax = 6.23f;
    private float saberYMin = 2.469f;
    private float saberXMax = -3.824f;
    private float saberXMin = -10.817f;
    private float saberZMax = -36.724f;
    private float saberZMin = -42.0f;
    public CharacterController playerHitBox;
    private Vector3 dif;
    private Vector3 deltaPosition;
    private bool moving;
    private int counter;
    private int movingCounter;
    public GameObject bulletSpawner;
    public GameObject childWithCollider;
    public GameObject bulletPrefab;


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

    void FixedUpdate()
    {
        if(saberMode && counter>=150){counter=0;choiceMaker();}
        else if(saberMode){counter++;}
        if(moving){transform.position += deltaPosition; movingCounter++;if(movingCounter == 51){moving = false;movingCounter = 0;}}
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
            childWithCollider.layer = LayerMask.NameToLayer("Bullet");
            levitateToNewPosition();
        }
        else{resetSphere();childWithCollider.layer = LayerMask.NameToLayer("Default");}
    }
    private void resetSphere()
    {
        gameObject.transform.position = initialPosition;
        gameObject.transform.localScale = new Vector3(1,1,1);
    }

    public void repositionSphere() // Blastermode
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

    public void choiceMaker()
    {
        Debug.Log("SaberTrainer is making a choice!");
        if(Random.value >=0.3f){shootAtPlayer();}
        else{levitateToNewPosition();}
    }
    private void shootAtPlayer()
    {
        Debug.Log("Sphere is shooting");
        counter=80;
        Vector3 target = RandomPointInBounds(playerHitBox.bounds);
        Debug.Log(target + "is the target for the sphere");
        Vector3 direction = (target - bulletSpawner.transform.position).normalized;
        Debug.DrawLine(bulletSpawner.transform.position,bulletSpawner.transform.position + direction * 30,Color.red,Mathf.Infinity);

        // Shoot
        GameObject bulletPrefabClone = Instantiate(bulletPrefab, bulletSpawner.transform.position, Quaternion.Euler(direction.x-90f,direction.y,direction.z));
        bulletPrefabClone.GetComponent<Rigidbody>().AddForce(direction * 30f, ForceMode.Impulse);
        Destroy(bulletPrefabClone, 3f);
    }
    private void levitateToNewPosition()
    {
        Debug.Log("Sphere is moving");
        Vector3 newPosition = new Vector3(
        Random.Range(saberXMin, saberXMax),
        Random.Range(saberYMin, saberYMax),
        Random.Range(saberZMin, saberZMax));
        Vector3 dif = newPosition - transform.position;
        deltaPosition = dif / 50;
        moving = true;
    }

    public static Vector3 RandomPointInBounds(Bounds bounds) {
    return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
}
}
