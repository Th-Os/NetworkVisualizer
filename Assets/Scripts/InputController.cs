using System;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using Helpers;

// Functionality: All Input will be tracked. Different "Game States", different Events will be broadcasted
public class InputController : SetGlobalListenerHololens, SetGlobalListener
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
    private Transform _focusedObject;
    //private GestureRecognizer recognizer;

    // Use this for initialization
    void Start () {
        CurrentState = States.DefineDevice;

        Events.OnDefineProcessEnded += OnSwitchToStateOne;
        Events.OnTestStarted += OnSwitchToStateTwo;
        Events.OnTestEnded += OnSwitchToStateOne;
        Events.OnFocus += OnFocus;
        Events.OnUnfocus += OnUnfocus;

        /*
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.Tapped += OnTap;
        recognizer.GestureError += OnError;
        recognizer.StartCapturingGestures();
        */
        _currentlyOnObject = false;
        _trackGaze = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
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
                    if(_currentlyOnObject && _currentObject != null && _currentObject != hitObject)
                    {
                        OnHideObject();                        
                    }
                }

            }
        }*/

    }

    void OnFocus(Transform obj)
    {
        if(_focusedObject == null)
        {
            _focusedObject = obj;
        }
    }

    void OnUnfocus(Transform obj)
    {
        if(_focusedObject != null && _focusedObject.Equals(obj))
        {
            _focusedObject = null;
        }
    }

    /*
    void OnTap(TappedEventArgs args)
    {
        Debug.Log("TAP " + args.tapCount);

        if (CurrentState == States.ChooseTest)
        {
            Debug.Log("Test: " + args);
            
            if(_focusedObject != null)
            {
                Debug.Log("TestObj: " + _focusedObject.name);
                Events.Broadcast(Events.EVENTS.START_TEST, Convert.ToInt32(_focusedObject.name));
            }
        }

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

    void OnError(GestureErrorEventArgs args)
    {
        Debug.Log(args.error);
        recognizer.Dispose();
    }

    */
    void OnHighlightObject(GameObject obj)
    {
        _currentlyOnObject = true;
        if (obj != null)
        {           
            _currentObject = obj;
            Debug.Log("It is possible display data of " + obj.name + " with id " + obj.GetInstanceID() + " and tag " + obj.tag);
            Events.Broadcast(Events.EVENTS.HIGHLIGHT_OJECT, obj);
        }
    }

    void OnHideObject()
    {
        _currentlyOnObject = false;
        if (_currentObject != null) {
            Events.Broadcast(Events.EVENTS.HIDE_OBJECT, _currentObject);            
        }
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

    //TODO TEST !!!
    public void OnInputDown(InputEventData eventData)
    {
        Debug.Log("OnInputDown " + eventData.PressType + " " + eventData.selectedObject);
        GameObject obj = eventData.selectedObject;
        AbstractInteraction i = Utils.SearchFor<AbstractInteraction>(obj);

        Debug.Log(i.gameObject);

        if (CurrentState == States.ChooseTest)
        {
            Debug.Log("Test: " + i.name);

            if (_focusedObject != null)
            {
                Debug.Log("TestObj: " + _focusedObject.name);
                Events.Broadcast(Events.EVENTS.START_TEST, Convert.ToInt32(_focusedObject.name));
            }
        }

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

    public void OnInputUp(InputEventData eventData)
    {
        Debug.Log("OnInputUp " + eventData.PressType + " " + eventData.selectedObject);
    }


}
