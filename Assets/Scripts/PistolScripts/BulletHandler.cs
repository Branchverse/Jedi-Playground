using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PistolController;

public class BulletHandler : MonoBehaviour
{
    public float bulletSpeed = 4f;

    public GameObject BulletTexture;

    private Vector3 LastPosition;

    private Vector3 StartPosition;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        BulletUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        BulletUpdate();
    }

    void BulletUpdate()
    {
        if (gameObject.transform.localScale.y > -1)
        {
            Debug.Log("Test");
            float currentYScale = gameObject.transform.localScale.y;
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, Mathf.Clamp(currentYScale -= bulletSpeed * Time.deltaTime, -1f, 0f), gameObject.transform.localScale.z);
        }
        LastPosition = transform.position;
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



        Destroy(gameObject);

        RaycastHit hit;
        Physics.Raycast(LastPosition, gameObject.GetComponent<Rigidbody>().velocity, out hit, 10);
        
        
        //Debug.Log(hit.point);
        //Debug.DrawRay(StartPosition, gameObject.GetComponent<Rigidbody>().velocity, Color.magenta );
        //This does not work properly
        Instantiate(BulletTexture, hit.point, Quaternion.Euler(hit.normal));        
    }
}
