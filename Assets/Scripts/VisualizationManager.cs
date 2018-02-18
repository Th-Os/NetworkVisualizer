using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NetworkVisualizer.Objects;

public class VisualizationManager : MonoBehaviour {

    public GameObject Devices;

    public GameObject DevicePanel;
    public GameObject ConnectionPanel;

    private Vector3 _panelOffset =  new Vector3(0f, 4f, 0f);

    // Use this for initialization
    void Start () {
        Events.OnNewConnection += OnConnection;
        Events.OnShowDeviceData += ShowDeviceData;
        Events.OnShowConnectionData += ShowConnectionData;
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

    public void ShowDeviceData(GameObject obj, Device device)
    {
        Vector3 position = obj.transform.position + _panelOffset;
        obj.transform.position = position;
        GameObject devicePanel = Instantiate(DevicePanel, position, obj.transform.rotation, obj.transform);
        device.FillTexts(devicePanel.GetComponentsInChildren<Text>());
        Debug.Log("Display data " + device.Content.Value + " of device: " + device.Name + " with ip: " + device.Ip + " and position: " + device.Position);
    }

    public void ShowConnectionData(DeviceConnection dc, Connection conn)
    {
        Vector3 position = Vector3.Lerp(dc.Source.position, dc.Target.position + _panelOffset, 0.5f);
        GameObject connectionPanel = Instantiate(ConnectionPanel, position, dc.Source.rotation, dc.Source);
        conn.FillTexts(connectionPanel.GetComponentsInChildren<Text>());
        Debug.Log("Display data " + conn.Body + " of source: " + conn.Start.Name + " target: " + conn.Target.Name);
    }

    private void AddConnection(Connection con)
    {
        Debug.Log("New Connection from " + con.Start.Name + " to " + con.Target.Name);
        Events.Broadcast(Events.EVENTS.DRAW_CONNECTION, GetDeviceByName(con.Start.Name), GetDeviceByName(con.Target.Name));
    }

    private void AddCall(Call call)
    {
        Debug.Log("New Call from " + call.start.Name);
        Events.Broadcast(Events.EVENTS.DRAW_CALL, GetDeviceByName(call.start.Name));
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
