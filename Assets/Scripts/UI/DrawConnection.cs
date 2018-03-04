using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.AnimatedLineRenderer;

public class DrawConnection : MonoBehaviour {

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

    public void Connect(Transform source, Transform target)
    {
        Debug.Log(source + " to " + target);
        _source = source;
        _target = target;
        _aLine.Enqueue(source.position);
        _aLine.Enqueue(target.position);
        _hasStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasStarted && _aLine.LineRenderer.positionCount == 2 && _aLine.LineRenderer.GetPosition(1) == _target.position)
        {
            _aLine.Reset();
            //Destroy(gameObject);
        }
    }
}
