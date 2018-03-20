using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using NetworkVisualizer.Enums;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// The DeviceAnimator is responsible for animating the call and answer of the arp protocol of a device.
    /// </summary>
    public class DeviceAnimator : MonoBehaviour
    {

        public Transform CallIndicator;
        private Animator _anim;
        private Transform _indicator;
        private Action<Transform, string> _callback;
        private Transform _target;
        private string _toIp;
        private bool _isAnswering;

        // Use this for initialization
        void Start()
        {
            _anim = GetComponent<Animator>();
            _anim.GetBehaviour<DeviceAnimationBehaviour>().OnExit += OnStop;
        }

        /// <summary>
        /// Starts the arp call animation of a device.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="toIp"></param>
        /// <param name="callback"></param>
        public void Call(Transform target, string toIp, Action<Transform, string> callback)
        {
            _anim.Play("Call");
            _indicator = Instantiate(CallIndicator, transform).transform;
            _indicator.GetComponent<Canvas>().worldCamera = Camera.main;
            _indicator.GetComponentInChildren<Text>().text += toIp;
            _indicator.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
            _callback = callback;
            _target = target;
            _toIp = toIp;
        }


        /// <summary>
        /// Starts the arp answer animation of a device.
        /// </summary>
        /// <param name="toIp"></param>
        public void Answer(string toIp)
        {
            _isAnswering = true;
            _anim.Play("Call");
            _indicator = Instantiate(CallIndicator, transform).transform;
            _indicator.GetComponent<Canvas>().worldCamera = Camera.main;
            _indicator.GetComponentInChildren<Text>().text = "I have: " + toIp;
            _indicator.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }

        private void OnStop()
        {
            if (_indicator != null)
                Destroy(_indicator.gameObject);
            if (_callback != null && _target != null && _toIp != null)
                _callback(_target, _toIp);
            if (_isAnswering)
            {
                _isAnswering = false;
                NetworkVisualizer.EventHandler.Broadcast(Events.CALL_FINISHED);
            }
        }
    }
}