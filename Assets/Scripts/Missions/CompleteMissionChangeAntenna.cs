using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMissionChangeAntenna : MonoBehaviour
{
    public EventDriver eventDriver;
    public GameObject LED;
    public Material matGreen;

    public void sumbitCompletion()
    {
        eventDriver.completeEvent(MissionEvent.MissionKind.CHANGE_ANTENNA);
        LED.GetComponent<MeshRenderer>().material = matGreen;
    }

}
