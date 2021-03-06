﻿using UnityEngine;
using System.Collections;
using NetworkVisualizer.Enums;
using NetworkVisualizer.Data;
using NetworkVisualizer.Visual;

namespace NetworkVisualizer
{
    /// <summary>
    /// The MainController manages and initializes other controllers:
    /// <see cref="MqttController"/>, <see cref="CanvasController"/> and <see cref="DataController"/>
    /// </summary>
    public class MainController : MonoBehaviour
    {
        public GameObject ParentUI;

        //Prefabs
        public GameObject TestUI;
        public GameObject DefineUI;
        public GameObject VisualizeUI;
        public GameObject MenuUI;

        public MQTT m_MQTT = MQTT.TEST;

        public enum MQTT {
            OFF,
            TEST,
            PRODUCTIVE
        };

        // Use this for initialization
        void Start()
        {
            
            string server = "";
            switch (m_MQTT)
            {
                case MQTT.TEST:
                    server = "littleone";
                    break;
                case MQTT.PRODUCTIVE:
                    server = "10.0.0.1";
                    break;
                case MQTT.OFF:
                    break;
            }
            if (!server.Equals(""))
                MqttController.Init(server);

            CanvasController.Instance.Init(ParentUI, TestUI, DefineUI, VisualizeUI, MenuUI);
            DataController.Instance.Init();

            EventHandler.OnTestStarted += OnTestStarted;
            EventHandler.OnTestEnded += OnTestEnded;
            StartCoroutine(InitApp());
            //StartCoroutine(StopTest());
            
        }

        private IEnumerator InitApp()
        {
            Debug.Log("Wait for 5 seconds, then start application.");
            yield return new WaitForSeconds(5f);
            EventHandler.Broadcast(Events.START_DEFINE);
        }

        private IEnumerator StopTest()
        {
            yield return new WaitForSeconds(120f);
            EventHandler.Broadcast(Events.END_TEST);
        }



        private void OnTestStarted(int test)
        {
            GetComponent<SimulateCallBehaviour>().Init(test);
        }

        private void OnTestEnded(int test)
        {
           
        }
    }
}