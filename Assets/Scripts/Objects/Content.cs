using System;

namespace NetworkVisualizer.Objects
{
    [Serializable]
    public class Content
    {
        public string Value { get; set; }

        public Content(string value)
        {
            Value = value;
        }
    }
}
