using UnityEngine;
using System.Collections;

namespace NetworkVisualizer
{

    public class MainController : MonoBehaviour
    {
        public GameObject ParentUI;
        public GameObject TestUI;
        public GameObject DefineUI;
        public GameObject VisualizeUI;
        public MQTT_URI URI = MQTT_URI.TEST;

        public enum MQTT_URI {
            TEST,
            PRODUCTIVE
        };

        // Use this for initialization
        void Start()
        {
            string server = "";
            if(URI == MQTT_URI.TEST)
            {
                server = "littleone";
            }
            if(URI == MQTT_URI.PRODUCTIVE)
            {
                server = "10.0.0.1";
            }
            MqttController.Init(server);
            CanvasController.Instance.Init(ParentUI, TestUI, DefineUI, VisualizeUI);
            DataController.Instance.Init();

            Events.OnTestStarted += OnTestStarted;
            Events.OnTestEnded += OnTestEnded;
            StartCoroutine(InitApp());
            StartCoroutine(StopTest());

        }

        IEnumerator InitApp()
        {
            Debug.Log("Wait for 5 seconds, then start application.");
            yield return new WaitForSeconds(5f);
            Events.Broadcast(Events.EVENTS.START_DEFINE);
        }

        IEnumerator StopTest()
        {
            yield return new WaitForSeconds(120f);
            Events.Broadcast(Events.EVENTS.END_TEST);
        }



        void OnTestStarted(int test)
        {
            
        }

        void OnTestEnded()
        {
           
        }
    }
}