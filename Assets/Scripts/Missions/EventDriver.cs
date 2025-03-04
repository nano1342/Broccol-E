using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MissionStartedEvent : UnityEvent { } // no arguments because they cannot be transferred through the GUI
[System.Serializable]
public class MissionCompletedEvent : UnityEvent { } // no arguments because they cannot be transferred through the GUI
[System.Serializable]
public class LostEvent : UnityEvent { }
[System.Serializable]
public class NewSessionStartedEvent: UnityEvent { }

public class MissionEvent
{
    private static System.Random random = new System.Random();

    public enum MissionKind
    {
        CHANGE_SOLAR_PANNEL,
        CHANGE_ANTENNA,
        REPAIR_CABLES,
        FIX_PIPES,
        RECHARGE_BATTERY,
        WATER_BROCCOLI,
        DESTROY_METEORITE,
        CHANGE_THERMAL_SHIELD,
        ANSWER_TO_PC,
        ENTER_DIGICODE,
    }
    private static Dictionary<MissionKind, int> eventDurations = new Dictionary<MissionKind, int>
    {
        {MissionKind.CHANGE_ANTENNA, 180 },
        {MissionKind.CHANGE_SOLAR_PANNEL, 180 },
        {MissionKind.REPAIR_CABLES, 120 },
        {MissionKind.FIX_PIPES, 120 },
        {MissionKind.RECHARGE_BATTERY, 180 },
        {MissionKind.WATER_BROCCOLI, 180 },
        {MissionKind.DESTROY_METEORITE, 120 },
        {MissionKind.CHANGE_THERMAL_SHIELD, 180 },
        {MissionKind.ANSWER_TO_PC, 60 },
        {MissionKind.ENTER_DIGICODE, 120 },
    };
    private static List<MissionKind> availableMissions = new List<MissionKind>(System.Enum.GetValues(typeof(MissionKind)).Cast<MissionKind>());
    private static List<MissionKind> disabledMissions = new List<MissionKind>
    {
        MissionKind.CHANGE_THERMAL_SHIELD,
        MissionKind.CHANGE_ANTENNA,
        MissionKind.CHANGE_SOLAR_PANNEL,
        MissionKind.REPAIR_CABLES,
        MissionKind.RECHARGE_BATTERY,
    };
    // il faut corriger le son et le debug
    public static void init()
    {
        foreach (var e in disabledMissions)
        {
            availableMissions.Remove(e);
        }
    }

    public MissionKind missionKind { get; private set; }
    private System.DateTime start;
    private System.DateTime end;

    public MissionEvent()
    {
        if (availableMissions.Count == 0)
        {
            throw new System.Exception("No new mission available.");
        }
        missionKind = getRandomAvailableMission();
        start = System.DateTime.Now;
        int duration;
        eventDurations.TryGetValue(missionKind, out duration);
        end = start.AddSeconds(duration);
    }
    public static bool eventAvailable()
    {
        return availableMissions.Count > 0;
    } 
    private MissionKind getRandomAvailableMission()
    {
        var index = random.Next(availableMissions.Count);
        var kind = availableMissions[index];
        availableMissions.RemoveAt(index);
        return kind;
    }
    public double getTimeLeftMs()
    {
        long elapsedTicks = end.Ticks - System.DateTime.Now.Ticks;
        return new System.TimeSpan(elapsedTicks).TotalMilliseconds;
    }

    public void destroy()
    {
        availableMissions.Add(missionKind);
    }

    public override string ToString()
    {
        string str = "{";
        str += "kind: " + missionKind + "; ";
        str += "timeLeft (s): " + (getTimeLeftMs() / 1_000) + "; ";
        str += "}; ";
        return str;
    }
}

public class EventDriver : MonoBehaviour
{
    private System.DateTime sessionStart;
    private List<MissionEvent> events;
    private static System.Random random = new System.Random();
    private bool lost;
    private bool running = false;
    private long ticks = 0;
    private System.DateTime nextEventTimeStamp;
    private static int NEXT_EVENT_MIN_SECONDS = 10;
    private static int NEXT_EVENT_MAX_SECONDS = 30;

