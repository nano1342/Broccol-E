using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMeteor : MonoBehaviour
{
    public EventDriver eventDriver;
    public GameObject asteroidPrefab;
    public Transform asteroidSpawnPoint;
    public Transform asteroidEndPoint;

    public void handleNewMissionEvent()
    {
        List<MissionEvent> activeMissions = eventDriver.getEventsSnapshot();

        foreach (MissionEvent e in activeMissions)
        {
            if (e.missionKind == MissionEvent.MissionKind.DESTROY_METEORITE)
            {
                startMission();
                return;
            }
        }
    }

    public void startMission()
    {
        GameObject meteor = Instantiate(asteroidPrefab, asteroidSpawnPoint.position, asteroidSpawnPoint.rotation);
        meteor.GetComponent<Rigidbody>().velocity = asteroidSpawnPoint.forward * 10f;

    }


}
