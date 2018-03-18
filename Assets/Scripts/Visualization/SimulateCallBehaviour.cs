using System.Collections;
using NetworkVisualizer.Objects;
using NetworkVisualizer.Enums;
using UnityEngine;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// This class simulates an ARP call to each Device. This is just a behaviour for the prototyping.
    /// </summary>
    public class SimulateCallBehaviour : MonoBehaviour
    {

        private static Device _router;
        private static Device _esp_1;
        private static Device _esp_2;
        private static Device _esp_3;
        private static Device _esp_4;

        private int _current;
        private int _test;

        public void Init(int test)
        {
            _test = test;
             _router = new Device("router", "10.0.0.1", new Position(1, 1, 1, 1));
            _esp_1 = new Device("esp_1", "10.0.0.11", new Position(2, 1, 2, 3));
            _esp_2 = new Device("esp_2", "10.0.0.12", new Position(3, 3, 2, 1));
            _esp_3 = new Device("esp_3", "10.0.0.13", new Position(3, 3, 2, 1));
            _esp_4 = new Device("esp_4", "10.0.0.14", new Position(3, 3, 2, 1));
            EventHandler.OnFinishedCall += OnCallFinished;
            _current = 1;

            StartCoroutine(Run(_current));

        }

        IEnumerator Run(int number)
        {
            yield return new WaitForSeconds(5f);
            switch (number)
            {
                case 1:
                    EventHandler.Broadcast(Events.NEW_CONNECTION, new Call(1, _router, _esp_1, "call", "someTime", _esp_1.Ip));
                    break;
                case 2:
                    EventHandler.Broadcast(Events.NEW_CONNECTION, new Call(1, _router, _esp_2, "call", "someTime", _esp_2.Ip));
                    break;
                case 3:
                    EventHandler.Broadcast(Events.NEW_CONNECTION, new Call(1, _router, _esp_3, "call", "someTime", _esp_3.Ip));
                    break;
                case 4:
                    EventHandler.Broadcast(Events.NEW_CONNECTION, new Call(1, _router, _esp_4, "call", "someTime", _esp_4.Ip));
                    break;
            }
        }

        private void OnCallFinished()
        {
            if (_current == 4)
            {
                EventHandler.Broadcast(Events.INFORM_START_TEST, _test);
                return;
            }
            _current++;
            StartCoroutine(Run(_current));
        }

    }
}