    public Console console;
    public MissionStartedEvent missionStartedEvent;
    public MissionCompletedEvent missionCompletedEvent;
    public LostEvent lostEvent;
    public NewSessionStartedEvent newSessionStartedEvent;

    // Start is called before the first frame update
    void Start()
    {
        //startNewEventSession();
        events = new List<MissionEvent>();
        MissionEvent.init();
    }

    // Update is called once per frame
    void Update()
    {
        trySpawningEvent();
        tickEvents();
        log();
        ticks++;
    }

    public bool isRunning()
    {
        return running;
    }
    private System.TimeSpan getElapsedSpan()
    {
        long elapsedTicks = System.DateTime.Now.Ticks - sessionStart.Ticks;
        return new System.TimeSpan(elapsedTicks);
    }
    private System.TimeSpan getTimeBeforeNextEventSpan()
    {
        long elapsedTicks = nextEventTimeStamp.Ticks - System.DateTime.Now.Ticks;
        return new System.TimeSpan(elapsedTicks);

    }

    override public string ToString()
    {
        string str = "";
        str += "lost: " + lost + "; ";
        str += "elapsed (s): " + getElapsedSpan().TotalSeconds + "; ";
        str += "ongoing missions: [";
        foreach (var e in events)
        {
            str += e.ToString();
        }
        str += "]; ";
        str += "next mission event: " + getTimeBeforeNextEventSpan().TotalSeconds + ";";
        return str;
    }

    private void log()
    {
        if (ticks % 600 == 0)
        {
            Debug.Log(this);
        }
    }

    public void startNewEventSession()
    {
        console.AddLine("######## startNewEventSession ########");
        this.sessionStart = System.DateTime.Now;
        nextEventTimeStamp = System.DateTime.Now.AddSeconds(10);
        events = new List<MissionEvent>();
        lost = false;
        running = true;
        newSessionStartedEvent.Invoke();
    }

    private void trySpawningEvent()
    {
        if (nextEventTimeStamp < System.DateTime.Now && running)
        {
            console.AddLine("trySpawningEvent");
            if (MissionEvent.eventAvailable())
            {
                var e = new MissionEvent();
                events.Add(e);
                console.AddLine("Instanciated mission event: " + e.missionKind);
                missionStartedEvent.Invoke();
            } else
            {
                console.AddLine("All events are instanciated. No new event created.");
            }

            // define when the next event will occur
            int min = NEXT_EVENT_MIN_SECONDS;
            int max = NEXT_EVENT_MAX_SECONDS;
            int computedMin = Mathf.Max(min, (int)(max - getElapsedSpan().TotalSeconds / 4));
            int delay = random.Next(computedMin, max);
            nextEventTimeStamp = System.DateTime.Now.AddSeconds(delay);
        }
    }

    public MissionEvent getEventByKind(MissionEvent.MissionKind kind)
    {
        foreach (var e in events)
        {
            if (e.missionKind.Equals(kind))
            {
                return e;
            }
        }
        return null;
    }

    private void tickEvents()
    {
        foreach (var e in events)
        {
            var leftMs = e.getTimeLeftMs();
            if (leftMs < 0 && !lost)
            {
                Debug.Log("oh no! Anyways...");
                lost = true;
                lostEvent.Invoke();
            }
        }
    }

    public bool isEventActive(MissionEvent.MissionKind kind)
    {
        return getEventByKind(kind) != null;
    }

    public List<MissionEvent> getEventsSnapshot()
    {
        return new List<MissionEvent>(events);
    }

    public void completeEvent(MissionEvent.MissionKind kind)
    {
        bool found = false;
        foreach (var e in events)
        {
            if (e.missionKind.Equals(kind))
            {
                e.destroy();
                events.Remove(e); // ok to remove because we break the loop right after.
                found = true;
                missionCompletedEvent.Invoke();
                break;
            }
        }
        if (!found)
        {
            throw new System.Exception("MissionEvent is not running.");
        }
    }

    public double getGlobalTimeLeftMs()
    {
        double min = long.MaxValue;
        foreach (var e in events)
        {
            double localMin = e.getTimeLeftMs();
            if (localMin < min)
            {
                min = localMin;
            }
        }
        return min;
    }
}
