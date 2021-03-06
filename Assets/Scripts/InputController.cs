﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;
using Helpers;
using NetworkVisualizer.Enums;
using NetworkVisualizer.Visual;


namespace NetworkVisualizer
{
    /// <summary>
    /// The InputController works as a GlobalListener and is registered on input and hold events.
    /// Furthermore it implements functionality to manage the hold interaction and differences between states and clicked objects.
    /// </summary>
    public class InputController : SetGlobalListenerHololens, SetGlobalListener
    {

        public Transform HoldIndicator;
        public Color HoldStartColor;
        public Color HoldInitiatedColor;
        public static States CurrentState { get; private set; }
        

        private GameObject _focusedObject;
        private GestureRecognizer _recognizer;
        private Transform _holdIndicator;
        private States _lastState;

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


        /// <summary>
        /// All taps or clicks will be handled by this method.
        /// It has a different functionality for every combination of state and object that was clicked.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnInputDown(InputEventData eventData)
        {
            //Debug.Log("current state: " + CurrentState);
            //Debug.Log("has selected object: ");
            //Debug.Log(eventData.selectedObject != null);
            //if (eventData.selectedObject != null)
            //    Debug.Log(eventData.selectedObject.name);

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
                            CurrentState = States.VISUALIZE;
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
            else
            {
                if (CurrentState != States.DEFINE && CurrentState != States.MENU && eventData.selectedObject.CompareTag("Untagged") && eventData.selectedObject.name.IndexOf("surface", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    _lastState = CurrentState;
                    CurrentState = States.MENU;
                    Debug.Log("Hello indicator");
                    GameObject cursor = GameObject.Find("DefaultCursor");
                    Vector3 position = (cursor != null) ? cursor.transform.position : transform.position;
                    _holdIndicator = Instantiate(HoldIndicator, position, transform.rotation, transform);
                    _holdIndicator.GetComponent<Canvas>().worldCamera = Camera.main;
                    _holdIndicator.LookAt(Camera.main.transform);
                    foreach (Image img in _holdIndicator.GetComponentsInChildren<Image>())
                    {
                        img.color = HoldStartColor;
                    }
                }
            }
        }

        /// <summary>
        /// OnInputUp indicates a exit of an interaction. If the interaction before was of the type hold, then it will cancel.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnInputUp(InputEventData eventData)
        {
            if (_holdIndicator != null)
            {
                CurrentState = _lastState;
                Destroy(_holdIndicator.gameObject);
            }
        }

        #region private methods

        private void OnFocus(GameObject obj)
        {
            if (obj != null)
            {
                _focusedObject = obj;
            }
        }

        private void OnUnfocus(GameObject obj)
        {
            _focusedObject = null;
        }

        private void OnSwitchToStateTwo(int id)
        {
            CurrentState = States.VISUALIZE;
        }

        private  void OnSwitchToStateOne()
        {
            OnSwitchToStateOne(1);
        }

        private void OnSwitchToStateOne(int test)
        {
            CurrentState = States.TEST;
            Debug.Log("Hello State " + CurrentState);
            if (_recognizer == null)
            {
                _recognizer = new GestureRecognizer();
                _recognizer.SetRecognizableGestures(GestureSettings.Hold);

                _recognizer.HoldStarted += OnHoldStarted;
                _recognizer.HoldCompleted += OnHold;
                _recognizer.StartCapturingGestures();
            }
        } 

        private void OnHoldStarted(HoldStartedEventArgs args)
        {
            if (_focusedObject == null && _holdIndicator != null)                       
            {
                foreach(Image img in _holdIndicator.GetComponentsInChildren<Image>())
                {
                    img.color = HoldInitiatedColor;
                }
                _holdIndicator.GetComponentInChildren<Text>().text = "Release";
            }
        }

        private void OnHold(HoldCompletedEventArgs args)
        {
            if (_focusedObject == null && _holdIndicator != null)
            {
                
                Destroy(_holdIndicator.gameObject);
                if (_lastState == States.VISUALIZE)
                {
                    EventHandler.Broadcast(Events.OPEN_MENU);
                }

                if(_lastState == States.TEST)
                {
                    EventHandler.Broadcast(Events.SHOW_TEST, 0);
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
        #endregion
    }
}