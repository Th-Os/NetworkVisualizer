using System;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using Helpers;
using NetworkVisualizer.Enums;
using UnityEngine.XR.WSA.Input;

namespace NetworkVisualizer
{
    // Functionality: All Input will be tracked. Different "Game States", different Events will be broadcasted
    public class InputController : SetGlobalListenerHololens, SetGlobalListener
    {

        public static States CurrentState { get; private set; }

        private GameObject _focusedObject;
        private GestureRecognizer _recognizer;
        private States _lastState;

        // Use this for initialization
        void Start()
        {
            CurrentState = States.DEFINE;
            _lastState = CurrentState;
            EventHandler.OnDefineProcessEnded += OnSwitchToStateOne;
            EventHandler.OnTestStarted += OnSwitchToStateTwo;
            EventHandler.OnTestEnded += OnSwitchToStateOne;
            EventHandler.OnFocus += OnFocus;
            EventHandler.OnUnfocus += OnUnfocus;
        }

        void OnFocus(GameObject obj)
        {
            if (obj != null)
            {
                Debug.Log("Hello Focused object " + obj.name);
                _focusedObject = obj;
            }
        }

        void OnUnfocus(GameObject obj)
        {
            _focusedObject = null;
        }

        void OnSwitchToStateTwo(int id)
        {
            CurrentState = States.VISUALIZE;
        }

        void OnSwitchToStateOne()
        {
            OnSwitchToStateOne(0);
        }

        void OnSwitchToStateOne(int test)
        {
            CurrentState = States.TEST;

            _recognizer = new GestureRecognizer();
            _recognizer.SetRecognizableGestures(GestureSettings.Hold);

            _recognizer.HoldCompleted += OnHold;
            _recognizer.StartCapturingGestures();
        }

        void OnHold(HoldCompletedEventArgs args)
        {
            Debug.Log("Start OnHold");
            if (CurrentState == States.TEST || CurrentState == States.VISUALIZE)
            {
                if (_focusedObject == null)
                {
                    if (CurrentState == States.VISUALIZE)
                    {
                        //MAYBE PAUSE CONNECTIONS
                        Debug.Log("Open Menu");
                        EventHandler.Broadcast(Events.OPEN_MENU, CurrentState);
                        CurrentState = States.MENU;
                    }

                    if(CurrentState == States.TEST)
                    {
                        Debug.Log("Open Testmenu");
                        EventHandler.Broadcast(Events.SHOW_TEST, 0);
                    }
                    
                }
                Debug.Log("On Hold started");
            }
        }

        //TODO TEST !!!
        public void OnInputDown(InputEventData eventData)
        {
            if (_focusedObject == null)
                Debug.Log("focused object is null");

            if (_focusedObject != null)
            {
                if (CurrentState != States.DEFINE)
                {
                    Debug.Log("OnInputDown " + eventData.PressType + " " + eventData.selectedObject);
                    GameObject obj = eventData.selectedObject;

                    if (_focusedObject == null)
                        Debug.Log("focused object is null");
                    else
                        Debug.Log("Clicked on " + obj.name + " and focused on " + _focusedObject.name);

                    Interaction i = Utils.SearchFor<Interaction>(obj);

                    if (i == null)
                    {
                        Debug.Log("No Interaction found");
                        return;
                    }

                    Debug.Log(i.gameObject);

                    obj = i.gameObject;

                    i.OnClick();

                    if (CurrentState == States.TEST)
                    {
                        Debug.Log("Test: " + obj.name);
                        Debug.Log("Test Focused: " + obj);

                        if (obj != null)
                        {
                            Debug.Log("TestObj: " + obj.name);
                            EventHandler.Broadcast(Events.START_TEST, Convert.ToInt32(obj.name));
                        }
                    }

                    if (_focusedObject != null)
                    {
                        Debug.Log("Clicked on " + _focusedObject.name);


                        if (CurrentState == States.TEST && _focusedObject.tag.Equals("Test"))
                        {
                            Debug.Log("Test: " + _focusedObject.name);
                            EventHandler.Broadcast(Events.START_TEST, Convert.ToInt32(_focusedObject.name));
                        }

                        //Get Data of:
                        if (CurrentState == States.VISUALIZE)
                        {
                            Debug.Log("Get Data of a " + _focusedObject.tag);
                            if (_focusedObject.tag.Equals("Connection"))
                            {
                                DeviceConnection dc = _focusedObject.GetComponent<DeviceConnection>();
                                //DEBUG SETTING
                                GetComponent<VisualizationManager>().ShowConnectionData(dc, null);

                                EventHandler.Broadcast(Events.REQUEST_LOCAL_DATA, dc.Source, dc.Target);
                            }

                            if (_focusedObject.tag.Equals("Device"))
                            {
                                //DEBUG SETTING
                                GetComponent<VisualizationManager>().ShowDeviceData(_focusedObject, null);

                                EventHandler.Broadcast(Events.REQUEST_LOCAL_DATA, _focusedObject);
                            }
                            if (_focusedObject.tag.Equals("Menu"))
                            {
                                Debug.Log("Hello menu item: " + _focusedObject.name);
                                if (_focusedObject.name.Equals("Minimize"))
                                {
                                    EventHandler.Broadcast(Events.HIDE_MENU);
                                }
                                if (_focusedObject.name.Equals("Close"))
                                {
                                    Destroy(_focusedObject.transform.parent.parent.gameObject);
                                    EventHandler.Broadcast(Events.END_TEST, 0);

                                }
                            }
                        }

                        //TEST!!!
                        if (CurrentState == States.MENU)
                        {
                            if (_focusedObject != null && _focusedObject.CompareTag("Menu"))
                            {
                                Debug.Log("Menu: " + _focusedObject.name);

                            }
                            CurrentState = _lastState;
                        }
                    }
                }
            }
        }

        public void OnInputUp(InputEventData eventData)
        {
            Debug.Log("OnInputUp " + eventData.PressType + " " + eventData.selectedObject);
        }

        private void OnDestroy()
        {
            if (_recognizer != null)
            {
                _recognizer.StopCapturingGestures();
                _recognizer.Dispose();
            }
        }
    }
}