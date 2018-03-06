using System;
using Newtonsoft.Json;

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
           Example:
           {
          "Connection": {
                    "body": "",
                    "id": 27455,
                    "start": {
                              "content": "\"\"",
                              "id": 51,
                              "ip": "10.0.0.11",
                              "name": "esp_1",
                              "position": {
                                        "id": 50,
                                        "x": 1.8716265,
                                        "y": 1.38976359,
                                        "z": 2.20299029
                              }
                    },
                    "target": {
                              "content": "\"\"",
                              "id": 49,
                              "ip": "10.0.0.1",
                              "name": "router",
                              "position": {
                                        "id": 48,
                                        "x": -0.231124252,
                                        "y": 0.773213863,
                                        "z": 2.274579
                              }
                    },
                    "time": "80303",
                    "type": "ANSWER"
          },
          "Timebased": true,
          "Type": "connection"
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

        [JsonConstructor]
        public Data(NetworkObject no, bool timebased, string type)
        {
            if (type == "connection")
                Connection = no as Connection;
            else
                Device = no as Device;

            Timebased = timebased;
            Type = type;
        }

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

