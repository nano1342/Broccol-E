using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMissionWaterBroccoli : MonoBehaviour
{
    public EventDriver eventDriver;
    public GameObject LED;
    public Material matGreen;


    public void sumbitCompletion()
    {
        eventDriver.completeEvent(MissionEvent.MissionKind.WATER_BROCCOLI);
        LED.GetComponent<MeshRenderer>().material = matGreen;
    }

}
