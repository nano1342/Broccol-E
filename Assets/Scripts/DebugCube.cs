using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[System.Serializable]
public class DebugAEvent : UnityEvent { }


public class DebugCube : MonoBehaviour
{
    public Console console;

    public DebugAEvent debugAEvent;

    public void OnMove()
    {
        Debug.Log("Movement Input");
    }

    public void OnDebugA()
    {
        Debug.Log("Input debug A");
        debugAEvent.Invoke();
    }
}
