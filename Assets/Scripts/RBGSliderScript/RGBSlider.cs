using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;




public class RGBSlider : MonoBehaviour
{

    [Header("Slider")]
    public LinearMapping R_Slider;
    public LinearMapping G_Slider;
    public LinearMapping B_Slider;
    public GameObject ColorShow;
    private Color color;

    void Update()
    {
        color = new Color(R_Slider.value * 255, G_Slider.value * 255, B_Slider.value * 255, 1f);
        //Debug.Log(color);

        ColorShow.GetComponent<Renderer>().material.SetColor("_BaseColor", color);

        }
}
