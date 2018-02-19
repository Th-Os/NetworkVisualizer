using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;

public class ObjectsDefiner : MonoBehaviour {

    public Transform DefineCube;
   
    private Text _text;
    private Vector3 _lastPosition;
    private Transform _currentObj;
    private GestureRecognizer _recognizer;
    private bool _disabled;

    // Use this for initialization
    void Start () {
        Events.OnDefineProcessStarted += OnStart;
        _disabled = true;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(!_disabled)
        {
            _lastPosition = GazeManager.Instance.HitInfo.point;
            if (_currentObj != null && _lastPosition != null)
            {
                _currentObj.position = _lastPosition;
                if(_text != null)
                    _text.text = _lastPosition.ToString();
            }
        }

    }

    void OnStart()
    {
        _text = GetComponentInChildren<Text>();
        _currentObj = Instantiate(DefineCube, transform);
        _recognizer = new GestureRecognizer();
        _recognizer.SetRecognizableGestures(GestureSettings.Tap);
        _recognizer.Tapped += OnTap;
        _recognizer.GestureError += OnError;
        _recognizer.StartCapturingGestures();

        InteractionManager.InteractionSourcePressed += InteractionManager_SourcePressed;

        _disabled = false;

        Debug.Log("Definer started");
    }

    void OnTap(TappedEventArgs args)
    {
        Debug.Log("TAPPED " + args.tapCount);
        
        //Instantiate(DefineCube, _currentObj.position, _currentObj.rotation, transform);

        Debug.Log(_currentObj + " : " + _currentObj.position);
        Events.Broadcast(Events.EVENTS.DEVICE_FOUND, _currentObj.position);
    }

    void OnError(GestureErrorEventArgs args)
    {
        Debug.Log(args.error);
        _recognizer.Dispose();
    }

    private void InteractionManager_SourcePressed(InteractionSourcePressedEventArgs args)
    {
        Debug.Log("PressType: " + args.pressType);
    }

    private void OnDestroy()
    {
        if (_recognizer != null)
        {
            _recognizer.Tapped -= OnTap;
            _recognizer.StopCapturingGestures();
            _recognizer.Dispose();
        }
    }

    private void OnDisable()
    {
        _disabled = true;
        if (_recognizer != null)
        {
            _recognizer.Tapped -= OnTap;
            _recognizer.StopCapturingGestures();
        }
    }
}
