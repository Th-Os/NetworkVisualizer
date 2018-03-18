using System;
using UnityEngine;
using UnityEngine.UI;
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
            OnSwitchToStateOne(1);
        }

        void OnSwitchToStateOne(int test)
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

        void OnHoldStarted(HoldStartedEventArgs args)
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

        void OnHold(HoldCompletedEventArgs args)
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
            }else
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
                    foreach (Image img in _holdIndicator.GetComponentsInChildren<Image>()) {
                        img.color = HoldStartColor;
                    }
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

        public void OnInputUp(InputEventData eventData){
            if(_holdIndicator != null)
            {
                CurrentState = _lastState;
                Destroy(_holdIndicator.gameObject);
            }
        }
    }
}