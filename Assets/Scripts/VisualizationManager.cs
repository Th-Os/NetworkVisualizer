using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkVisualizer.Objects;

public class VisualizationManager : MonoBehaviour {

    public Transform m_Trail;
    public Transform m_Connection;
    public Transform m_Call;
    public GameObject Devices;

	// Use this for initialization
	void Start () {
        //EventManager.AddHandler<System.Object>(EVNT.NewConnection, OnConnection);
        Events.OnNewConnection += OnConnection;
        Events.OnDataArrived += OnDataArrived;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnConnection(NetworkObject connection)
    {
        Debug.Log("New Connection: " + connection.GetType());

        if(connection is Connection)
        {
            AddConnection(connection as Connection);
        }

        if(connection is Call)
        {
            AddCall(connection as Call);
        }

    }

    public void OnDataArrived(Data data)
    {
        AddData(data);
    }

    private void AddConnection(Connection con)
    {
        Debug.Log("New Connection from " + con.start.name + " to " + con.target.name);
    }

    private void AddCall(Call call)
    {
        Debug.Log("New Call from " + call.start.name);
    }

    private void AddData(Data data)
    {
        if(data is DeviceData)
            Debug.Log("New Data for " + (data as DeviceData).device.name);

        if (data is ConnectionData)
            Debug.Log("New Data for a Connection");
    }

    private Transform GetDeviceByName(string name)
    {
        GameObject[] objects = Devices.GetComponentsInChildren<GameObject>();

        foreach(GameObject o in objects)
        {
            if(string.Equals(o.name, name, System.StringComparison.OrdinalIgnoreCase))
            {
                return o.transform;
            }
        }

        return null;
    }
}
