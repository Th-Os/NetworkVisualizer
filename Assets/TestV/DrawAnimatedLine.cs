using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.AnimatedLineRenderer;

public class DrawAnimatedLine : MonoBehaviour {

    private LineRenderer _line;
    private AnimatedLineRenderer _aLine;

	// Use this for initialization
	void Start () {
        _line = GetComponent<LineRenderer>();
        new WaitForSeconds(2f);
        _aLine = GetComponent<AnimatedLineRenderer>();
        _aLine.Enqueue(new Vector3(0,0,1));
        _aLine.Enqueue(new Vector3(2,0,8));

    }

    // Update is called once per frame
    void Update () {
		if(_aLine.LineRenderer.positionCount == 2 && _aLine.LineRenderer.GetPosition(1) == new Vector3(2, 0, 8)) {
            _aLine.Reset();
            Destroy(gameObject);
        }
	}
}
