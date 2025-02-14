using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public Transform canonRestPoint;
    public Console console;

    public void Fire()
    {
        // Instantiate the projectile at the spawn point
        GameObject proj = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Get the Rigidbody component of the projectile
        Rigidbody projRigidbody = proj.GetComponent<Rigidbody>();

        projRigidbody.velocity = projectileSpawnPoint.forward * 15f;
    }

    public void returnToAnchor()
    {
        console.AddLine("returnToAnchor");
        if (projectileSpawnPoint != null)
        {
            console.AddLine("ouiii" + transform.position + " / " + projectileSpawnPoint.position);
            //this.transform.position = new Vector3(1, 0, 3.3f);
            this.transform.position = new Vector3(0.9431f, -0.06980002f, 3.0791f);
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        
    }
}
