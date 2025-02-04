using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrabbable : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    public Console console;

    public void spawn()
    {
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        console.AddLine("spawn cube");
    }
}
