using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class MenuButtonEvent : UnityEvent<bool> { }

public class MenuButtonWatcher : MonoBehaviour
{
    public MenuButtonEvent menuButtonPress;
    public StartGame gameScript;
    public Console console;

    private bool lastButtonState = false;
    private bool appState = true;

    private List<InputDevice> devicesWithMenuButton;

    private void InputDevices_deviceConnected(InputDevice device)
    {
        bool discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.menuButton, out discardedValue))
        {
            devicesWithMenuButton.Add(device);
            console.AddLine("[" + this.name + "] add device " + device.name);
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithMenuButton.Contains(device))
        {
            devicesWithMenuButton.Remove(device);
            console.AddLine("[" + this.name + "] remove device " + device.name);
        }
    }

    private void Start()
    {
        devicesWithMenuButton = new List<InputDevice>();

        RegisterDevices();
    }

    private void OnEnable()
    {
        RegisterDevices();
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        devicesWithMenuButton.Clear();
    }

    private void RegisterDevices()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
        {
            InputDevices_deviceConnected(device);
        }

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;


    }

    void Update()
    {
        if (!gameScript.debug) return;

        bool tempState = false;
        foreach (var device in devicesWithMenuButton)
        {
            bool primaryButtonState = false;
            tempState = device.TryGetFeatureValue(CommonUsages.menuButton, out primaryButtonState)
                && primaryButtonState
                || tempState;
        }

        if (tempState != lastButtonState)
        {
            //console.AddLine("call menuButtonPress with " + tempState);
            if (tempState)
            {
                appState = !appState;
                menuButtonPress.Invoke(appState);
            }
            lastButtonState = tempState;
        }
    }

}
