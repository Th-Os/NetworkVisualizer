using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public enum States
    {
        DefineDevice,
        ChooseTest,
        Visualize
    }

    public static States CurrentState { get; set; }

    // Functionality: All Input will be tracked. Different "Game States", different Events will be broadcasted

    // Use this for initialization
    void Start () {
        CurrentState = States.DefineDevice;

        Events.OnTestStarted += OnSwitchToStateTwo;
        Events.OnTestEnded += OnSwitchToStateOne;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnSwitchToStateTwo(int id)
    {
        CurrentState = States.Visualize;
    }

    void OnSwitchToStateOne()
    {
        CurrentState = States.ChooseTest;
    }
}
