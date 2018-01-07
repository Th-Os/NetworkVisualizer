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

            Events.Broadcast(Events.EVENTS.SWITCH_UI);
        }
    }
}
