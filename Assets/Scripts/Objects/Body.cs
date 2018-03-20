using System;

namespace NetworkVisualizer.Objects
{
    /// <summary>
    /// Body of a connection. Is not used right now.
    /// </summary>
    [Serializable]
    public class Body
    {
        public string body;

        public Body(String body)
        {
            this.body = body;
        }
    }
}
