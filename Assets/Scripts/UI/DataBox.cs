using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class DataBox : AbstractInteraction {

    public override void OnFocusEnter()
    {
        Debug.Log("DataBox: Enter " + gameObject.name);
    }

    public override void OnFocusExit()
    {
        Debug.Log("DataBox: Exit " + gameObject.name);
    }

    public override void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("DataBox: Clicked " + gameObject.name);
    }

    // Use this for initialization
    protected void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}
}
