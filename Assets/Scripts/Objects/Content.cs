using System;

namespace NetworkVisualizer.Objects
{
    /// <summary>
    /// Content of a device. Not used right now.
    /// </summary>
    [Serializable]
    public class Content
    {
        public string Value { get; set; }

        public Content(string value)
        {
            Value = value;
        }

        public string ToText()
        {
            return Value;
        }
    }
}
