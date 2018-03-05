using System.Collections;
using NetworkVisualizer.Objects;
using NetworkVisualizer.Enums;
using UnityEngine;

namespace NetworkVisualizer
{
    public class Test : MonoBehaviour
    {

        private static Device _router;
        private static Device _esp_1;
        private static Device _esp_2;


        public void Init()
        {
             _router = new Device("router", new Position(1, 1, 1, 1));
            _esp_1 = new Device("esp_1", new Position(2, 1, 2, 3));
            _esp_2 = new Device("esp_2", new Position(3, 3, 2, 1));

            //StartCoroutine(Run());

        }

        IEnumerator Run()
        {
            yield return new WaitForSeconds(4f);
            EventHandler.Broadcast(Events.NEW_CONNECTION, new Connection(1, _router, _esp_1, "connection", "someTime", "someBody"));
            yield return new WaitForSeconds(2f);
            EventHandler.Broadcast(Events.NEW_CONNECTION, new Connection(2, _esp_1, _esp_2, "connection", "someTime", "someBody"));
            yield return new WaitForSeconds(2f);
            EventHandler.Broadcast(Events.NEW_CONNECTION, new Call(1, _router, "call", "someTime"));
            yield return new WaitForSeconds(4f);

            EventHandler.Broadcast(Events.NEW_CONNECTION, new Connection(3, _router, _esp_1, "connection", "someTime", "someBody"));
            yield return new WaitForSeconds(4f);
            EventHandler.Broadcast(Events.NEW_CONNECTION, new Connection(4, _router, _esp_1, "connection", "someTime", "someBody"));
            yield return new WaitForSeconds(4f);
            EventHandler.Broadcast(Events.NEW_CONNECTION, new Connection(5, _router, _esp_1, "connection", "someTime", "someBody"));
            yield return new WaitForSeconds(4f);
        }

    }
}
