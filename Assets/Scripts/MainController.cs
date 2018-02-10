using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using NetworkVisualizer.Objects;
using Newtonsoft.Json;

namespace NetworkVisualizer
{

    public class MainController : MonoBehaviour
    {

        public string m_Uri;

        // Use this for initialization
        void Start()
        {
            MqttController.Start(m_Uri);
            Events.OnTestStarted += OnTestStarted;
            Events.OnTestEnded += OnTestEnded;

        }


        void OnTestStarted(int test)
        {
            
        }

        void OnTestEnded()
        {
           
        }
    }
}