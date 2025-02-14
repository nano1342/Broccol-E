using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public Console console;

    public GameObject XRRig;
    public GameObject TPAnchor;
    public EventDriver eventDriver;

    public void startMainGame()
    {
        console.AddLine("Start game !!! depuis le startGame");
        XRRig.transform.position = TPAnchor.transform.position;
        eventDriver.startNewEventSession();
    }
    public void startMainGameDebug()
    {
        console.AddLine("Start game !!! depuis le startGame");
        XRRig.transform.position = TPAnchor.transform.position;
        eventDriver.startNewEventSession();
    }

    public void closeApp()
    {
        Application.Quit();
    }

    public void setVisible(bool visible)
    {
        if (visible)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
