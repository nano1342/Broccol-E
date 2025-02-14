using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public int healthPoints = 10;
    //public EventDriver eventDriver;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision meteor : " + collision.ToString());    //REGARDER LE PRINT et mettre la même chose dans la collision du projectile
        healthPoints--;
        if (healthPoints <= 0)
        {
            die();
        }
    }

    public void die()
    {
        GameObject eventDriverObj = GameObject.Find("MissionsManager");
        EventDriver eventDriver = eventDriverObj.GetComponent<EventDriver>();
        eventDriver.completeEvent(MissionEvent.MissionKind.DESTROY_METEORITE);
        Destroy(gameObject);
    }
}
