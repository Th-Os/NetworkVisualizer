using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.AnimatedLineRenderer;

public class DrawConnection : MonoBehaviour {

    private AnimatedLineRenderer _aLine;
    private bool _hasStarted;

    public DrawConnection Init(float width, float duration)
    {
        _aLine = GetComponent<AnimatedLineRenderer>();
        _hasStarted = false;
        _aLine.StartWidth = width;
        _aLine.EndWidth = width;
        _aLine.SecondsPerLine = duration;
        return this;
    }

    public void Connect(Transform source, Transform target)
    {
        _aLine.Enqueue(source.position);
        _aLine.Enqueue(target.position);
        _hasStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasStarted && _aLine.LineRenderer.positionCount == 2 && _aLine.LineRenderer.GetPosition(1) == new Vector3(2, 0, 8))
        {
            _aLine.Reset();
            Destroy(gameObject);
        }
    }
}
