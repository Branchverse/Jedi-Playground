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
    int rotate;

    public bool isFloating;

    private float targetAngle;
    private float newTargetAngle;
    private float rotateAngle;

    private Rigidbody rigidbody;

    private bool hasScreamed = false;
    private IEnumerator Wandercoroutine;

    [SerializeField]
    private AudioSource screamingSound;
    [SerializeField]
    private AudioSource movementSound;
    [SerializeField]
    private AudioSource talkSoundLong;
    [SerializeField]
    private AudioSource talkSoundShort1;
    [SerializeField]
    private AudioSource talkSoundShort2;
    [SerializeField]
    private AudioSource collisionSound;

    private bool isTalking = false;

    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
       rigidbody = GetComponent<Rigidbody>();
       rotate = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(isFloating){
            if (!hasScreamed) {
                screamingSound.Play();
                hasScreamed = true;
            }
            return;
        } else {
            hasScreamed = false;
        }

        if(isWandering == false){
            StartCoroutine(Wander());
        }
        if(isRotating == true){
            if(targetAngle == null || targetAngle == 0.0f){
                targetAngle = Random.Range(1.0f, 360.0f);
            }

            rotateAngle = rotateAngle + 1.0f;
            
            if(targetAngle <= 180.0f){
                transform.Rotate(0.0f, 0.0f, 1.0f); 
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

            movementSound.loop = true;
            movementSound.Play();
        } else {
            movementSound.Stop();
        }
        if (!isTalking) {
            StartCoroutine(playTalkingSound());
        }
    }

    IEnumerator Wander(){
        
        isWandering = true;

        yield return new WaitForSeconds(Random.Range(1, 3));
        animator.SetBool("isWalking", true);
        yield return new WaitForSeconds(1);
        isWalking = true;
        yield return new WaitForSeconds(Random.Range(1, 5));
        animator.SetBool("isWalking", false);
        isWalking = false;
        yield return new WaitForSeconds(Random.Range(1, 3));
        if(rotate == 2){
            isRotating = true;
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
        rotate = Random.Range(1, 3);
        isWandering = false;
    }

    IEnumerator playTalkingSound() {  
        isTalking = true;
        yield return new WaitForSeconds(Random.Range(1, 45));
        List<AudioSource> talkingSounds = new List<AudioSource>() { talkSoundLong, talkSoundShort1, talkSoundShort2 };
        talkingSounds[Random.Range(0, talkingSounds.Count)].Play();
        isTalking = false;    
    }

    void OnCollisionEnter(Collision other){
        animator.SetBool("isWalking", false);
        isWalking = false;
        animator.SetBool("isColliding", true);
        rotate = 2;

        if (isFloating) {
            collisionSound.Play();
        }
        return;
    }

    void OnTriggerEnter(Collider other){
        isWalking = false;
        isRotating = true;
        targetAngle = 180f;
    }

    void OnCollisionExit(Collision other){
        animator.SetBool("isColliding", false);
        return;
    }
}
