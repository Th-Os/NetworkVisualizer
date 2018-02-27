using System;
using UnityEngine.UI;

namespace NetworkVisualizer.Objects
{
    //TODO evtl Body object benutzen.
    [Serializable]
    public class Connection : NetworkObject
    {
        public Device Start { get; }
        public Device Target { get; }
        public string Type { get; }
        public string Time { get; }
        public string Body { get; }

        public Connection(Device start, Device target, string type, string time, string body)
        {
            this.Start = start;
            this.Target = target;
            this.Type = type;
            this.Time = time;
            this.Body = body;
        }

        public override void FillTexts(Text[] texts)
        {
            foreach (Text text in texts)
            {
                switch (text.name)
                {
                    case "source":
                        text.text += Start.Name;
                        break;
                    case "target":
                        text.text += Target.Name;
                        break;
                    case "type":
                        text.text += Type;
                        break;
                    case "time":
                        text.text += Time;
                        break;
                    case "body":
                        text.text += Body;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}