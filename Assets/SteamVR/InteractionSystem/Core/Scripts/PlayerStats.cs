using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerStats
{
    private Interactable LastEquippedLightSaber;


    // Start is called before the first frame update
    void setLastLightsaber(Interactable Lightsaber) {
        this.LastEquippedLightSaber = Lightsaber;
    }

    // Update is called once per frame
    Interactable getLastLightsaber() {
        return this.LastEquippedLightSaber;
    }
}
