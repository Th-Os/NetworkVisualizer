using System;
using System.Collections;
using UnityEngine;
using NetworkVisualizer.Objects;
using NetworkVisualizer.Enums;
using NetworkVisualizer.Data;


namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// The VisualizationManager is the layer between the system and the visual representations.
    /// It manages specific incoming events and distributes them to the responsible controller.
    /// Furthermore it requires 2 GameObjects that act as parents for the devices or panels.
    /// </summary>
    public class VisualizationManager : MonoBehaviour
    {

        public GameObject Devices;
        public GameObject Panels;

        // Use this for initialization
        void Start()
        {
            EventHandler.OnNewConnection += OnConnection;
            EventHandler.OnShowDeviceData += ShowDeviceData;
            EventHandler.OnShowConnectionData += ShowConnectionData;
            EventHandler.OnDestroyVisualization += DestroyConnections;
        }

        private void OnConnection(NetworkObject connection)
        {
            if (connection is Connection)
            {
                AddConnection(connection as Connection);
            }

            if (connection is Call)
            {
                AddCall(connection as Call);
            }

        }

        private void ShowDeviceData(GameObject obj, Device device)
        {
            Panels.GetComponent<PanelManager>().ShowPanel(obj, obj.transform.position, PanelType.Device, device);
            if(device != null)
                Debug.Log("Display data " + device.Content + " of device: " + device.Name + " with ip: " + device.Ip + " and position: " + device.Position);
        }

        private void ShowConnectionData(DeviceConnection dc, Connection conn)
        {
            Vector3 position = Vector3.Lerp(dc.Source.position, dc.Target.position, 0.5f);
            Panels.GetComponent<PanelManager>().ShowPanel(dc, position, PanelType.Connection, conn);
            if(conn != null)
                Debug.Log("Display data " + conn.Body + " of source: " + conn.Start.Name + " target: " + conn.Target.Name);
        }

        private void AddConnection(Connection con)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(
                GetDeviceByName(con.Start.Name, con.Target.Name, (values) => {
                if (values != null)
                {
                    EventHandler.Broadcast(Events.DRAW_CONNECTION, values[0], values[1]);
                }
            }));
        }

        private void AddCall(Call call)
        {
            Debug.Log("New Call from " + call.Start.Name + " to " + call.Target.Name);
            UnityMainThreadDispatcher.Instance().Enqueue(
                GetDeviceByName(call.Start.Name, call.Target.Name, (values) => {
                    if (values != null)
                    {
                        EventHandler.Broadcast(Events.DRAW_CALL, values[0], values[1], call.ToIp);
                    }
                }));
        }

        private IEnumerator GetDeviceByName(string sourceName, string targetName, Action<Transform[]> callback)
        {
            Transform source = null;
            Transform target = null;
            try
            {
                source = Devices.transform.Find(sourceName);
                if(targetName.Length > 0)
                    target = Devices.transform.Find(targetName);
            }catch(Exception exception)
            {
                Debug.Log(exception.StackTrace);
                Debug.Log(exception.Message);
                Debug.Log("Try another approach.");
                foreach(Transform t in Devices.transform)
                {
                    if (t.name.Equals(sourceName, StringComparison.OrdinalIgnoreCase))
                    {
                        source = t;
                    }
                    if (targetName.Length > 0 && t.name.Equals(targetName, StringComparison.OrdinalIgnoreCase))
                    {
                        target = t;
                    }
                }
            }
            yield return null;
            callback(new Transform[] { source, target });
        }

        private void DestroyConnections()
        {
            foreach(Transform device in Devices.transform)
            {
               foreach(Transform part in device.transform)
                {
                    if(part.name.Contains("(Clone)"))
                    {
                        Destroy(part.gameObject);
                    }
                }
            }

            foreach (Transform panel in Panels.transform)
            {
                if (panel.name.Contains("(Clone)"))
                    Destroy(panel.gameObject);
            }

            DataStore.Instance.RefreshConnectedDevices();
        }
    }
}