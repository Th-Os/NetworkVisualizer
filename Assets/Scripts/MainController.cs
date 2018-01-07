using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NetworkVisualizer.Objects;

namespace NetworkVisualizer
{

    public class MainController : MonoBehaviour
    {

        public string m_Uri;

        // Use this for initialization
        void Start()
        {
            EventManager.AddHandler<string>(EVNT.StartTest, BeginTest);
           // MqttController.Start(m_Uri);

            Test();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void BeginTest(string name)
        {
            EventManager.Broadcast(EVNT.SwitchTestUI, "");
            Debug.Log("Beginning " + name);
        }

        void Test()
        {
            /*
            EventManager.AddHandler<string>(EVNT.UpdateDevice, TestString);
            EventManager.Broadcast<string>(EVNT.UpdateDevice, "hello");

            EventManager.AddHandler<GameObject>(EVNT.UpdateDevice, TestTransform);
            EventManager.Broadcast<GameObject>(EVNT.UpdateDevice, new GameObject());
            */

            Events.OnTest += TestString;

        }

        void TestString(string value)
        {
            Debug.Log("TestString:  " + value);
        }

        void TestTransform(GameObject t)
        {
            Debug.Log("TestObject " + t.ToString());
        }
    }
}