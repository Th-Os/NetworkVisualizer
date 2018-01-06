using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using UnityEngine;

using NetworkVisualizer.Objects;

public static class MqttController {

    private static string PUB_TOPIC = "controller";
    private static string SUB_TOPIC = "hololens/#";
    private static string CONNECTION = "hololens/connection";
    private static string CALL = "hololens/call";
    private static string DATA = "hololens/data";

    private static MqttClient client;

    public static void Start(String uri)
    {
        client = new MqttClient(uri);
        client.MqttMsgPublishReceived += OnMessageReceived;

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);

        client.Subscribe(new String[] { SUB_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });


        Debug.Log("MQTT started with " + uri + " as host");


        Send(PUB_TOPIC, "Hololens online");
        SendDeviceData("blub", new Vector3(1,2,3));

        Events.OnDataRequested += SendDataRequest;
    }

    static void Send(string topic, string msg)
    {
        Debug.Log("Sent message: " + topic + ": " + msg);
        client.Publish(topic, Encoding.UTF8.GetBytes(msg), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
    }

    public static void SendDeviceData(string name, Vector3 position)
    {
        Device device = new Device(name, new Position(position.x, position.y, position.z));
        string json = JsonConvert.SerializeObject(device);
        Send(PUB_TOPIC, json);
    }

    static void SendDataRequest(string name)
    {
        Send(PUB_TOPIC + "/data", name);
        Debug.Log("Get Data for " + name);

    }

    private static void OnMessageReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string topic = e.Topic.ToString();
        string msg = Encoding.UTF8.GetString(e.Message);

        if(String.Equals(topic, CONNECTION, StringComparison.OrdinalIgnoreCase))
        {
            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, JsonConvert.DeserializeObject<Connection>(msg));
        }
        if (String.Equals(topic, CALL, StringComparison.OrdinalIgnoreCase))
        {
            Events.Broadcast(Events.EVENTS.NEW_CONNECTION, JsonConvert.DeserializeObject<Call>(msg));
        }
        if (String.Equals(topic, DATA, StringComparison.OrdinalIgnoreCase))
        {
            Events.Broadcast(Events.EVENTS.DATA_ARRIVED, JsonConvert.DeserializeObject<Data>(msg));
        }

        Debug.Log("Got message: " + topic + ": " + msg);
        
    }

}
