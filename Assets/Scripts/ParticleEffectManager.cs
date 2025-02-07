using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    public GameObject particleEmitterPrefab;
    private GameObject instance;
/*    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
    public void Activate()
    {
        instance = Instantiate(particleEmitterPrefab, transform.position, transform.rotation);
    }
    public void Deactivate()
    {
        Destroy(instance);
    }
}
