using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GateMover : MonoBehaviour
{
    private bool openTheGate = false;
    private bool closeTheGate = false;

    private float lowestPosition = -3.740645f;
    private float highestPosition = 1.169354f;

    private float openSpeed = 1f;
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
        float currentPosition = gameObject.transform.localPosition.y;
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, Mathf.Clamp(currentPosition + (openSpeed * Time.deltaTime), lowestPosition, highestPosition), gameObject.transform.localPosition.z);
        if(gameObject.transform.localPosition.y == highestPosition)
        {
            openTheGate = false;
            Debug.Log("Gate fully open now");
        }
    }

    private void CloseGate()
    {
        float currentPosition = gameObject.transform.localPosition.y;
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, Mathf.Clamp(currentPosition + (openSpeed * -Time.deltaTime), lowestPosition, highestPosition), gameObject.transform.localPosition.z);
        if(gameObject.transform.localPosition.y == lowestPosition)
        {
            closeTheGate = false;
            Debug.Log("Gate fully clsoed now");
        }
    }

    public void activateOpening()
    {
        openTheGate = true;
    }

    public void activateClosing()
    {
        closeTheGate = true;
    }
}
