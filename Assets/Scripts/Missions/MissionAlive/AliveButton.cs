using TMPro;
using UnityEngine;

public class AliveButton : MonoBehaviour
{
    public BlinkingText blinkingText;
    public MissionAlive scriptMission;
    public void OnButtonClick()
    {
        blinkingText.StopBlinking();
        scriptMission.completeMission(); 
    }
}