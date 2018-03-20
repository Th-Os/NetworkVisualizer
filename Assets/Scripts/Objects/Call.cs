using System;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace NetworkVisualizer.Objects
{
    /// <summary>
    /// ARP protocol call.
    /// </summary>
    [Serializable]
    public class Call : NetworkObject
    {
        public int Id { get; set; }
        public Device Start { get; }
        public Device Target { get; set; }
        public string Type { get; }
        public string Time { get; }
        public string ToIp { get; set; }

        [JsonConstructor]
        public Call(int id, Device start, Device target, string type, string time, string toIp)
        {
            this.Id = id;
            this.Start = start;
            this.Target = target;
            this.Type = type;
            this.Time = time;
            this.ToIp = toIp;
        }

        public override void FillTexts(Text[] texts)
        {
            throw new NotImplementedException();
        }

    }
}
