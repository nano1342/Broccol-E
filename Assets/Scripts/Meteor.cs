using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public int healthPoints = 10;

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
        Destroy(gameObject);
    }
}
