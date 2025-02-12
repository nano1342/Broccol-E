using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public Transform canonRestPoint;

    public void Fire()
    {
        // Instantiate the projectile at the spawn point
        GameObject proj = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Get the Rigidbody component of the projectile
        Rigidbody projRigidbody = proj.GetComponent<Rigidbody>();

        projRigidbody.velocity = projectileSpawnPoint.forward * 10f;
    }

    public void returnToAnchor()
    {
        transform.position = projectileSpawnPoint.position;
    }
}
