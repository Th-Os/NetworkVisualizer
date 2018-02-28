using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.AnimatedLineRenderer;

public class DrawConnection : MonoBehaviour {

    public float Duration = 2f;

    private AnimatedLineRenderer _aLine;
    private bool _hasStarted;

	// Use this for initialization
	void Start () {
        _aLine = GetComponent<AnimatedLineRenderer>();
        _aLine.SecondsPerLine = Duration;
        _hasStarted = false;
    }

    public DrawConnection Init(float width, float duration)
    {
        _aLine.StartWidth = width;
        _aLine.EndWidth = width;
        _aLine.SecondsPerLine = duration;
        Duration = duration;
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
