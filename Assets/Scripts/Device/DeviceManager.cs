using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using NetworkVisualizer;
using NetworkVisualizer.Enums;

/// <summary>
/// The DeviceManager initializes an Device with a position given by the <see cref="DeviceDefiner"/>.
/// Furthermore it ends the device defining process, when all devices are assigned.
/// </summary>
public class DeviceManager : MonoBehaviour {

    public string[] Device_Names = new string[] { "router", "esp_1", "esp_2" };
    public GameObject Device;

    public Transform Devices;

    private int _deviceCount;
    private int count;

	// Use this for initialization
	void Start () {
        EventHandler.OnDeviceFound += OnNewDevice;
        _deviceCount = Device_Names.Length;                                                    
        count = 0;
	}

    private void OnNewDevice(Vector3 position)
    {
        string name = Device_Names[count];
        GameObject obj = Instantiate(Device, position, transform.rotation, Devices);

        obj.name = name;

        EventHandler.Broadcast(Events.DEVICE_DEFINED, obj.transform);

        Debug.Log("New Device: " + obj.name + " parent: " + obj.transform.parent.name); 

        count++;
        if (count == _deviceCount)
        {
            GetComponent<DeviceDefiner>().enabled = false;
            EventHandler.Broadcast(Events.END_DEFINE);
        }
    }
}
