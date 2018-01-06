using System;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class DeviceManager : MonoBehaviour {

    
    public Transform m_Device;
    public Transform m_Trail;
    public Transform m_Connection;
    public Transform m_Call;

    private Transform m_Devices;

    private Dictionary<string, Transform> devices;

    private Transform[] array;

    // Use this for initialization
    void Start () {
        devices = new Dictionary<string, Transform>();
        EventManager.AddHandler<string>(EVNT.NewConnection, TestConnection);
        EventManager.AddHandler(EVNT.UpdateDevice, UpdateDevice);
        m_Devices = gameObject.transform;
        EventManager.AddHandler<string>(EVNT.StartTest, TestConnection);
    }

    public void TestConnection<T>(T value)
    {
        if (value is string)
        {
            Transform device = Instantiate(m_Device);
            device.position = new Vector3(-5, 1, 10);
            Transform target = Instantiate(m_Device);
            target.position = new Vector3(5, 1, 10);
            Transform trail = Instantiate(m_Trail);
            trail.parent = device;

            ConnectionMove move = trail.GetComponent<ConnectionMove>();
            move.m_Origin = device.position;
            move.m_Target = target.position;
            move.m_Speed = 1f;
            move.Move();

            Transform connection = Instantiate(m_Connection);
            LineRenderer renderer = connection.GetComponent<LineRenderer>();
            renderer.positionCount = 2;
            renderer.SetPositions(new Vector3[] { device.position, target.position });


            Transform call = Instantiate(m_Call);
            call.position = new Vector3(0f, -5f, 50f);
        }
    }
	
	// Update is called once per frame
	void Update () {	
	}

    /// <summary>
    /// Initialises a new device or updates it with a new position.
    /// And sends the data via mqtt to the Publisher.
    /// </summary>
    /// <param name="name">Name of a Device</param>
    /// <param name="position">Position of a device</param>
    void UpdateDevice(string name, Vector3 position)
    {
        Transform device;
        if (!devices.ContainsKey(name))
            device = Instantiate(m_Device);
        else
            devices.TryGetValue(name, out device);

        if(device)
        {
            device.parent = m_Devices;
            device.position = position;
            devices.Add(name, device);

            MqttController.SendDeviceData(name, position);
        }
    }

    /// <summary>
    /// Adds a connection between a sending and receiving device. 
    /// </summary>
    /// <param name="start">Name of the sending device</param>
    /// <param name="target">Name of the receiving device</param>
    void AddConnection(string start, string target)
    {
        Transform d_Start;
        Transform d_Target;
        devices.TryGetValue(name, out d_Start);
        devices.TryGetValue(name, out d_Target);
        if (d_Start != null && d_Target != null)
        {
            Transform connection = Instantiate(m_Connection);
            connection.parent = d_Start;

            /*
            LineRenderer renderer = connection.GetComponent<LineRenderer>();
            renderer.positionCount = 2;
            renderer.SetPositions(new Vector3[] { d_Start.position, d_Target.position });
            */
            ConnectionMove move = connection.GetComponent<ConnectionMove>();
            move.m_Origin = d_Start.position;
            move.m_Target = d_Target.position;
            move.m_Speed = 0.3f;
            move.Move();
        }
    }

    void AddCall(string name)
    {
        Transform device;
        devices.TryGetValue(name, out device);

        if(device)
        {
            Transform call = Instantiate(m_Call);
            call.parent = device;
        }
    }

}
