using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

using NetworkVisualizer.Objects;

//Responsible for Device Initialization and Protection (World Anchor)
public class ObjectManager : MonoBehaviour {

    public int DeviceCount = 4;
    public string[] Device_Names = new string[] { "Router", "First", "Second", "Third" };
    public GameObject Device;

    public Transform Devices;

    private int count;

	// Use this for initialization
	void Start () {
        Events.OnDeviceFound += OnNewDevice;
                                                            
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

    void OnNewDevice(Transform transform)
    {
        string name = Device_Names[count];
        Debug.Log("hello " + name);

        if(name.Equals("None"))
        {
            Debug.Log("Here is a None");
        }

        GameObject obj = Instantiate(Device, transform.position, transform.rotation, Devices);

        obj.name = name;
        //AddAnchor(obj, name);
        //obj.AddComponent<Content>();

        count++;
        if (count == DeviceCount)
        {
            Debug.Log("device count reached");
            GetComponent<ObjectsDefiner>().enabled = false;
            Events.Broadcast(Events.EVENTS.END_DEFINE);
        }
    }
}
