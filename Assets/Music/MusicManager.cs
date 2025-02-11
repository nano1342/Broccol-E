using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * Stores a checkpoint for the math step function, together with the volume.
 * values between checkpoints are interpolated
 */
class MathStepCheckpoint : IComparable<MathStepCheckpoint>
{
    public long checkpointSeconds { get; }
    public float volume { get; }
    public MathStepCheckpoint(long checkpointSeconds, float volume)
    {
        this.checkpointSeconds = checkpointSeconds;
        this.volume = volume;
    }

    public int CompareTo(MathStepCheckpoint other)
    {
        if (null == other)
            return 1;
        return this.checkpointSeconds.CompareTo(other.checkpointSeconds);
    }
}

/**
 * Apply with easing the volume based on a given math step config, at every
 * call of the tick() function, for a given audio source.
 */
class VolumeManager
{
    private AudioSource src;
    private List<MathStepCheckpoint> checkpoints;
    /**
     * Between 0 and 1
     */
    private float targetVolume;
    private const float LINEAR_ADJUSTMENT_STEP = 0.0005f;
    public VolumeManager(AudioSource src, List<MathStepCheckpoint> checkpoints)
    {
        this.src = src;
        this.checkpoints = checkpoints;
        src.volume = 0;
        this.checkpoints.Sort();
    }

    /**
     * Not influenced by times but by each call, not proof to lag
     */
    public void tick(double timeLeftMs)
    {
        float timeLeftSeconds = (float) timeLeftMs / 1_000;
        targetVolume = mathStep(timeLeftSeconds, checkpoints);
        if (Math.Abs(src.volume - targetVolume) < LINEAR_ADJUSTMENT_STEP)
        {
            src.volume = targetVolume;
        } else if (src.volume - targetVolume < 0)
        {
            src.volume += LINEAR_ADJUSTMENT_STEP;
        } else
        {
            src.volume -= LINEAR_ADJUSTMENT_STEP;
        }
    }

    private static float mathStep(float t, List<MathStepCheckpoint> checkpoints)
    {
        MathStepCheckpoint before = null;
        MathStepCheckpoint after = null;

        // find the range in which the current value is
        foreach (var c in checkpoints)
        {
            if (c.checkpointSeconds < t)
            {
                if (before == null || before.checkpointSeconds < c.checkpointSeconds)
                {
                    before = c;
                }
            }
            if (c.checkpointSeconds > t)
            {
                if (after == null || after.checkpointSeconds > c.checkpointSeconds)
                {
                    after = c;
                }
            }
        }

        // compute value based on boundaries.
        if (before == null)
        {
            return after.volume;
        }
        else if (after == null)
        {
            return before.volume;
        }
        else
        {
            long checkpointRange = before.checkpointSeconds - after.checkpointSeconds;
            float interpol = (t - before.checkpointSeconds) / checkpointRange;
            if (interpol < 0)
            {
                interpol = -interpol;
            }
            return Mathf.Lerp(before.volume, after.volume, interpol);

        }
    }

    override public string ToString()
    {
        return "VolumeManager{" + src.name + ", volume: " + src.volume + ", targetVolume: " + targetVolume + "}";
    }
}

public class MusicManager : MonoBehaviour
{
    public EventDriver eventDriver;
    public AudioSource trumpet;
    public AudioSource clockFast;
    public AudioSource clock;
    public AudioSource farAwayPad;
    public AudioSource frostPad;
    public AudioSource gettingAggressive;
    public AudioSource maracas;
    public AudioSource mutedPad;
    public AudioSource panickStrings;
    public AudioSource piano;
    public AudioSource strings;
    public AudioSource strongLead;
    public AudioSource timpani;
    private List<VolumeManager> volumeManagers = null;
    private const long LOOP_DURATION_SECONDS = 24; // audio file loop duration is 32, decrease for changes in a smaller time frame.

    /**
     * Shorthand checkpoint constructor
     */
    private MathStepCheckpoint cp(long t, float v)
    {
        return new MathStepCheckpoint(t, v);
    }
    /**
     * return a duration in seconds based on the number of loops given
     */
    private long durationFromLoops(int nb)
    {
        return nb * LOOP_DURATION_SECONDS;
    }
    /**
     * create a step in the form of a table (/--\) between the two number of loops given
     */
    private List<MathStepCheckpoint> maxVolumeWithin(int loopNb1, int loopNb2, bool invert = false)
    {
        if (loopNb1 >= loopNb2)
        {
            throw new ArgumentException("maxVolumeWithin: nb1 < nb2 required");
        }
        float vMin = invert ? 1 : 0;
        float vMax = invert ? 0 : 1;
        return new List<MathStepCheckpoint> {
            cp(durationFromLoops(loopNb1)-1, vMin),
            cp(durationFromLoops(loopNb1), vMax),
            cp(durationFromLoops(loopNb2), vMax),
            cp(durationFromLoops(loopNb2)+1, vMin),
        };
    }
    // Start is called before the first frame update
    void Start()
    {
        List<MathStepCheckpoint> stringsCheckpoints = new List<MathStepCheckpoint>();
        stringsCheckpoints.AddRange(maxVolumeWithin(0,1));
        stringsCheckpoints.AddRange(maxVolumeWithin(2,7));
        volumeManagers = new List<VolumeManager>
        {
            new VolumeManager(trumpet, maxVolumeWithin(1,2)),
            new VolumeManager(clockFast, maxVolumeWithin(0,1)),
            new VolumeManager(clock, new List<MathStepCheckpoint> {
                cp(durationFromLoops(1)-1, 0),
                cp(durationFromLoops(1), 1)
            }),
            new VolumeManager(farAwayPad, new List<MathStepCheckpoint> {
                cp(durationFromLoops(4), 1),
                cp(durationFromLoops(4)+1, 0)
            }),
            new VolumeManager(frostPad, maxVolumeWithin(0,4)),
            new VolumeManager(gettingAggressive, maxVolumeWithin(0,1)),
            new VolumeManager(maracas, maxVolumeWithin(0,6)),
            new VolumeManager(mutedPad, maxVolumeWithin(3,6)),
            new VolumeManager(panickStrings, maxVolumeWithin(0,1)),
            new VolumeManager(piano, maxVolumeWithin(0,1,true)),
            new VolumeManager(strings, stringsCheckpoints),
            new VolumeManager(strongLead, maxVolumeWithin(1,3)),
            new VolumeManager(timpani, maxVolumeWithin(0,5)),
        };

        piano.volume = 1;
        clock.volume = 1;
    }

    long i = 0;
    // Update is called once per frame
    void Update()
    {
        if (volumeManagers == null) return;
        if (eventDriver == null) return;

        double t = eventDriver.getGlobalTimeLeftMs();
        foreach (var v in volumeManagers)
        {
            v.tick(t);
            if (i % 10_000 == 0) Debug.Log(v.ToString());
        }
        i++;
    }
}
