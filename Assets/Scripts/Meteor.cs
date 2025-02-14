using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public int healthPoints = 10;
    //public EventDriver eventDriver;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject consoleObj = GameObject.Find("GOConsole");
        if (consoleObj != null )
        {
            Console console = consoleObj.GetComponent<Console>();
            console.AddLine("arriv���� jivhng");
        }

        Debug.Log("collision meteor : " + collision.ToString());    //REGARDER LE PRINT et mettre la m�me chose dans la collision du projectile
        healthPoints--;
        if (healthPoints <= 0)
        {
            die();
        }
    }

    public void die()
    {
        GameObject consoleObj = GameObject.Find("GOConsole");
        if (consoleObj != null)
        {
            Console console = consoleObj.GetComponent<Console>();
            console.AddLine("DIE");
        }
        GameObject eventDriverObj = GameObject.Find("MissionsManager");
        EventDriver eventDriver = eventDriverObj.GetComponent<EventDriver>();
        eventDriver.completeEvent(MissionEvent.MissionKind.DESTROY_METEORITE);
        Destroy(this.gameObject);
    }
}
