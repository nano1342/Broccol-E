using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMissionDestroyMeteorite : MonoBehaviour
{
    public EventDriver eventDriver;
    public GameObject LED;
    public Material matGreen;

    public void sumbitCompletion()
    {
        eventDriver.completeEvent(MissionEvent.MissionKind.DESTROY_METEORITE);
        LED.GetComponent<MeshRenderer>().material = matGreen;
    }

}
