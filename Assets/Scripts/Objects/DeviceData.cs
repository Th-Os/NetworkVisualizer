using System;

namespace NetworkVisualizer.Objects
{
    [Serializable]
    public class DeviceData : Data
    {
        public Device device { get; }

        public DeviceData(Device device)
        {
            this.device = device;
        }
    }
}