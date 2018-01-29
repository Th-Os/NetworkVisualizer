using System;

namespace NetworkVisualizer.Objects
{
    /*

           data : {
        //multiple
        multiple: _Boolean,
        devices: _DeviceIds, (list of device ids)
        //last
        timebased: _Boolean, (last connection or call)
        source: _DeviceId,
        target: _DeviceId, (not required, when call)
        //specific
        specific: _Boolean,
        device: _DeviceId,
        type: _Type, (connection, call, device)
        id: _IdOfType
       }
     */
    [Serializable]
    public class DataRequest
    {

        public bool Multiple { get; set; }
        public long[] Ids { get; set; }
        public bool Timebased { get; set; }
        public Device Source { get; set; }
        public Device Target { get; set; }
        public bool Specific { get; set; }
        public long DeviceId { get; set; }
        public string Type { get; set; }
        public long Id { get; set; }


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

        //Multiple All
        public DataRequest(bool multiple, long[] ids)
        {
            this.Multiple = multiple;
            this.Ids = ids;
        }

        //Timebased Device
        public DataRequest(bool timebased, string type, long id)
        {
            this.Timebased = timebased;
            this.Type = type;
            this.Id = id;
        }

        //Timebased Connection/Call
        public DataRequest(bool timebased, Device source, Device target, string type, long id)
        {
            this.Timebased = timebased;
            this.Source = source;
            this.Target = target;
            this.Type = type;
            this.Id = id;
        }

        //Specific Connection/Call/Device
        public DataRequest(bool specific, long deviceId, string type, long id)
        {
            this.Specific = specific;
            this.DeviceId = deviceId;
            this.Type = type;
            this.Id = id;
        }


    }
}
