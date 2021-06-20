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

    public GameObject Liquid;

    void Update()
    {
        //color = new Color(R_Slider.value * 255, G_Slider.value * 255, B_Slider.value * 255, 1f);
        color = new Color(R_Slider.value, G_Slider.value, B_Slider.value, 1f);
        //Debug.Log(color);

        ColorShow.GetComponent<Renderer>().material.SetColor("_BaseColor", color);

        applyColorChange();
    }

    public void applyColorChange(){
        Liquid.GetComponent<LiquidController>().alterColor(R_Slider.value, G_Slider.value, B_Slider.value);
    }
}
