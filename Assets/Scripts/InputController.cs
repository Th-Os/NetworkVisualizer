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

        public Transform HoldIndicator;
        public static States CurrentState { get; private set; }

        private GameObject _focusedObject;
        private GestureRecognizer _recognizer;
        private Transform _holdIndicator;

        // Use this for initialization
        void Start()
        {
            CurrentState = States.DEFINE;
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

            if (_recognizer == null)
            {
                _recognizer = new GestureRecognizer();
                _recognizer.SetRecognizableGestures(GestureSettings.Hold);

                _recognizer.HoldStarted += OnHoldStarted;
                _recognizer.HoldCompleted += OnHold;
                _recognizer.StartCapturingGestures();
            }
        } 

        void OnHoldStarted(HoldStartedEventArgs args)
        {
            if (_focusedObject == null)
            {
                _holdIndicator = Instantiate(HoldIndicator, transform);
                _holdIndicator.GetComponent<Canvas>().worldCamera = Camera.main;
            }
        }

        void OnHold(HoldCompletedEventArgs args)
        {
            if (_focusedObject == null)
            {
                Destroy(_holdIndicator.gameObject);
                if (CurrentState == States.VISUALIZE)
                {
                    EventHandler.Broadcast(Events.OPEN_MENU);
                }

                if(CurrentState == States.TEST)
                {
                    EventHandler.Broadcast(Events.SHOW_TEST, 0);
                }
                    
            }
        }

        public void OnInputDown(InputEventData eventData)
        {
            if (_focusedObject != null)
            {
                Debug.Log("Clicked: " + _focusedObject.tag + ": " + _focusedObject.name);
                switch (_focusedObject.tag)
                {
                    case "Test":
                        EventHandler.Broadcast(Events.START_TEST, Convert.ToInt32(_focusedObject.name));
                        CurrentState = States.VISUALIZE;
                        break;
                    case "Menu":
                        if (_focusedObject.name.Equals("Minimize"))
                        {
                            EventHandler.Broadcast(Events.HIDE_MENU);
                        }
                        if (_focusedObject.name.Equals("Close"))
                        {
                            Destroy(_focusedObject.transform.parent.parent.gameObject);
                            EventHandler.Broadcast(Events.END_TEST, 0);
                            EventHandler.Broadcast(Events.DESTROY_VISUALIZATION);
                            CurrentState = States.TEST;
                        }
                        break;
                    case "Connection":
                        if (CurrentState == States.VISUALIZE)
                        {
                            DeviceConnection dc = _focusedObject.GetComponent<DeviceConnection>();
                            //DEBUG SETTING
                            //GetComponent<VisualizationManager>().ShowConnectionData(dc, null);

                            EventHandler.Broadcast(Events.REQUEST_LOCAL_DATA, dc.Source, dc.Target);
                        }
                        break;
                    case "Device":
                        if (CurrentState == States.VISUALIZE)
                        {
                            //GetComponent<VisualizationManager>().ShowDeviceData(_focusedObject, null);
                            EventHandler.Broadcast(Events.REQUEST_LOCAL_DATA, _focusedObject.transform);
                        }
                        break;
                    case "Panel":
                        if (CurrentState == States.VISUALIZE)
                        {
                            Utils.SearchFor<Interaction>(_focusedObject).OnClick();
                        }
                        break;

                }
            }

        }

        private void OnDestroy()
        {
            if (_recognizer != null)
            {
                _recognizer.StopCapturingGestures();
                _recognizer.Dispose();
            }
        }

        public void OnInputUp(InputEventData eventData){}
    }
}