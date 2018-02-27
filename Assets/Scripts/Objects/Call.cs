using System;
using UnityEngine.UI;

namespace NetworkVisualizer.Objects
{

    [Serializable]
    public class Call : NetworkObject
    {
        public Device start { get; }
        public string type { get; }
        public string time { get; }

        public Call(Device start, string type, string time)
        {
            this.start = start;
            this.type = type;
            this.time = time;
        }

        public override void FillTexts(Text[] texts)
        {
            throw new NotImplementedException();
        }

    }
}
