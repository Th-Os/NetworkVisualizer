using System;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;

// Functionality: All Input will be tracked. Different "Game States", different Events will be broadcasted
public class InputController : MonoBehaviour, IInputHandler
{

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
    private GestureRecognizer recognizer;

    // Use this for initialization
    void Start () {
        CurrentState = States.DefineDevice;

        Events.OnDefineProcessEnded += OnSwitchToStateOne;
        Events.OnTestStarted += OnSwitchToStateTwo;
        Events.OnTestEnded += OnSwitchToStateOne;

        InteractionManager.InteractionSourcePressed += OnClick;

        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.Tapped += OnTap;
        recognizer.GestureError += OnError;
        recognizer.StartCapturingGestures();
        _currentlyOnObject = false;
        _trackGaze = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_trackGaze && CurrentState == States.Visualize)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(
                    Camera.main.transform.position,
                    Camera.main.transform.forward,
                    out hitInfo))
            {
                // If the Raycast has succeeded and hit a hologram
                // hitInfo's point represents the position being gazed at
                // hitInfo's collider GameObject represents the hologram being gazed at
                GameObject hitObject = null;
                if (hitInfo.collider != null)
                    hitObject = hitInfo.collider.gameObject;
                if (hitObject != null && !hitObject.tag.Equals("Untagged") && !_currentlyOnObject)
                {
                    OnHighlightObject(hitObject);
                }
                else
                {
                    if(_currentlyOnObject && _currentObject != null)
                    {
                        OnHideObject();                        
                    }
                }

            }
        }

    }

    void OnTap(TappedEventArgs args)
    {
        Debug.Log("TAP " + args.tapCount);
        if (_currentlyOnObject && _currentObject != null)
        {
            Debug.Log("Clicked on " + _currentObject.name);
            Debug.Log("Get Data of a " + _currentObject.tag);

            if (CurrentState == States.ChooseTest && _currentObject.tag.Equals("Test"))
            {
                Debug.Log("Test: " + _currentObject.name);
                Events.Broadcast(Events.EVENTS.START_TEST, Convert.ToInt32(_currentObject.name));
            }

            //Get Data of:
            if (CurrentState == States.Visualize)
            {
                if (_currentObject.tag.Equals("Connection"))
                {
                    DeviceConnection dc = _currentObject.GetComponent<DeviceConnection>();
                    Events.Broadcast(Events.EVENTS.REQUEST_LOCAL_DATA, dc.Source, dc.Target);
                }

                //Not sure if needed.
                if (_currentObject.tag.Equals("Call"))
                {

                }

                if (_currentObject.tag.Equals("Device"))
                {
                    Events.Broadcast(Events.EVENTS.REQUEST_LOCAL_DATA, _currentObject);
                }
            }
        }
    }

    void OnClick(InteractionSourcePressedEventArgs args)
    {
        Debug.Log("INTERACTION PRESS: " + args.pressType);
        if (_currentlyOnObject && _currentObject != null)
        {
            Debug.Log("Clicked on " + _currentObject.name);
            Debug.Log("Get Data of a " + _currentObject.tag);

            if(CurrentState == States.ChooseTest && _currentObject.tag.Equals("Test"))
            {
                Debug.Log("Test: " + _currentObject.name);
                Events.Broadcast(Events.EVENTS.START_TEST, Convert.ToInt32(_currentObject.name));
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
        Events.Broadcast(Events.EVENTS.HIGHLIGHT_OJECT, obj);
        Debug.Log("It is possible display data of " + obj.name + " with id " + obj.GetInstanceID() + " and tag " + obj.tag);
    }

    void OnHideObject()
    {
        Events.Broadcast(Events.EVENTS.HIDE_OBJECT, _currentObject);
        _currentlyOnObject = false;
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

    void OnError(GestureErrorEventArgs args)
    {
        Debug.Log(args.error);
        recognizer.Dispose();
    }

    //TODO TEST !!!
    public void OnInputDown(InputEventData eventData)
    {
        Debug.Log("OnInputDown " + eventData.PressType + " " + eventData.selectedObject);
    }

    public void OnInputUp(InputEventData eventData)
    {
        Debug.Log("OnInputUp " + eventData.PressType + " " + eventData.selectedObject);
    }
}
