using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.AnimatedLineRenderer;

public class DrawAnimatedLine : MonoBehaviour {

    private LineRenderer _line;

	// Use this for initialization
	void Start () {
        _line = GetComponent<LineRenderer>();
        GetComponent<AnimatedLineRenderer>().Enqueue(_line.GetPosition(0));
        GetComponent<AnimatedLineRenderer>().Enqueue(_line.GetPosition(1));

    }

    // Update is called once per frame
    void Update () {
		
	}
}
