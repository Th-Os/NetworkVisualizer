using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.World.Scripts
{
    [Serializable]
    public class Device
    {
        string name;
        Position position;

        public Device(string name, Position position)
        {
            this.name = name;
            this.position = position;
        }

    }

    // TODO: body evtl in Serializable & evtl einem Device zuordnen
    [Serializable]
    public class Connection
    {
        Device start;
        Device target;
        string type;
        string time;
        string body;

        public Connection(Device start, Device target, string type, string time, string body)
        {
            this.start = start;
            this.target = target;
            this.type = type;
            this.time = time;
            this.body = body;
        }

    }

    [Serializable]
    public class Position
    {
        double x;
        double y;
        double z;

        public Position(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

}
