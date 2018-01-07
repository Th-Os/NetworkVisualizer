using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NetworkVisualizer.Objects;

public class TestController : MonoBehaviour {

    public GameObject m_Devices;
    public GameObject m_Device;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //not working for hololens ...
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("key pressed");


            GameObject obj = Instantiate(m_Device, m_Devices.transform);
            obj.transform.position = new Vector3(1, 2, 3);

            Events.Broadcast(Events.EVENTS.DEVICE_FOUND, obj);
            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, new Call(new Device("Router", new Position(1, 2, 3)), "call", "time", "body"));
            //EventManager.Broadcast(EVNT.NewConnection, "hello");
        }
    }
}
