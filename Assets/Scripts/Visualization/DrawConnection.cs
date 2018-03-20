using UnityEngine;
using DigitalRuby.AnimatedLineRenderer;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// DrawConnection will draw a line with the <see cref="AnimatedLineRenderer"/> feature to visualize incoming or outgoing connections.
    /// </summary>
    public class DrawConnection : MonoBehaviour
    {

        /// <summary>
        /// Right now only the ConnectionUp material is used, because the material changes faces, when the line moves from the other side.
        /// </summary>
        public Material ConnectionUp;
        public Material ConnectionDown;

        private AnimatedLineRenderer _aLine;
        private LineRenderer _line;
        private bool _hasStarted;

        private Transform _source;
        private Transform _target;

        public DrawConnection Init(float duration)
        {
            _aLine = GetComponent<AnimatedLineRenderer>();
            _line = GetComponent<LineRenderer>();
            _hasStarted = false;
            _aLine.StartWidth = _line.startWidth;
            _aLine.EndWidth = _line.endWidth;
            _aLine.SecondsPerLine = duration;
            return this;
        }

        public void Connect(Transform source, Transform target, DeviceConnection dc)
        {
            Debug.Log(source + " to " + target);
            _source = source;
            _target = target;
            SetMaterial(dc);
            _aLine.Enqueue(source.position);
            _aLine.Enqueue(target.position);
            _hasStarted = true;
        }

        void Update()
        {
            if (_hasStarted && _aLine.LineRenderer.positionCount == 2 && _aLine.LineRenderer.GetPosition(1) == _target.position)
            {
                _aLine.Reset();
                Destroy(gameObject);
            }
        }

        private void SetMaterial(DeviceConnection dc)
        {
            if (dc != null)
            {
                //Debug.Log(_line.material.name);
                //Debug.Log(_source.name + " vs " + dc.Source.name);
                //Debug.Log(_source.name.Equals(dc.Source.name));
                _line.material = ConnectionUp;
                //if (_source.name.Equals(dc.Source.name))
                //    _line.material = ConnectionUp;
                //else
                //    _line.material = ConnectionDown;

                //line.UpdateGIMaterials();
                Debug.Log(_line.material.name);
            }

        }
    }
}