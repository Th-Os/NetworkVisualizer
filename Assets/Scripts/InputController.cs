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

    private Transform _focusedObject;

    // Use this for initialization
    void Start () {
        CurrentState = States.DefineDevice;

        Events.OnDefineProcessEnded += OnSwitchToStateOne;
        Events.OnTestStarted += OnSwitchToStateTwo;
        Events.OnTestEnded += OnSwitchToStateOne;
        Events.OnFocus += OnFocus;
        Events.OnUnfocus += OnUnfocus;


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

    void OnSwitchToStateTwo(int id)
    {
        CurrentState = States.Visualize;
    }

    void OnSwitchToStateOne()
    {
        CurrentState = States.ChooseTest;
    }

    //TODO TEST !!!
    public void OnInputDown(InputEventData eventData)
    {
        Debug.Log("OnInputDown " + eventData.PressType + " " + eventData.selectedObject);
        GameObject obj = eventData.selectedObject;

        Debug.Log(obj);

        AbstractInteraction i = Utils.SearchFor<AbstractInteraction>(obj);

        Debug.Log(i.gameObject);

        obj = i.gameObject;

        i.OnClick();

        if (CurrentState == States.ChooseTest)
        {
            Debug.Log("Test: " + obj.name);
            Debug.Log("Test Focused: " + _focusedObject);

            if (_focusedObject != null)
            {
                Debug.Log("TestObj: " + _focusedObject.name);
                Events.Broadcast(Events.EVENTS.START_TEST, Convert.ToInt32(_focusedObject.name));
            }
        }

        if (_focusedObject != null)
        {
            Debug.Log("Clicked on " + _focusedObject.name);
            Debug.Log("Get Data of a " + _focusedObject.tag);

            if (CurrentState == States.ChooseTest && _focusedObject.tag.Equals("Test"))
            {
                Debug.Log("Test: " + _focusedObject.name);
                Events.Broadcast(Events.EVENTS.START_TEST, Convert.ToInt32(_focusedObject.name));
            }

            //Get Data of:
            if (CurrentState == States.Visualize)
            {
                if (_focusedObject.tag.Equals("Connection"))
                {
                    DeviceConnection dc = _focusedObject.GetComponent<DeviceConnection>();
                    Events.Broadcast(Events.EVENTS.REQUEST_LOCAL_DATA, dc.Source, dc.Target);
                }

                if (_focusedObject.tag.Equals("Device"))
                {
                    Events.Broadcast(Events.EVENTS.REQUEST_LOCAL_DATA, _focusedObject);
                }
            }
        }
    }

    public void OnInputUp(InputEventData eventData)
    {
        Debug.Log("OnInputUp " + eventData.PressType + " " + eventData.selectedObject);
    }


}
