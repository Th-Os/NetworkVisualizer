using System;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace NetworkVisualizer.Objects
{
    /// <summary>
    /// Representation of a device data object.
    /// </summary>
    [Serializable]
    public class Device : NetworkObject
    {
        public int Id { get; set; }
        public string Name { get; set;  }
        public string Ip { get; set; }
        public Position Position { get; set; }
        public string Content { get; set; }

        [JsonConstructor]
        public Device(int id, string name, string ip, Position position, string content)
        {
            Id = id;
            Name = name;
            Ip = ip;
            Position = position;
            Content = content;
        }

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

        public Device(string name, String ip, Position position, string content)
        {
            this.Name = name;
            this.Ip = ip;
            this.Position = position;
            this.Content = content;

        }

        public override void FillTexts(Text[] texts)
        {
            foreach(Text text in texts)
            {
                switch(text.name)
                {
                    case "name":
                        text.text = DeviceNameToUpperCase(Name);
                        break;
                    case "ip":
                        text.text += " " + Ip;
                        break;
                    case "content":
                        text.text += " " + Content;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}