using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class UIInteraction : AbstractInteraction {

    /*
    public override void OnFocusEnter()
    {
        Debug.Log("UIInteraction: Enter " + gameObject.name);
    }

    public override void OnFocusExit()
    {
        Debug.Log("UIInteraction: Exit " + gameObject.name);
    }
    */

    public override void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("UIInteraction: Clicked " + gameObject.name);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
