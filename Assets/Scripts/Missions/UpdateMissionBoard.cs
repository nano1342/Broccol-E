using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMissionBoard : MonoBehaviour
{
    public Console console;
    public Material materialGreen;
    public Material materialRed;
    public GameObject LEDChangeSolarPanel;
    public GameObject LEDChangeAntenna;
    public GameObject LEDRepairCables;
    public GameObject LEDFixPipes;
    public GameObject LEDRechargeBattery;
    public GameObject LEDWaterBroccoli;
    public GameObject LEDDestroyMeteorite;
    public GameObject LEDChangeThermalShield;

    public void handleNewMissionEvent(MissionEvent e)
    {
        console.AddLine("handleNewMissionEvent");
        console.AddLine(e.ToString());
        if (e.missionKind == MissionEvent.MissionKind.CHANGE_SOLAR_PANNEL)
        {
            console.AddLine("1");
            LEDChangeSolarPanel.GetComponent<MeshRenderer>().material = materialRed;
        }

        if (e.missionKind == MissionEvent.MissionKind.CHANGE_ANTENNA)
        {
            console.AddLine("2");
            LEDChangeAntenna.GetComponent<MeshRenderer>().material = materialRed;
        }

        if (e.missionKind == MissionEvent.MissionKind.REPAIR_CABLES)
        {
            console.AddLine("3");
            LEDChangeThermalShield.GetComponent<MeshRenderer>().material = materialRed;
        }

        if (e.missionKind == MissionEvent.MissionKind.FIX_PIPES)
        {
            console.AddLine("4");
            LEDFixPipes.GetComponent<MeshRenderer>().material = materialRed;
        }

        if (e.missionKind == MissionEvent.MissionKind.RECHARGE_BATTERY)
        {
            console.AddLine("5");
            LEDRechargeBattery.GetComponent<MeshRenderer>().material = materialRed;
        }

        if (e.missionKind == MissionEvent.MissionKind.WATER_BROCCOLI)
        {
            console.AddLine("6");
            LEDWaterBroccoli.GetComponent<MeshRenderer>().material = materialRed;
        }

        if (e.missionKind == MissionEvent.MissionKind.DESTROY_METEORITE)
        {
            console.AddLine("7");
            LEDDestroyMeteorite.GetComponent<MeshRenderer>().material = materialRed;
        }

        if (e.missionKind == MissionEvent.MissionKind.CHANGE_THERMAL_SHIELD)
        {
            console.AddLine("8");
            LEDChangeThermalShield.GetComponent<MeshRenderer>().material = materialRed;
        }

    }
}
