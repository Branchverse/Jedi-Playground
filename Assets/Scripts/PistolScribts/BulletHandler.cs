using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PistolController;

public class BulletHandler : MonoBehaviour
{
    private float bulletSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    void OnTriggerEnter()
    {
        Debug.Log("asdasd");
        //Destroy(gameObject);
    }

    // OnTrigger Destroy
    // Destroy(gamObject)
}
