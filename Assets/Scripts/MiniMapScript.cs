using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MissionEvent;

public class MiniMapScript : MonoBehaviour
{
    public EventDriver eventDriver;
    public GameObject changeAntennaLocation;
    public GameObject waterBroccoliLocation;
    public GameObject changeSolarPannelLocation;
    public GameObject repairCableLocation;
    public GameObject fixPipesLocation;
    public GameObject rechargeBatteryLocation;
    public GameObject destroyMeteoriteLocation;
    public GameObject changeThermalShieldLocation;
    public GameObject answerToPCLocation;
    public GameObject enterDigicodeLocation;
    private Dictionary<MissionKind, GameObject> missionLocations;
    // Start is called before the first frame update
    void Start()
    {
        missionLocations = new Dictionary<MissionKind, GameObject>
        {
            { MissionKind.CHANGE_ANTENNA, changeAntennaLocation },
            { MissionKind.CHANGE_SOLAR_PANNEL, changeSolarPannelLocation },
            { MissionKind.CHANGE_THERMAL_SHIELD, changeThermalShieldLocation },
            { MissionKind.RECHARGE_BATTERY,  rechargeBatteryLocation },
            { MissionKind.FIX_PIPES, fixPipesLocation },
            { MissionKind.DESTROY_METEORITE, destroyMeteoriteLocation },
            { MissionKind.REPAIR_CABLES, repairCableLocation },
            { MissionKind.WATER_BROCCOLI, waterBroccoliLocation },
            { MissionKind.ENTER_DIGICODE, enterDigicodeLocation },
            { MissionKind.ANSWER_TO_PC, answerToPCLocation}
        };

        foreach (var v in missionLocations)
        {
            v.Value.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateMap()
    {
        foreach (var e in missionLocations)
        {
            e.Value.SetActive(eventDriver.isEventActive(e.Key));
        }
    }
    private void completeEvent(MissionKind kind)
    {
        GameObject gameObject;
        missionLocations.TryGetValue(kind, out gameObject);
        gameObject.SetActive(false);
    }
    private void startEvent(MissionKind kind)
    {
        GameObject gameObject;
        missionLocations.TryGetValue(kind, out gameObject);
        gameObject.SetActive(true);
    }
}
