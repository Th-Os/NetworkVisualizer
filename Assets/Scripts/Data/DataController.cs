using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkVisualizer.Objects;
using Helpers;
using System;
using NetworkVisualizer.Enums;

namespace NetworkVisualizer.Data
{
    /// <summary>
    /// The DataController gets all requests to and from the server to verify/convert the data and adds missing components.
    /// </summary>
    public class DataController : Singleton<DataController>
    {
        private bool _hasDataLocally;

        /// <summary>
        /// Adds the Eventlisteners to the DataController.
        /// </summary>
        public void Init()
        {
            EventHandler.OnLocalDataRequested += OnDataRequested;
            EventHandler.OnDataArrived += OnDataArrived;
        }

        private void OnDataRequested(Transform source, Transform target)
        {
            if (target == null)
                _hasDataLocally = LookUpConnection(source, target);
            else
                _hasDataLocally = LookUpDevice(source);

            if (_hasDataLocally)
                EventHandler.Broadcast(Events.DATA_ARRIVED, CreateData());
            else
                EventHandler.Broadcast(Events.REQUEST_DATA, CreateDataRequest(source, target));

        }

        private void OnDataArrived(DataResponse data)
        {
            if (data.Timebased) {
                if (data.Type.Equals("device"))
                {
                    Device device = data.Device;
                    UnityMainThreadDispatcher.Instance().Enqueue(
                    DataStore.Instance.GetTransform(device.Name, "", (source, target) => {
                        if (source != null)
                        {
                            EventHandler.Broadcast(Events.SHOW_DEVICE_DATA, source.gameObject, device);
                        }
                    }));
                }
                if(data.Type.Equals("connection"))
                {
                    Connection conn = data.Connection;
                    UnityMainThreadDispatcher.Instance().Enqueue(
                    DataStore.Instance.GetTransform(conn.Start.Name, conn.Target.Name, (source, target) => {
                        if (source != null && target != null)
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(
                            LookUpDeviceConnection(source, target, (deviceConnection) => {
                                if (deviceConnection != null)
                                {
                                    EventHandler.Broadcast(Events.SHOW_CONNECTION_DATA, deviceConnection, conn);
                                }
                            }));
                        }
                    }));
                }

            }
        }

        private DeviceConnection LookUpDeviceConnection(Transform source, Transform target)
        {
            GameObject connectionContainer = GameObject.FindGameObjectWithTag("ConnectionContainer");
            foreach (DeviceConnection dc in connectionContainer.GetComponentsInChildren<DeviceConnection>())
            {
                if ((dc.Source.name.Equals(source.name, StringComparison.OrdinalIgnoreCase) && dc.Target.name.Equals(target.name, StringComparison.OrdinalIgnoreCase)) || (dc.Source.name.Equals(target.name, StringComparison.OrdinalIgnoreCase) && dc.Target.name.Equals(source.name, StringComparison.OrdinalIgnoreCase)))
                {
                    return dc;
                }

            }
            return null;
        }

        private IEnumerator LookUpDeviceConnection(Transform source, Transform target, Action<DeviceConnection> callback)
        {
            GameObject connectionContainer = GameObject.FindGameObjectWithTag("ConnectionContainer");
            DeviceConnection deviceConnection = null;
            foreach (DeviceConnection dc in connectionContainer.GetComponentsInChildren<DeviceConnection>())
            {
                if ((dc.Source.name.Equals(source.name, StringComparison.OrdinalIgnoreCase) && dc.Target.name.Equals(target.name, StringComparison.OrdinalIgnoreCase)) || (dc.Source.name.Equals(target.name, StringComparison.OrdinalIgnoreCase) && dc.Target.name.Equals(source.name, StringComparison.OrdinalIgnoreCase)))
                {
                    deviceConnection = dc; 
                }

            }
            yield return null;
            callback(deviceConnection);
        }


        private DataRequest CreateDataRequest(Transform source, Transform target)
        {
            bool timebased = true;
            string type = "";
            Device start = null;
            Device destination = null;
            if(target == null)
            {
                type = "device";
                Debug.Log(" datarequest device with: " + source.name);
                start = DataStore.Instance.GetDevice(source);
                return new DataRequest(timebased, type, start);
            }
            else
            {
                type = "connection";
                start = DataStore.Instance.GetDevice(source);
                destination = DataStore.Instance.GetDevice(target);
                return new DataRequest(timebased, start, destination, type);
            }   
        }

        #region NotImplementedYet

        bool LookUpConnection(Transform source, Transform target)
        {
            return false;
        }

        bool LookUpDevice(Transform device)
        {
            return false;
        }

        DataResponse CreateData()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}