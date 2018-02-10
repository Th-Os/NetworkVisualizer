using System;
using UnityEngine;

namespace NetworkVisualizer.Objects
{

    [Serializable]
    public class Position
    {
        public float x { get; }
        public float y { get; }
        public float z { get; }

        public Position(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Position(Vector3 position)
        {
            x = position.x;
            y = position.y;
            z = position.z;
        }
    }
}
