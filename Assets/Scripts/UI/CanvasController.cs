using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    public GameObject TestCanvas;  

    private bool active;

	// Use this for initialization
	void Start () {
        TestCanvas = Instantiate(TestCanvas, transform);
        Events.OnTestUISwitched += ToggleTestUI;
        //EventManager.AddHandler<string>(EVNT.SwitchTestUI, ToggleTestUI);
        active = false;
        TestCanvas.SetActive(active);
	}
	
    void ToggleTestUI()
    {
        Debug.Log("Toggle TEST UI");
        active = (active) ? false : true;
        TestCanvas.SetActive(active);
    }
}
