using System;
using UnityEngine.UI;

namespace NetworkVisualizer.Objects
{

    [Serializable]
    public class Call : NetworkObject
    {
        public int Id { get; set; }
        public Device start { get; }
        public string type { get; }
        public string time { get; }

        public Call(int id, Device start, string type, string time)
        {
            this.Id = id;
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
