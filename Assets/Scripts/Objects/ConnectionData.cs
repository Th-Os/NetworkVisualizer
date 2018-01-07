using System;

namespace NetworkVisualizer.Objects
{
    [Serializable]
    public class ConnectionData : Data
    {
        public Device device { get; }

        public ConnectionData(Device device)
        {
            this.device = device;
        }
    }
}
