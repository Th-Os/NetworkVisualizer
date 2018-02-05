using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkVisualizer.Objects;

public class VisualizationManager : MonoBehaviour {

    public Transform Connection;
    public Transform Call;
    public GameObject Devices;

	// Use this for initialization
	void Start () {
        Events.OnNewConnection += OnConnection;
        Events.OnDataArrived += OnDataArrived;
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
        Events.Broadcast(Events.EVENTS.DRAW_CONNECTION, GetDeviceByName(con.start.name), GetDeviceByName(con.target.name));
    }

    private void AddCall(Call call)
    {
        Debug.Log("New Call from " + call.start.name);
        Events.Broadcast(Events.EVENTS.DRAW_CALL, GetDeviceByName(call.start.name));
    }

    private void AddData(Data data)
    {
        Debug.Log("New Data for " + data.Type + " type: " + data.GetType());
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
