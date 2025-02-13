using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMeteor : MonoBehaviour
{
    public EventDriver eventDriver;
    public GameObject asteroidPrefab;

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

    }
}
