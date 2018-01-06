using Assets.World.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;

public static class MqttController {

    private static MqttClient client;

    public static void Start(String uri)
    {
        client = new MqttClient(uri);
        client.MqttMsgPublishReceived += OnMessageReceived;

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);

        client.Subscribe(new String[] { "hololens/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });


        Debug.Log("MQTT started with " + uri + " as host");


        Send("global","Hololens online");
    }

    static void Send(string topic, string msg)
    {
        Debug.Log("Sent message: " + topic + ": " + msg);
        client.Publish(topic, Encoding.UTF8.GetBytes(msg), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
    }

    // TODO: Not working
    public static void SendDeviceData(string name, Vector3 position)
    {
        Device device = new Device(name, new Position(position.x, position.y, position.z));
        
        string json = JsonUtility.ToJson(device);
        //Send("device", json);
    }

    static void SendDataRequest()
    {

    }

    // TODO: Ankommende JSON Objekte serialisieren.
    private static void OnMessageReceived(object sender, MqttMsgPublishEventArgs e)
    {
        Debug.Log("Got message: " + e.Topic.ToString() + ": " + Encoding.UTF8.GetString(e.Message));
        EventManager.Broadcast(EVNT.Mqtt, e.Topic.ToString() + "," + Encoding.UTF8.GetString(e.Message));
    }

}
