using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMissionFixPipes : MonoBehaviour
{
    public EventDriver eventDriver;
    public GameObject LED;
    public Material matGreen;

    public void sumbitCompletion()
    {
        eventDriver.completeEvent(MissionEvent.MissionKind.FIX_PIPES);
        LED.GetComponent<MeshRenderer>().material = matGreen;
    }

}
