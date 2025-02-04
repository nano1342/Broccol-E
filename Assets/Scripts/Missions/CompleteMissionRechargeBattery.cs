using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMissionRechargeBattery : MonoBehaviour
{
    public EventDriver eventDriver;
    public GameObject LED;
    public Material matGreen;

    public void sumbitCompletion()
    {
        eventDriver.completeEvent(MissionEvent.MissionKind.RECHARGE_BATTERY);
        LED.GetComponent<MeshRenderer>().material = matGreen;
    }

}
