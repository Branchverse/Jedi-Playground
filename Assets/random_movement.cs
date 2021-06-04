using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_movement : MonoBehaviour
{
    private Animator animator;

    public float moveSpeed = 3f;
    public float rotSpeed = 100f; //this shit doesn't work.... and idk why aaaargh
    
    private bool isWandering = false;
    private bool isRotating = false;
    private bool isWalking = false;
    int rotate = Random.Range(1, 3);

    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       //var vec = new Vector3(0.0f, 0.0f, Random.Range(10.0f, 340.0f));
         
        if(isWandering == false){
            StartCoroutine(Wander());
        }
        if(isRotating == true){
            transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f)); 
            isRotating = false;
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
        Debug.Log("Oupsi, R2-A2 collided");
        animator.SetBool("isWalking", false);
        isWalking = false;
        animator.SetBool("isColliding", true);
        rotate = 2;
        //yield return new WaitForSeconds(1);
        //isColliding = true;
        return;
    }

    void OnCollisionExit(Collision other){
        Debug.Log("R2-A2 not colliding anymore");
        animator.SetBool("isColliding", false);
        //isColliding = false;
        return;
    }
}
