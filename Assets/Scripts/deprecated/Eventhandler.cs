using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventhandler : MonoBehaviour {

    /*

    public float smoothTime = 0.3f;
    public GameObject line;
    public GameObject canvas;

    private Transform cube;
    private LineRenderer renderer;
    private CanvasGroup cGroup;

    private Vector3 velocity = Vector3.one;

	// Use this for initialization
	void Start () {
        cube = this.transform.GetChild(0);
        renderer = line.GetComponent<LineRenderer>();
        cGroup = canvas.GetComponent<CanvasGroup>();
        //EventManager.AddHandler<string>(EVNT.TestClicked, OnUI);


	}

    // https://gist.github.com/brean/5210d01240116573d61556173837745e
    void Update () {
        
        // move cube to camera
        Quaternion toQuat = Camera.main.transform.rotation;

        Vector3 targetPosition = Camera.main.transform.position;



        // move according to start position

        targetPosition += Camera.main.transform.forward * cube.position.z;

        targetPosition += Camera.main.transform.up * cube.position.y;

        targetPosition += Camera.main.transform.right * cube.position.x;



        // smoothly rotate and position in front of camera

        cube.position = targetPosition;

        Debug.Log("cube position: " + cube.position);

        cube.rotation = toQuat;

        Vector3 start = Camera.main.transform.position;
        start.y = -1;
        renderer.SetPosition(0, start);
        renderer.SetPosition(1, cube.position);

        Debug.Log("start: " + start);
        Debug.Log("target: " + cube.position);
        
    }

    private void OnUI(string name)
    {
        //EventManager.Broadcast(EVNT.StartTest, name);
        Debug.Log("name of clicked: " + name);
    }
    */
}
