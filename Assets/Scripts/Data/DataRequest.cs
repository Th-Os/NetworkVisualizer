using System;

namespace NetworkVisualizer.Objects
{
    /// <summary>
    /// This object represents the request for data to the server.
    /// </summary>
    [Serializable]
    public class DataRequest
    {
        /// <summary>
        /// This value describes, whether the server should send multiple data blocks.
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// If multiple data blocks are needed, 
        /// </summary>
        public long[] Ids { get; set; }

        /// <summary>
        /// Server returns newest data.
        /// </summary>
        public bool Timebased { get; set; }

        /// <summary>
        /// Device that data will be requested. If a connection is requested, this is the starting point.
        /// </summary>
        public Device Source { get; set; }

        /// <summary>
        /// The device representing the target point of a connection.
        /// </summary>
        public Device Target { get; set; }

        /// <summary>
        /// Server returns specific data.
        /// </summary>
        public bool Specific { get; set; }

        /// <summary>
        /// This is the specific DeviceID that will be used to get only the device data.
        /// </summary>
        public long DeviceId { get; set; }

        /// <summary>
        /// Defines the type of the data, e.g. device or connection.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// If a specific connection of a specific device is needed.
        /// </summary>
        public long Id { get; set; }

          /// <summary>
          /// Request all types of data.
          /// </summary>
          /// <param name="multiple"></param>
          /// <param name="ids"></param>
          /// <param name="timebased"></param>
          /// <param name="source"></param>
          /// <param name="target"></param>
          /// <param name="specific"></param>
          /// <param name="deviceId"></param>
          /// <param name="type"></param>
          /// <param name="id"></param>
        public DataRequest(bool multiple, long[] ids, bool timebased, Device source, Device target, bool specific, long deviceId, string type, long id)
        {
            this.Multiple = multiple;
            this.Ids = Ids;
            this.Timebased = timebased;
            this.Source = source;
            this.Target = target;
            this.Specific = specific;
            this.DeviceId = deviceId;
            this.Type = type;
            this.Id = id;
        }

        /// <summary>
        /// Request multiple data.
        /// </summary>
        public DataRequest(bool multiple, long[] ids)
        {
            this.Multiple = multiple;
            this.Ids = ids;
        }

        /// <summary>
        /// Request newest data of a device.
        /// </summary>
        /// <param name="timebased"></param>
        /// <param name="type"></param>
        /// <param name="device"></param>
        public DataRequest(bool timebased, string type, Device device)
        {
            this.Timebased = timebased;
            this.Type = type;
            this.Source = device;
        }

        /// <summary>
        /// Request newest data of a connection.
        /// </summary>
        /// <param name="timebased"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="type"></param>
        public DataRequest(bool timebased, Device source, Device target, string type)
        {
            this.Timebased = timebased;
            this.Source = source;
            this.Target = target;
            this.Type = type;
        }

        /// <summary>
        /// Request specific data.
        /// </summary>
        /// <param name="specific"></param>
        /// <param name="deviceId"></param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        public DataRequest(bool specific, long deviceId, string type, long id)
        {
            this.Specific = specific;
            this.DeviceId = deviceId;
            this.Type = type;
            this.Id = id;
        }


    }
}
