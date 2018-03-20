using System;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace NetworkVisualizer.Objects
{
    /// <summary>
    /// A network connection.
    /// </summary>
    [Serializable]
    public class Connection : NetworkObject
    {
        public int Id { get; set; }
        public Device Start { get; }
        public Device Target { get; }
        public string Type { get; }
        public string Time { get; }
        public string Body { get; }

        [JsonConstructor]
        public Connection(int id, Device start, Device target, string type, string time, string body)
        {
            this.Id = id;
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
                        text.text += DeviceNameToUpperCase(Start.Name);
                        break;
                    case "target":
                        text.text += DeviceNameToUpperCase(Target.Name);
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