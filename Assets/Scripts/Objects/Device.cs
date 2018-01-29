using System;

namespace NetworkVisualizer.Objects
{

    [Serializable]
    public class Device
    {
        public string name { get; set;  }
        public Position position { get; set; }

        public Device(string name, Position position)
        {
            this.name = name;
            this.position = position;
        }

    }
}