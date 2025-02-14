using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public Console console;

    public GameObject XRRig;
    public GameObject TPAnchor;
    public EventDriver eventDriver;
    public bool debug = false;

    public void startMainGame()
    {
        console.AddLine("Start game !!! depuis le startGame");
        XRRig.transform.position = TPAnchor.transform.position;
        eventDriver.startNewEventSession();
    }
    public void startMainGameDebug()
    {
        debug = true;
        console.AddLine("Start game !!! depuis le startGame");
        XRRig.transform.position = TPAnchor.transform.position;
        eventDriver.startNewEventSession();
    }

    public void closeApp()
    {
        Application.Quit();
    }
}
