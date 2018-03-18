using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using UnityEngine;
using NetworkVisualizer.Enums;
using NetworkVisualizer.Objects;
using NetworkVisualizer.Data;

namespace NetworkVisualizer
{

    public static class MqttController
    {

        private static string PUB_TOPIC = "controller";
        private static string SUB_TOPIC = "hololens/#";
        private static string CONNECTION = "hololens/connection";
        private static string CALL = "hololens/call";
        private static string INCOMING_DATA = "hololens/data";

        private static MqttClient client;

        public static void Init(String uri)
        {
            try
            {
                client = new MqttClient(uri);
                client.MqttMsgPublishReceived += OnMessageReceived;

                string clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);

                client.Subscribe(new String[] { SUB_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

                Debug.Log("MQTT started with " + uri + " as host");


                Send(PUB_TOPIC + "/status", "Hololens online");

                EventHandler.OnDataRequested += SendDataRequest;
                EventHandler.OnInformTestStarted += SendTestInitializer;
                EventHandler.OnTestEnded += SendTestCancel;
                EventHandler.OnDeviceDefined += SendDeviceData;

            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
                Debug.Log(e.Message);
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

        private static void SendTestCancel(int test)
        {
            Send(PUB_TOPIC + "/test/stop", "{\"stop\": true}");
        }

        public static void SendDeviceData(Transform transform)
        {
            Device device = new Device(transform.name, new Position(transform.position));
            DataStore.Instance.AddDevice(transform, device);
            string json = JsonConvert.SerializeObject(device);
            Send(PUB_TOPIC + "/device", json);
        }

        static void SendDataRequest(DataRequest request)
        {
            Debug.Log("Get Data for " + request.Type + " : " + request.Id + " in this manner: multiple: " + request.Multiple + ", timebased: " + request.Timebased + ", specific: " + request.Specific);
            Send(PUB_TOPIC + "/data", JsonConvert.SerializeObject(request));

        }

        private static void OnMessageReceived(object sender, MqttMsgPublishEventArgs e)
        {     
            ProcessMessage(e.Topic.ToString(), Encoding.UTF8.GetString(e.Message));
        }

        private static void ProcessMessage(string topic, string msg)
        {
            string jsonString = msg.Substring(msg.IndexOf("{"), (msg.LastIndexOf("}") - msg.IndexOf("{")) + 1);
            Debug.Log("Got message: " + topic + ": " + jsonString);
            try
            {
                if (String.Equals(topic, CONNECTION, StringComparison.OrdinalIgnoreCase))
                {
                    EventHandler.Broadcast(Events.NEW_CONNECTION, JsonConvert.DeserializeObject<Connection>(jsonString));
                }
                if (String.Equals(topic, CALL, StringComparison.OrdinalIgnoreCase))
                {
                    EventHandler.Broadcast(Events.NEW_CONNECTION, JsonConvert.DeserializeObject<Call>(jsonString));
                }
                if (String.Equals(topic, INCOMING_DATA, StringComparison.OrdinalIgnoreCase))
                {
                    EventHandler.Broadcast(Events.DATA_ARRIVED, JsonConvert.DeserializeObject<DataResponse>(jsonString));
                }
            }
            catch (Exception exception)
            {
                Debug.Log(exception.StackTrace);
                Debug.Log(exception.Message);
            }
        }
    }
}