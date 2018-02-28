using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using NetworkVisualizer;
using NetworkVisualizer.Enums;

//Responsible for Device Initialization and Protection (World Anchor)
public class ObjectManager : MonoBehaviour {

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

    void AddAnchor(GameObject obj, string name)
    {
        WorldAnchorManager.Instance.AttachAnchor(obj, name);
    }

    void RemoveAnchor(GameObject obj)
    {
        WorldAnchorManager.Instance.RemoveAnchor(obj);
    }

    void OnNewDevice(Vector3 position)
    {
        string name = Device_Names[count];
        Debug.Log("hello " + name);

        if(name.Equals("None"))
        {
            Debug.Log("Here is a None");
        }

        GameObject obj = Instantiate(Device, position, transform.rotation, Devices);

        obj.name = name;

        EventHandler.Broadcast(Events.DEVICE_DEFINED, obj.transform);

        Debug.Log("New Device: " + obj.name + " parent: " + obj.transform.parent.name); 
        //AddAnchor(obj, name);
        //obj.AddComponent<Content>();

        count++;
        if (count == _deviceCount)
        {
            Debug.Log("device count reached");
            GetComponent<ObjectsDefiner>().enabled = false;
            EventHandler.Broadcast(Events.END_DEFINE);
        }
    }
}
