using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public Console console;

    public GameObject XRRig;
    public GameObject TPAnchor;
    public EventDriver eventDriver;

    public void startGame()
    {
        console.AddLine("Start game !!! depuis le startGame");
        XRRig.transform.position = TPAnchor.transform.position;
        eventDriver.startNewEventSession();
    }
}
