using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionWaterBroccoli : MonoBehaviour
{
    public EventDriver eventDriver;
    public ParticleSystem fire;

    public void handleNewMissionEvent()
    {
        List<MissionEvent> activeMissions = eventDriver.getEventsSnapshot();

        foreach (MissionEvent e in activeMissions)
        {
            if (e.missionKind == MissionEvent.MissionKind.WATER_BROCCOLI)
            {
                startMission();
                return;
            }
        }
    }

    public void startMission()
    {
        fire.Play();
    }

    public void completeMission()
    {
        eventDriver.completeEvent(MissionEvent.MissionKind.WATER_BROCCOLI);
    }
}
