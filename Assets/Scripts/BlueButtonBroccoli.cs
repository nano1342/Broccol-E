using System.Collections;
using UnityEngine;

public class BlueButtonBroccoli : MonoBehaviour
{
    public ParticleSystem waterParticles;
    public ParticleSystem fireParticles;
    public int repeatCount = 3;
    public float delayBetweenRepeats = 0.2f;

    public MissionWaterBroccoli missionScript;

    public void OnMouseDown()
    {
        if (fireParticles != null && fireParticles.isPlaying)
        {
            fireParticles.Stop();
        }

        if (waterParticles != null)
        {
            StartCoroutine(PlayWaterEffect()); // Active water particles 3 times with a short delay between repeats
        }
    }

    public void OnTriggerPressed()
    {
        if (fireParticles != null && fireParticles.isPlaying)
        {
            fireParticles.Stop();
        }

        if (waterParticles != null)
        {
            StartCoroutine(PlayWaterEffect()); // Active water particles 3 times with a short delay between repeats
        }
        missionScript.completeMission();
    }

    IEnumerator PlayWaterEffect()
    {
        for (int i = 0; i < repeatCount; i++)
        {
            waterParticles.Play();
            yield return new WaitForSeconds(waterParticles.main.duration + delayBetweenRepeats);
        }
    }
}
