using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineWalking : MonoBehaviour {

    public Transform Source;
    public Transform Target;

    private LineRenderer _line;

	// Use this for initialization
	void Start () {
        _line = GetComponent<LineRenderer>();
        _line.SetPosition(0, Source.position);
        _line.SetPosition(1, Source.forward);

        StartCoroutine(Walk());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Walk()
    {
        while (_line.GetPosition(0) != Target.position)
        {
            yield return new WaitForSeconds(2f);
            _line.SetPosition(0, _line.GetPosition(1));
            _line.SetPosition(1, _line.GetPosition(0) + Vector3.forward);
        }

    }
}
