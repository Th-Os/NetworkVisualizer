using System;

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
    }
}
