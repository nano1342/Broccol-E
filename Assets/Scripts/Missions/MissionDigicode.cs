using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDigicode : MonoBehaviour
{
    public EventDriver eventDriver;
    public ScreenManager screenManager;
    public void handleNewMissionEvent()
    {
        List<MissionEvent> activeMissions = eventDriver.getEventsSnapshot();

        foreach (MissionEvent e in activeMissions)
        {
            if (e.missionKind == MissionEvent.MissionKind.ENTER_DIGICODE)
            {
                startMission();
                return;
            }
        }
    }

    public void startMission()
    {
        screenManager.GenerateRandomCode();
    }

    public void completeMission()
    {
        eventDriver.completeEvent(MissionEvent.MissionKind.ENTER_DIGICODE);
    }
}
