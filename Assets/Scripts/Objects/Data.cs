using System;
using UnityEngine;

namespace NetworkVisualizer.Objects
{
    [Serializable]
    public class Data
    {
        public String name;
        public Device device;
        public Connection connection;
        public Call call;

        public Data(String name, Device device, Connection connection, Call call)
        {
            this.name = name;
            this.device = device;
            this.connection = connection;
            this.call = call;
        }

        new public Type GetType() {
            Debug.Log("Device:" + (device != null) + " Connection: " + (connection != null) + " call: " + (call != null));
            if(device != null)
            {
                return device.GetType();
            }
            if(connection != null)
            {
                return connection.GetType();
            }
            if(call != null)
            {
                call.GetType();
            }

            return null;
        }
        

    }
}

