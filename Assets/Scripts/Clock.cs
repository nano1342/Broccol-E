using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public EventDriver eventDriver;
    public TMPro.TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        setAsDefault();
    }

    // Update is called once per frame
    void Update()
    {
        if (eventDriver.isRunning())
        {
            var timeLeftMs = eventDriver.getGlobalTimeLeftMs();
            if (timeLeftMs == long.MaxValue)
            {
                setAsDefault();
            } else if (timeLeftMs <= 0)
            {
                text.text = "00:00\nThe vessel exploded.\nGame over.";
                text.faceColor = new Color32(255, 88, 120, 240); // red
            } else
            {
                var convertedTime = msToStrMinutesSeconds(timeLeftMs);
                var leftMinutes = convertedTime.Item2;
                var leftSeconds = convertedTime.Item3;
                var timeStr = convertedTime.Item1;
                text.text = timeStr;
                if (leftMinutes == 0 && leftSeconds < 30)
                {
                    text.faceColor = new Color32(255, 88, 120, 240); // red
                } else
                {
                    text.faceColor = new Color32(88, 255, 228, 240); // blue
                }

                var activeEvents = eventDriver.getEventsSnapshot();
                foreach (var e in activeEvents)
                {
                    var convertedEventTime = msToStrMinutesSeconds(e.getTimeLeftMs());
                    var str = convertedEventTime.Item1;
                    text.text += "\n" + missionKindToName(e.missionKind) + " - " + str;
                }
            }
        } else
        {
            setAsDefault();
        }
    }

    private void setAsDefault()
    {
        text.faceColor = new Color32(88, 255, 228, 240); // blue
        text.text = "--:--";
    }

    private (string, float, float) msToStrMinutesSeconds(double ms)
    {
        var leftMinutes = Mathf.Floor((float)ms / 1_000 / 60);
        var leftSeconds = Mathf.Floor((float)ms / 1_000 % 60);
        var minutesStr = leftMinutes.ToString();
        var secondsStr = leftSeconds.ToString();
        if (leftMinutes < 10) minutesStr = "0" + minutesStr;
        if (leftSeconds < 10) secondsStr = "0" + secondsStr;
        return (minutesStr + ":" + secondsStr, leftMinutes, leftSeconds);
    }

    private string missionKindToName(MissionEvent.MissionKind kind)
    {
        if (kind == MissionEvent.MissionKind.CHANGE_ANTENNA) return "Change antenna";
        else if (kind == MissionEvent.MissionKind.CHANGE_SOLAR_PANNEL) return "Change solar pannel";
        else if (kind == MissionEvent.MissionKind.CHANGE_THERMAL_SHIELD) return "Change thermal shield";
        else if (kind == MissionEvent.MissionKind.DESTROY_METEORITE) return "Destroy meteorite";
        else if (kind == MissionEvent.MissionKind.FIX_PIPES) return "Fix pipes";
        else if (kind == MissionEvent.MissionKind.RECHARGE_BATTERY) return "Recharge battery";
        else if (kind == MissionEvent.MissionKind.REPAIR_CABLES) return "Repair cables";
        else if (kind == MissionEvent.MissionKind.WATER_BROCCOLI) return "Water broccoli";
        else if (kind == MissionEvent.MissionKind.ANSWER_TO_PC) return "Answer to PC";
        else if (kind == MissionEvent.MissionKind.ENTER_DIGICODE) return "Enter digicode";
        else throw new System.Exception("Unknown kind");
    }
}
