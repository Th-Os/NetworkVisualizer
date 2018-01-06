using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

//Responsible for Device Initialization and Protection (World Anchor)
public class ObjectManager : MonoBehaviour {

    public int DeviceCount = 3;

    private int count;

	// Use this for initialization
	void Start () {
        EventManager.AddHandler(EVNT.NewDevice, OnDeviceFound);
        
                                                            
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

    void OnDeviceFound(string name, Vector3 position)
    {
        Debug.Log("hello " + name);
        count++;
        if(count == DeviceCount)
        {
            Debug.Log("device count reached");
            GetComponent<ObjectsDefiner>().enabled = false;
            EventManager.Broadcast(EVNT.SwitchTestUI, "");
        }
    }
}
