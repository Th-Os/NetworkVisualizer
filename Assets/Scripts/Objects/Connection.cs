using System;

namespace NetworkVisualizer.Objects
{

    [Serializable]
    public class Connection : NetworkObject
    {
        public Device start { get; }
        public Device target { get; }
        public string type { get; }
        public string time { get; }
        public string body { get; }

        public Connection(Device start, Device target, string type, string time, string body)
        {
            this.start = start;
            this.target = target;
            this.type = type;
            this.time = time;
            this.body = body;
        }

    }
}