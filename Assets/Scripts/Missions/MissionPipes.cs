using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPipes : MonoBehaviour
{
    public EventDriver eventDriver;
    public PipeGame pipeGame;
    public GameObject LED;
    public Material materialGreen;
    public Material materialRed;

    public void handleNewMissionEvent()
    {
        List<MissionEvent> activeMissions = eventDriver.getEventsSnapshot();

        foreach (MissionEvent e in activeMissions)
        {
            if (e.missionKind == MissionEvent.MissionKind.FIX_PIPES)
            {
                startMission();
                return;
            }
        }
    }

    public void startMission()
    {
        pipeGame.StartGame();
        LED.GetComponent<MeshRenderer>().material = materialRed;
    }

    public void completeMission()
    {
        eventDriver.completeEvent(MissionEvent.MissionKind.FIX_PIPES);
        LED.GetComponent<MeshRenderer>().material = materialGreen;
    }
}
