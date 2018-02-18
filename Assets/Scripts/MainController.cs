using UnityEngine;

namespace NetworkVisualizer
{

    public class MainController : MonoBehaviour
    {
        public GameObject ParentUI;
        public GameObject TestUI;
        public GameObject DefineUI;
        public GameObject WorldUI;

        public string MQTT_URI;

        // Use this for initialization
        void Start()
        {
            MqttController.Init(MQTT_URI);
            CanvasController.Instance.Init(ParentUI, TestUI, DefineUI, WorldUI);
            DataController.Instance.Init();

            Events.OnTestStarted += OnTestStarted;
            Events.OnTestEnded += OnTestEnded;

            Events.Broadcast(Events.EVENTS.START_DEFINE);

        }


        void OnTestStarted(int test)
        {
            
        }

        void OnTestEnded()
        {
           
        }
    }
}