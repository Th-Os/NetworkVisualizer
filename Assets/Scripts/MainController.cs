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
            switch(m_MQTT)
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
            if(!server.Equals(""))
                MqttController.Init(server);
            CanvasController.Instance.Init(ParentUI, TestUI, DefineUI, VisualizeUI);
            DataController.Instance.Init();

            Events.OnTestStarted += OnTestStarted;
            Events.OnTestEnded += OnTestEnded;
            StartCoroutine(InitApp());
            //StartCoroutine(StopTest());
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
            GetComponent<Test>().Init();
        }

        void OnTestEnded()
        {
           
        }
    }
}