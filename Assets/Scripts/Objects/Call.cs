using System;

namespace NetworkVisualizer.Objects
{

    [Serializable]
    public class Call : NetworkObject
    {
        public Device start { get; }
        public string type { get; }
        public string time { get; }
        public string body { get; }

        public Call(Device start, string type, string time, string body)
        {
            this.start = start;
            this.type = type;
            this.time = time;
            this.body = body;
        }

    }
}
