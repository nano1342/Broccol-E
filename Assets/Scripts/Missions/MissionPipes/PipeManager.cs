using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject pipeNullPrefab;
    public GameObject pipeiPrefab;
    public GameObject pipeLPrefab;
    public GameObject pipe_Prefab;
    public GameObject pipeTPrefab;
    public GameObject pipeCrossPrefab;

    private Dictionary<int, GameObject> pipePrefabs;

    void Awake()
    {
        // Associer chaque type de tuyau à son Prefab
        pipePrefabs = new Dictionary<int, GameObject>
        {
            { 0, pipeNullPrefab }, // Tuyau vide
            { 1, pipeiPrefab }, // Tuyau en i
            { 2, pipeLPrefab }, // Tuyau en L
            { 3, pipe_Prefab }, // Tuyau en -
            { 4, pipeTPrefab },  // Tuyau en T
            { 5, pipeCrossPrefab } // Tuyau en +
        };
    }

    public GameObject GetPipePrefab(int type)
    {
        if (pipePrefabs.TryGetValue(type, out var prefab))
        {
            return prefab;
        }

        Debug.LogWarning($"Aucun prefab trouvé pour le type : {type}");
        return null;
    }
}