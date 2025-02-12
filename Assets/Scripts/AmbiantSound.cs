using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiantSound : MonoBehaviour
{
    public List<AudioSource> sources;
    public AudioSource explosion;
    // Start is called before the first frame update
    void Start()
    {
        muteAll();
    }

    public void muteAll()
    {
        foreach (var s in sources)
        {
            s.mute = true;
        }
    }

    public void unmuteAll()
    {
        foreach (var s in sources)
        {
            s.mute = false;
        }
    }

    public void playExplosion()
    {
        Debug.Log("playing explosion");
        explosion.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
