using System;

namespace NetworkVisualizer.Objects
{
    //TODO: Extend Data Object

    [Serializable]
    public class Data : NetworkObject
    {

        public Device device { get; }

        public Data(Device device)
        {
            this.device = device;
        }

    }
}

