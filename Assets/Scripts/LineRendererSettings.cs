using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererSettings : MonoBehaviour
{

    [SerializeField] LineRenderer rend;
    Vector3[] points;

    public GameObject panel;
    public Image image;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<LineRenderer>();

        points = new Vector3[2];

        points[0] = Vector3.zero;
        points[1] = transform.position + new Vector3(0,0,20);

        rend.SetPositions(points);
        rend.enabled = true;

        image = panel.GetComponent<Image>();
 
    }

    // Update is called once per frame
    void Update()
    {
        AlignLineRenderer(rend);
        rend.material.color = rend.startColor;

        if (AlignLineRenderer(rend) && Input.GetAxis("XRI_Right_Trigger") > 0)
            {
                button.onClick.Invoke();
            }   
    }

    public LayerMask layerMask;

    public bool AlignLineRenderer(LineRenderer rend)
    {
        bool hitButton = false;

        Ray ray;
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, layerMask))
        {
            rend.startColor = Color.red;
            rend.endColor = Color.red;
            points[1] = transform.forward + new Vector3(0,0,hit.distance);
            button = hit.collider.gameObject.GetComponent<Button>();
            hitButton = true;
            
        }
        else 
        {
            rend.startColor = Color.red;
            rend.endColor = Color.red;
            points[1] = transform.forward + new Vector3(0,0,20);
            hitButton = false;
        }

        rend.SetPositions(points);
        return hitButton;
    }

    public void ColorChangeOnClick() {
        if (button != null) {
            if (button.name == "StartBtn") {
                image.color = Color.green;
            }
            else if (button.name == "SettingsBtn") {
                image.color = Color.blue;
            }
            else if (button.name == "ExitBtn") {
                image.color = Color.red;
            }
        }
    }
}

