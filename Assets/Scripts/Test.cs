using System.Collections;
using NetworkVisualizer.Objects;
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
             _router = new Device("router", new Position(1, 1, 1));
            _esp_1 = new Device("esp_1", new Position(1, 2, 3));
            _esp_2 = new Device("esp_1", new Position(3, 2, 1));

            StartCoroutine(Run());

        }

        IEnumerator Run()
        {
            yield return new WaitForSeconds(4f);
            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, new Connection(_router, _esp_1, "connection", "someTime", "someBody"));
            yield return new WaitForSeconds(2f);
            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, new Connection(_esp_1, _esp_2, "connection", "someTime", "someBody"));
            yield return new WaitForSeconds(2f);
            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, new Call(_router, "call", "someTime"));
            yield return new WaitForSeconds(4f);

            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, new Connection(_router, _esp_1, "connection", "someTime", "someBody"));
            yield return new WaitForSeconds(4f);
            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, new Connection(_router, _esp_1, "connection", "someTime", "someBody"));
            yield return new WaitForSeconds(4f);
            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, new Connection(_router, _esp_1, "connection", "someTime", "someBody"));
            yield return new WaitForSeconds(4f);
        }

    }
}
