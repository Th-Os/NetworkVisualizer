using System;

namespace NetworkVisualizer.Objects
{

    [Serializable]
    public class Device
    {
        public string Name { get; set;  }
        public string Ip { get; set; }
        public Position Position { get; set; }
        public Content Content { get; set; }

        public Device(string name, Position position)
        {
            this.Name = name;
            this.Position = position;
        }

        public Device(string name, String ip, Position position)
        {
            this.Name = name;
            this.Ip = ip;
            this.Position = position;

        }

        public Device(string name, String ip, Position position, Content content)
        {
            this.Name = name;
            this.Ip = ip;
            this.Position = position;
            this.Content = content;

        }
    }
}