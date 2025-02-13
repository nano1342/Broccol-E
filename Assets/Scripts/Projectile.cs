using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int i = 0;
    private int iMax = 360000;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision projectile : " + collision.ToString());
        die();
    }

    public void die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (i > iMax)
        {
            die();
        }
    }
}
