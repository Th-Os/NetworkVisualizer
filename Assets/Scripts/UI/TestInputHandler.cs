using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;
using System;

// TODO: Click works even when canvas is hidden
public class TestInputHandler : MonoBehaviour {

    public void OnTestClicked(InteractionSourcePressedEventArgs args)
    {
        Debug.Log("clicked: " + this.name);
        EventManager.Broadcast(EVNT.TestClicked, this.name);
    }

    // Use this for initialization
    void Start () {
        InteractionManager.InteractionSourcePressed += OnTestClicked;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
