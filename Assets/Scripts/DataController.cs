using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkVisualizer.Objects;
using Helpers;
using System;

namespace NetworkVisualizer
{

    public class DataController : Singleton<DataController>
    {
        private bool _hasDataLocally;

        // Use this for initialization
        void Start()
        {
            Events.OnLocalDataRequested += OnDataRequested;
        }

        void OnDataRequested(Transform source, Transform target)
        {
            if (target == null)
                _hasDataLocally = LookUpConnection(source, target);
            else
                _hasDataLocally = LookUpDevice(source);

            if (_hasDataLocally)
                Events.Broadcast(Events.EVENTS.DATA_ARRIVED, CreateData());
            else
                Events.Broadcast(Events.EVENTS.REQUEST_DATA, CreateDataRequest(source, target));

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