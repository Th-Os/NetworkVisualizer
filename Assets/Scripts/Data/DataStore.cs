using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using NetworkVisualizer.Objects;
using NetworkVisualizer.Visual;


namespace NetworkVisualizer.Data
{
    /// <summary>
    /// The DataStore functions as a storage for the relations between devices and transforms and between the connected transforms.
    /// </summary>
    public class DataStore : Singleton<DataStore>
    {

        private Dictionary<Transform, Device> _deviceMap;
        private Dictionary<int, Transform[]> _connectedDevices;

        public DataStore()
        {
            _deviceMap = new Dictionary<Transform, Device>();
            _connectedDevices = new Dictionary<int, Transform[]>();
        }

        /// <summary>
        /// Adds a device with its device representation to the relation map.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="device"></param>
        public void AddDevice(Transform transform, Device device)
        {
            _deviceMap.Add(transform, device);
        }

        /// <summary>
        /// Adds two devices to the map, which contains all connected devices.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool AddConnectedDevices(Transform source, Transform target)
        {
            if (GetConnectedDevices(source, target) == null)
            {
                _connectedDevices.Add(_connectedDevices.Count, new Transform[] { source, target });
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the device representation of a transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public Device GetDevice(Transform transform)
        {
            Device device = null;
            if(transform != null)
                _deviceMap.TryGetValue(transform, out device);
            return device;
        }

        /// <summary>
        /// Starts an asynchronous function that finds transforms with the name of a device or devices.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the DeviceConnection of two transforms.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns an array, if the two params are connected.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Clears the connected devices map.
        /// </summary>
        public void RefreshConnectedDevices()
        {
            _connectedDevices.Clear();
        }
    }
}