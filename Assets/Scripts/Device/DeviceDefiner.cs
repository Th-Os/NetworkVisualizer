using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;
using NetworkVisualizer.Enums;

namespace NetworkVisualizer
{
    /// <summary>
    /// The DeviceDefiner handles the device position definition. As such it indicates where the user is looking and informs the <see cref="DeviceManager"/> on a tap.
    /// </summary>
    public class DeviceDefiner : MonoBehaviour
    {

        public Transform DefineCube;

        private Text _text;
        private Vector3 _lastPosition;
        private Transform _currentObj;
        private GestureRecognizer _recognizer;
        private bool _disabled;

        // Use this for initialization
        void Start()
        {
            EventHandler.OnDefineProcessStarted += OnStart;
            _disabled = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!_disabled)
            {
                _lastPosition = GazeManager.Instance.HitInfo.point;
                if (_currentObj != null && _lastPosition != null)
                {
                    _currentObj.position = _lastPosition;
                    if (_text != null)
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
            _disabled = false;

            Debug.Log("Definer started");
        }

        void OnTap(TappedEventArgs args)
        {
            Debug.Log("TAPPED " + args.tapCount);
            Debug.Log(_currentObj + " : " + _currentObj.position);
            EventHandler.Broadcast(Events.DEVICE_FOUND, _currentObj.position);
        }

        void OnError(GestureErrorEventArgs args)
        {
            Debug.Log(args.error);
            _recognizer.Dispose();
        }

        private void OnDestroy()
        {
            if (_currentObj != null)
            {
                Destroy(_currentObj.gameObject);
            }
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
            if (_currentObj != null)
            {
                Destroy(_currentObj.gameObject);
            }

        }
    }
}