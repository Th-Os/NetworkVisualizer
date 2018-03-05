using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NetworkVisualizer.Objects;
using NetworkVisualizer.Enums;
using System;

namespace NetworkVisualizer
{

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

        public void OnConnection(NetworkObject connection)
        {
            Debug.Log("New Connection: " + connection.GetType());

            if (connection is Connection)
            {
                AddConnection(connection as Connection);
            }

            if (connection is Call)
            {
                AddCall(connection as Call);
            }

        }

        public void ShowDeviceData(GameObject obj, Device device)
        {
            Panels.GetComponent<PanelManager>().ShowPanel(obj, obj.transform.position, PanelType.Device, device);
            if(device != null)
                Debug.Log("Display data " + device.Content + " of device: " + device.Name + " with ip: " + device.Ip + " and position: " + device.Position);
        }

        public void ShowConnectionData(DeviceConnection dc, Connection conn)
        {
            Vector3 position = Vector3.Lerp(dc.Source.position, dc.Target.position, 0.5f);
            Panels.GetComponent<PanelManager>().ShowPanel(dc, position, PanelType.Connection, conn);
            if(conn != null)
                Debug.Log("Display data " + conn.Body + " of source: " + conn.Start.Name + " target: " + conn.Target.Name);
        }

        private void AddConnection(Connection con)
        {
            Debug.Log("New Connection from " + con.Start.Name + " to " + con.Target.Name);

            StartCoroutine(GetDeviceByName(con.Start.Name, con.Target.Name, (values) => {
                if (values != null) {
                    EventHandler.Broadcast(Events.DRAW_CONNECTION, values[0], values[1]);
                }
            }));            
        }

        private void AddCall(Call call)
        {
            Debug.Log("New Call from " + call.start.Name);
            StartCoroutine(GetDeviceByName(call.start.Name, "", (values) => {
                if (values != null)
                {
                    EventHandler.Broadcast(Events.DRAW_CONNECTION, values[0]);
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