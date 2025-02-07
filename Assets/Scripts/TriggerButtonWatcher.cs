using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class TriggerButtonEvent : UnityEvent<bool> { }

public class TriggerButtonWatcher : MonoBehaviour
{
    public MenuButtonEvent menuButtonPress;
    public Console console;

    private bool lastButtonState = false;
    private bool appState = true;

    private List<InputDevice> devicesWithTriggerButton;


    private void InputDevices_deviceConnected(InputDevice device)
    {
        bool discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out discardedValue))
        {
            devicesWithTriggerButton.Add(device);
            console.AddLine("[" + this.name + "] add device " + device.name);
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithTriggerButton.Contains(device))
        {
            devicesWithTriggerButton.Remove(device);
            console.AddLine("[" + this.name + "] remove device " + device.name);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        devicesWithTriggerButton = new List<InputDevice>();
        RegisterDevices();
    }

    private void OnEnable()
    {
        RegisterDevices();
    }
    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
        devicesWithTriggerButton.Clear();
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

    private void Update()
    {
        bool tempState = false;
        foreach (var device in devicesWithTriggerButton)
        {
            bool primaryButtonState = false;
            tempState = device.TryGetFeatureValue(CommonUsages.triggerButton, out primaryButtonState)
                    && primaryButtonState
                    || tempState;
        }

        if (tempState != lastButtonState)
        {
            console.AddLine("call menuButtonPress with " + tempState);
            if (tempState)
            {
                appState = !appState;
                menuButtonPress.Invoke(appState);
            }
            lastButtonState = tempState;
        }
    }
}
