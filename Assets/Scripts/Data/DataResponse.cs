using System;
using Newtonsoft.Json;

namespace NetworkVisualizer.Objects
{
    /// <summary>
    /// This object is used for the JSON serialization for the data message, that is sent by the server.
    /// </summary>
    [Serializable]
    public class DataResponse
    {
        /// <summary>
        /// This value describes, whether the message contains multiple data blocks.
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// If multiple data blocks are sent, they will be saved as a Device array.
        /// </summary>
        public Device[] Devices { get; set; }

        /// <summary>
        /// Is true, when the server sent the newest available data set of an device or connection.
        /// </summary>
        public bool Timebased { get; set; }

        /// <summary>
        /// Is true, when the server sent a specific device or connection.
        /// </summary>
        public bool Specific { get; set; }

        /// <summary>
        /// This is the type of the timebased or specific data.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// A Connection, if type is connection.
        /// </summary>
        public Connection Connection { get; set; }

        /// <summary>
        /// A Call, if type is call.
        /// </summary>
        public Call Call { get; set; }

        /// <summary>
        /// A Device, if type is device.
        /// </summary>
        public Device Device { get; set; }

        [JsonConstructor]
        public DataResponse(NetworkObject no, bool timebased, string type)
        {
            if (type == "connection")
                Connection = no as Connection;
            else
                Device = no as Device;

            Timebased = timebased;
            Type = type;
        }

        public DataResponse(bool multiple, Device[] devices, bool timebased, bool specific, string type, Connection connection, Call call, Device device)
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
                return Connection as Connection;
            }
            if(Type.Equals("call"))
            {
                return Call as Call;
            }
            if (Type.Equals("device"))
                return Device as Device;

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

