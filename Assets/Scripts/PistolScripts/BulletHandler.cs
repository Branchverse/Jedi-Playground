using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PistolController;

public class BulletHandler : MonoBehaviour
{
    public float bulletSpeed = 30f;
    public GameObject BulletTexture;


    private Vector3 LastPosition;
    private Vector3 StartPosition;


    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        //BulletUpdate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BulletUpdate();
    }

    void BulletUpdate()
    {
        LastPosition = transform.position;
        if (gameObject.transform.localScale.y > -0.5f)
        {
            float currentYScale = gameObject.transform.localScale.y;
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, Mathf.Clamp(currentYScale -= bulletSpeed * Time.fixedDeltaTime, -1f, 1f), gameObject.transform.localScale.z);
        }
        
    }


    void OnTriggerEnter(Collider other){ 
        
        //pistol may not shoot itself
        if (other.gameObject.GetComponent<PistolController>() != null || other.gameObject.GetComponent<WeaponManager>() != null){
            Debug.Log("returned");
            return;
        }

        //lightsaber reflects shots    
        if (other.gameObject.GetComponent<Reflecting>() != null){
            Debug.Log("reflecting this bullet hehe");
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity * -1;
            if (other.gameObject.GetComponent<Reflecting>().IsActive){
                Debug.Log("Reflector active");
                //fill this
            }
            return;
        }

        // Trainingsphere is getting hit
        if(other.gameObject.GetComponentInParent<ShootSphereScript>() !=null)
        {
            Debug.Log("Bullet encountered a Trainingsphere");
            Destroy(gameObject);
            other.gameObject.GetComponentInParent<ShootSphereScript>().markSphereAsHit();
            return;
        }


        

        RaycastHit hit;
        Physics.Raycast(LastPosition-gameObject.GetComponent<Rigidbody>().velocity.normalized, gameObject.GetComponent<Rigidbody>().velocity, out hit, 10, ~(1<<2));
        
        
        //Debug.Log(hit.point);
        //Debug.DrawRay(StartPosition, gameObject.GetComponent<Rigidbody>().velocity, Color.magenta );
        //This does not work properly
        //GameObject bulletHoleClone = Instantiate(BulletTexture, hit.point, Quaternion.LookRotation(hit.normal)); 
        Debug.Log("Bullet collided!");
        
        // Destroy the bullethole
        //Destroy(bulletHoleClone, 5f);   // Destroyed after 5 seconds
        
        // Destroy the bullet
        Destroy(gameObject,5f);     
    }
}
