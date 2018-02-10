using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using UnityEngine;

using NetworkVisualizer.Objects;

namespace NetworkVisualizer {
    public static class MqttController {

        private static string PUB_TOPIC = "controller";
        private static string SUB_TOPIC = "hololens/#";
        private static string CONNECTION = "hololens/connection";
        private static string CALL = "hololens/call";
        private static string INCOMING_DATA = "hololens/data";

        private static MqttClient client;

        public static void Start(String uri)
        {
            try
            {
                client = new MqttClient(uri);
                client.MqttMsgPublishReceived += OnMessageReceived;

                string clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);

                client.Subscribe(new String[] { SUB_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

                Debug.Log("MQTT started with " + uri + " as host");


                Send(PUB_TOPIC, "Hololens online");

                Events.OnDataRequested += SendDataRequest;
                Events.OnTestStarted += SendTestInitializer;
                Events.OnDeviceFound += SendDeviceData;

            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
                Debug.Log("No start up of Mqtt module possible.");
            }
        }

        static void Send(string topic, string msg)
        {
            Debug.Log("Sent message: " + topic + ": " + msg);
            client.Publish(topic, Encoding.UTF8.GetBytes(msg), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        private static void SendTestInitializer(int testId)
        {
            Send(PUB_TOPIC + "/test", "{\"test\":" + testId + "}");
        }

        public static void SendDeviceData(Transform transform)
        {
            Device device = new Device(transform.name, new Position(transform.position));
            DataStore.Instance.AddDevice(transform, device);
            string json = JsonConvert.SerializeObject(device);
            Send(PUB_TOPIC, json);
        }

        static void SendDataRequest(DataRequest request)
        {
            Send(PUB_TOPIC + "/data", JsonConvert.SerializeObject(request));
            Debug.Log("Get Data for " + request.Type + " : " + request.Id + " in this manner: multiple: " + request.Multiple + ", timebased: " + request.Timebased + ", specific: " + request.Specific);

        }

        private static void OnMessageReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string topic = e.Topic.ToString();
            string msg = Encoding.UTF8.GetString(e.Message);

            if (String.Equals(topic, CONNECTION, StringComparison.OrdinalIgnoreCase))
            {
                Events.Broadcast(Events.EVENTS.NEW_CONNECTION, JsonConvert.DeserializeObject<Connection>(msg));
            }
            if (String.Equals(topic, CALL, StringComparison.OrdinalIgnoreCase))
            {
                Events.Broadcast(Events.EVENTS.NEW_CONNECTION, JsonConvert.DeserializeObject<Call>(msg));
            }
            if (String.Equals(topic, INCOMING_DATA, StringComparison.OrdinalIgnoreCase))
            {
                Events.Broadcast(Events.EVENTS.DATA_ARRIVED, JsonConvert.DeserializeObject<Data>(msg));
            }

            Debug.Log("Got message: " + topic + ": " + msg);

        }

    }
}