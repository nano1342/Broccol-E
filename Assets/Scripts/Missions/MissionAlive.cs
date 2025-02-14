using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionAlive : MonoBehaviour
{
    public EventDriver eventDriver;
    public BlinkingText blinkingText;
    public void handleNewMissionEvent()
    {
        List<MissionEvent> activeMissions = eventDriver.getEventsSnapshot();

        foreach (MissionEvent e in activeMissions)
        {
            if (e.missionKind == MissionEvent.MissionKind.ANSWER_TO_PC)
            {
                startMission();
                return;
            }
        }
    }

    public void startMission()
    {
        blinkingText.StartBlinking();
    }

    public void completeMission()
    {
        eventDriver.completeEvent(MissionEvent.MissionKind.ANSWER_TO_PC);
    }
}
