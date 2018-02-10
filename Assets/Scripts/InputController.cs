using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

// Functionality: All Input will be tracked. Different "Game States", different Events will be broadcasted
//TODO: INTERACTIONMANAGER COULD FAIL !! USE RECOGNIZER !!
public class InputController : MonoBehaviour {

    public enum States
    {
        DefineDevice,
        ChooseTest,
        Visualize
    }

    public static States CurrentState { get; set; }

    private bool _trackGaze;
    private bool _currentlyOnObject;
    private GameObject _currentObject;  

    // Use this for initialization
    void Start () {
        CurrentState = States.DefineDevice;

        Events.OnDefineProcessEnded += OnSwitchToStateOne;
        Events.OnTestStarted += OnSwitchToStateTwo;
        Events.OnTestEnded += OnSwitchToStateOne;

        InteractionManager.InteractionSourcePressed += OnClick;
    }

    // Update is called once per frame
    void Update()
    {
        if (_trackGaze)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(
                    Camera.main.transform.position,
                    Camera.main.transform.forward,
                    out hitInfo,
                    20.0f,
                    Physics.DefaultRaycastLayers))
            {
                // If the Raycast has succeeded and hit a hologram
                // hitInfo's point represents the position being gazed at
                // hitInfo's collider GameObject represents the hologram being gazed at
                GameObject hitObject = hitInfo.collider.gameObject;
                if (hitObject != null && !hitObject.tag.Equals("Untagged"))
                {
                    OnHighlightObject(hitObject);
                }
                else
                    _currentlyOnObject = false;
            }
        }

    }

    void OnClick(InteractionSourcePressedEventArgs args)
    {
        if (_currentlyOnObject)
        {
            Debug.Log("Clicked on " + _currentObject.name);
            Debug.Log("Get Data of a " + _currentObject.tag);

            if(CurrentState == States.ChooseTest && _currentObject.tag.Equals("Test"))
            {
                Debug.Log("Test: " + _currentObject.name);
                Events.Broadcast(Events.EVENTS.START_TEST, 1);
            }

            //Get Data of:
            if(CurrentState == States.Visualize)
            {
                if(_currentObject.tag.Equals("Connection"))
                {
                    DeviceConnection dc = _currentObject.GetComponent<DeviceConnection>();
                    Events.Broadcast(Events.EVENTS.REQUEST_LOCAL_DATA, dc.Source, dc.Target);
                }

                //Not sure if needed.
                if (_currentObject.tag.Equals("Call"))
                {

                }

                if(_currentObject.tag.Equals("Device"))
                {
                    Events.Broadcast(Events.EVENTS.REQUEST_LOCAL_DATA, _currentObject);
                }
            }
        }
    }

    void OnHighlightObject(GameObject obj)
    {
        _currentlyOnObject = true;
        _currentObject = obj;
        Debug.Log("It is possible display data of " + obj.name + " with id " + obj.GetInstanceID() + " and tag " + obj.tag);
    }

    void OnSwitchToStateTwo(int id)
    {
        CurrentState = States.Visualize;
    }

    void OnSwitchToStateOne()
    {
        _trackGaze = true;
        CurrentState = States.ChooseTest;
    }
}
