using System;

namespace NetworkVisualizer.Objects
/*
data : {
            multiple: _Boolean,
            devices: _DeviceArray (devices with specific connection types),
            timebased: _Boolean,
            specific: _Boolean
            type: _Type,
            connection: _Connection, (as JSONObject)
            call: _Call, (as JSONObject)
            device: _Device (as JSONObject)
           }
       */
{
    [Serializable]
    public class Data
    {
        public bool Multiple { get; set; }
        public Device[] Devices { get; set; }
        public bool Timebased { get; set; }
        public bool Specific { get; set; }
        public string Type { get; set; }
        public Connection Connection { get; set; }
        public Call Call { get; set; }
        public Device Device { get; set; }

        public Data(bool multiple, Device[] devices, bool timebased, bool specific, string type, Connection connection, Call call, Device device)
        {
            this.Multiple = multiple;
            this.Devices = devices;
            this.Timebased = timebased;
            this.Specific = specific;
            this.Type = type;
            this.Connection = connection;
            this.Call = call;
            this.Device = device;
        }

        public object GetRequest()
        {
            if(Type.Equals("connection"))
            {
                return Connection;
            }
            if(Type.Equals("call"))
            {
                return Call;
            }
            if (Type.Equals("device"))
                return Device;

            return null;
        }

        public Device[] GetDevices()
        {
            if(Multiple)
            {
                return Devices;
            }

            return new Device[] { };
        }

        new public string GetType()
        {
            if (Multiple)
                return "multiple";
            if (Timebased)
                return "timebased";
            if (Specific)
                return "specific";
            return "undefined";
        }
    }
}

