using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMissionChangeThermalShield : MonoBehaviour
{
    public EventDriver eventDriver;
    public GameObject LED;
    public Material matGreen;

    public void sumbitCompletion()
    {
        eventDriver.completeEvent(MissionEvent.MissionKind.CHANGE_THERMAL_SHIELD);
        LED.GetComponent<MeshRenderer>().material = matGreen;
    }

}
