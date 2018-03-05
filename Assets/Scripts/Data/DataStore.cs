using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using NetworkVisualizer.Objects;
using System;

namespace NetworkVisualizer
{
    public class DataStore : Singleton<DataStore>
    {

        private Dictionary<Transform, Device> _deviceMap;
        private Dictionary<int, Transform[]> _connectedDevices;

        public DataStore()
        {
            _deviceMap = new Dictionary<Transform, Device>();
            _connectedDevices = new Dictionary<int, Transform[]>();
        }

        public void AddDevice(Transform transform, Device device)
        {
            _deviceMap.Add(transform, device);
        }

        public Device GetDevice(Transform transform)
        {
            Device device = null;
            if(transform != null)
                _deviceMap.TryGetValue(transform, out device);
            return device;
        }

        /*
        public Transform GetTransform(Device device)
        {
            foreach(Transform transform in _deviceMap.Keys)
            {
                if(transform.name.Equals(device.Name))
                {
                    return transform;
                }
            }
            return null;
        }
         */
        public IEnumerator GetTransform(string source, string target, Action<Transform, Transform> callback)
        {
            Transform tSource = null;
            Transform tTarget = null;
            foreach (Transform t in _deviceMap.Keys)
            {
                if (t.name.Equals(source, StringComparison.OrdinalIgnoreCase))
                {
                    tSource = t;
                }
                if(t.name.Equals(target, StringComparison.OrdinalIgnoreCase))
                {
                    tTarget = t;
                }
            }
            yield return null;
            callback(tSource, tTarget);
        }

        public bool AddConnectedDevices(Transform source, Transform target)
        {
            if (GetConnectedDevices(source, target) == null)
            {
                _connectedDevices.Add(_connectedDevices.Count, new Transform[] { source, target });
                return true;
            }
            return false;
        }

        public DeviceConnection GetDeviceConnection(Transform source, Transform target)
        {
            Transform[] transforms = GetConnectedDevices(source, target);
            DeviceConnection dc = null;
            if (transforms != null)
            {
                foreach(Transform transform in transforms)
                {
                    dc = transform.gameObject.GetComponent<DeviceConnection>();
                    if(dc != null)
                    {
                        break;
                    }
                }
            }
            return dc;
        }

        public Transform[] GetConnectedDevices(Transform source, Transform target)
        {
            foreach(Transform[] array in _connectedDevices.Values)
            {
                if(array[0].name == source.name || array[0].name == target.name)
                {   
                    if(array[1].name == source.name || array[1].name == target.name)
                        return array;
                }
            }
            return null;
        }

        public void RefreshConnectedDevices()
        {
            _connectedDevices.Clear();
        }
    }
}