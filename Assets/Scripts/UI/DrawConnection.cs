using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.AnimatedLineRenderer;
using NetworkVisualizer;

public class DrawConnection : MonoBehaviour {

    public Material ConnectionUp;
    public Material ConnectionDown;

    private AnimatedLineRenderer _aLine;
    private bool _hasStarted;

    private Transform _source;
    private Transform _target;

    public DrawConnection Init(float duration)
    {
        _aLine = GetComponent<AnimatedLineRenderer>();
        LineRenderer line = GetComponent<LineRenderer>();
        _hasStarted = false;
        _aLine.StartWidth = line.startWidth;
        _aLine.EndWidth = line.endWidth;
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
            Debug.Log("DOING A DESTROY NOW ON DRAWCONNECTION");
            Destroy(gameObject);
        }
    }

    private void SetMaterial(DeviceConnection dc)
    {
        if (dc != null)
        {
            if (_source.name.Equals(dc.Source.name))
                _aLine.LineRenderer.material = ConnectionUp;
            else
                _aLine.LineRenderer.material = ConnectionDown;
        }

    }
}
