using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GateMover : MonoBehaviour
{
    public AudioSource audioSource;

    private bool openTheGate = false;
    private bool closeTheGate = false;
    private bool isSoundPlaying = false;

    private float lowestPosition = -3.800645f;
    private float highestPosition = 1.169354f;

    private float openSpeed = 1.3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (openTheGate)
        {
            OpenGate();
        }

        if (closeTheGate)
        {
            CloseGate();
        }

        if(Input.GetKeyDown("space"))
        {
            openTheGate = true;
            
        }
    }

    private void OpenGate()
    {
        if(!isSoundPlaying) {
            isSoundPlaying = true;
            audioSource.Play();
        }
        
        float currentPosition = gameObject.transform.localPosition.y;
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, Mathf.Clamp(currentPosition + (openSpeed * Time.deltaTime), lowestPosition, highestPosition), gameObject.transform.localPosition.z);
        if(gameObject.transform.localPosition.y == highestPosition)
        {
            openTheGate = false;
            Debug.Log("Gate fully open now");
            isSoundPlaying = false;
            audioSource.Stop();
        }
    }

    private void CloseGate()
    {
        if(!isSoundPlaying) {
            isSoundPlaying = true;
            audioSource.Play();
        }

        float currentPosition = gameObject.transform.localPosition.y;
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, Mathf.Clamp(currentPosition + (openSpeed * -Time.deltaTime), lowestPosition, highestPosition), gameObject.transform.localPosition.z);
        if(gameObject.transform.localPosition.y == lowestPosition)
        {
            closeTheGate = false;
            Debug.Log("Gate fully clsoed now");
            isSoundPlaying = false;
            audioSource.Stop();
        }
    }

    public void activateOpening()
    {
        closeTheGate = false;
        openTheGate = true;
    }

    public void activateClosing()
    {
        openTheGate = false;
        closeTheGate = true;
    }
}
