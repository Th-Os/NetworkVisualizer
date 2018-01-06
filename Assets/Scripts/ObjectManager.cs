using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

//Responsible for Device Initialization and Protection (World Anchor)
public class ObjectManager : MonoBehaviour {

    public int DeviceCount = 3;
    public string[] Device_Names = new string[] { "Router", "First", "Second", "Third" };

    private int count;

	// Use this for initialization
	void Start () {
        //EventManager.AddHandler<Transform>(EVNT.NewDevice, OnDeviceFound);
        Events.OnDeviceFound += OnNewDevice;
                                                            
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void AddAnchor(GameObject obj, string name)
    {
        WorldAnchorManager.Instance.AttachAnchor(obj, name);
    }

    void RemoveAnchor(GameObject obj)
    {
        WorldAnchorManager.Instance.RemoveAnchor(obj);
    }

    void OnNewDevice(GameObject obj)
    {
        string name = Device_Names[count];
        Debug.Log("hello " + name);

        obj.name = name;
        AddAnchor(obj, name);

        count++;
        if (count == DeviceCount)
        {
            Debug.Log("device count reached");
            GetComponent<ObjectsDefiner>().enabled = false;
            EventManager.Broadcast(EVNT.SwitchTestUI, "");
        }
    }
}
