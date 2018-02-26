using System.Collections;
using NetworkVisualizer.Objects;
using UnityEngine;

namespace NetworkVisualizer
{
    public static class Test
    {

        private static Device _router;
        private static Device _esp_1;
        private static Device _esp_2;


        public static void Init()
        {
             _router = new Device("router", new Position(1, 1, 1));
            _esp_1 = new Device("esp_1", new Position(1, 2, 3));
            _esp_2 = new Device("esp_1", new Position(3, 2, 1));


        }

        public static void Start()
        {
            new WaitForSeconds(2f);
            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, new Connection(_router, _esp_1, "connection", "someTime", "someBody"));
            new WaitForSeconds(2f);
            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, new Connection(_esp_1, _esp_2, "connection", "someTime", "someBody"));
            new WaitForSeconds(2f);
            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, new Call(_router, "call", "someTime"));
            
        }

    }
}
