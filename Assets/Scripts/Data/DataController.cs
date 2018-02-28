using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkVisualizer.Objects;
using Helpers;
using System;
using NetworkVisualizer.Enums;

namespace NetworkVisualizer
{

    public class DataController : Singleton<DataController>
    {
        private bool _hasDataLocally;

        // Use this for initialization
        public void Init()
        {
            EventHandler.OnLocalDataRequested += OnDataRequested;
            EventHandler.OnDataArrived += OnDataArrived;
        }

        void OnDataRequested(Transform source, Transform target)
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

        void OnDataArrived(Data data)
        {
            if (data.Timebased) {
                if (data.Type.Equals("device"))
                {
                    Device device = data.Device;

                    EventHandler.Broadcast(Events.SHOW_DEVICE_DATA, DataStore.Instance.GetTransform(device).gameObject, device);
                    
                }
                if(data.Type.Equals("connection"))
                {
                    Connection conn = data.Connection;
                    Transform source = DataStore.Instance.GetTransform(conn.Start);
                    Transform target = DataStore.Instance.GetTransform(conn.Target);

                    DeviceConnection dc = LookUpDeviceConnection(source, target);
                    if (dc == null)
                        dc = LookUpDeviceConnection(target, source);

                    EventHandler.Broadcast(Events.SHOW_CONNECTION_DATA, dc, conn);
                }

            }
        }

        DeviceConnection LookUpDeviceConnection(Transform one, Transform two)
        {
            foreach (DeviceConnection dc in one.GetComponentsInChildren<DeviceConnection>())
            {
                if (dc.Target.name.Equals(two.name))
                {
                    return dc;
                }
            }
            return null;
        }


        //Not Implemented
        bool LookUpConnection(Transform source, Transform target)
        {
            return false;
        }

        //Not Implemented
        bool LookUpDevice(Transform device)
        {
            return false;
        }

        Data CreateData()
        {
            throw new NotImplementedException();
        }

        DataRequest CreateDataRequest(Transform source, Transform target)
        {
            bool timebased = true;
            string type = "";
            Device start = null;
            Device destination = null;
            if(target == null)
            {
                type = "device";
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
    }
}