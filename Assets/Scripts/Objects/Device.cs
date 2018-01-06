﻿using System;

namespace NetworkVisualizer.Objects
{

    [Serializable]
    public class Device
    {
        public string name { get; }
        public Position position { get; }

        public Device(string name, Position position)
        {
            this.name = name;
            this.position = position;
        }

    }
}