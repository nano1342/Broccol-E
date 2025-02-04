using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMissionRepairCables : MonoBehaviour
{
    public EventDriver eventDriver;
    public GameObject LED;
    public Material matGreen;

    public void sumbitCompletion()
    {
        eventDriver.completeEvent(MissionEvent.MissionKind.REPAIR_CABLES);
        LED.GetComponent<MeshRenderer>().material = matGreen;
    }

}
