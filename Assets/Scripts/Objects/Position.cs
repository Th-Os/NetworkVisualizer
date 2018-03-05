using System;
using UnityEngine;
using Newtonsoft.Json;

namespace NetworkVisualizer.Objects
{

    [Serializable]
    public class Position
    {
        public int Id {get; set;}
        public float x { get; }
        public float y { get; }
        public float z { get; }

        [JsonConstructor]
        public Position(int id, float x, float y, float z)
        {
            this.Id = id;
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
