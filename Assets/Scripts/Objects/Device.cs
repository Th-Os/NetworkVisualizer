﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace NetworkVisualizer.Objects
{

    [Serializable]
    public class Device : NetworkObject
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

        public override void FillTexts(Text[] texts)
        {
            foreach(Text text in texts)
            {
                switch(text.name)
                {
                    case "name":
                        text.text += Name;
                        break;
                    case "ip":
                        text.text += Ip;
                        break;
                    case "content":
                        text.text += Content.ToText();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}