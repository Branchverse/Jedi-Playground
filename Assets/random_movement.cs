using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_movement : MonoBehaviour
{
    private Animator animator;

    public float moveSpeed = -3f;
    
    private bool isWandering = false;
    private bool isRotating = false;
    private bool isWalking = false;
    int rotate = Random.Range(1, 3);

    private float targetAngle;
    private float newTargetAngle;
    private float rotateAngle;

    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isWandering == false){
            StartCoroutine(Wander());
        }
        if(isRotating == true){
            if(targetAngle == null || targetAngle == 0.0f){
                targetAngle = Random.Range(1.0f, 360.0f);
                //Debug.Log("targetAngle is " + targetAngle);
            }

            rotateAngle = rotateAngle + 1.0f;
            
            if(targetAngle <= 180.0f){
                transform.Rotate(0.0f, 0.0f, 1.0f); 
               // Debug.Log("target smaller");
                if(rotateAngle >= targetAngle){
                isRotating = false;
                targetAngle = 0.0f;
                rotateAngle = 0.0f;
                return;
            }
            }

            if(targetAngle > 180.0f){
                newTargetAngle = 360.0f - targetAngle;
                transform.Rotate(0.0f, 0.0f, -1.0f);
               // Debug.Log("target higher subtract: " + newTargetAngle);
                if(rotateAngle >= newTargetAngle){
                isRotating = false;
                targetAngle = 0.0f;
                rotateAngle = 0.0f;
                return;
            }
            }
        }
        if(isWalking == true){
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator Wander(){
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 3);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        animator.SetBool("isWalking", true);
        yield return new WaitForSeconds(1);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        animator.SetBool("isWalking", false);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if(rotate == 2){
            isRotating = true;
            yield return new WaitForSeconds(rotTime);
        }
        rotate = Random.Range(1, 3);
        isWandering = false;
    }

    void OnCollisionEnter(Collision other){
        animator.SetBool("isWalking", false);
        isWalking = false;
        animator.SetBool("isColliding", true);
        rotate = 2;
        return;
    }

    void OnCollisionExit(Collision other){
        animator.SetBool("isColliding", false);
        return;
    }
}
