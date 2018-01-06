using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum EVNT { TestClicked, StartTest, Mqtt, NewConnection, UpdateDevice, NewDevice, EndTest, SwitchTestUI };

public static class EventManager
{
    //TODO EventManager erweitern
    private static Dictionary<EVNT, Delegate> eventTable = new Dictionary<EVNT, Delegate>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="action">string</param>
    public static void AddHandler(EVNT evnt, Action<string> action)
    {
        if (!eventTable.ContainsKey(evnt)) eventTable[evnt] = action;
        else eventTable[evnt] = (Action<string>)eventTable[evnt] + action;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="action">string and vector3</param>
    public static void AddHandler(EVNT evnt, Action<string, Vector3> action)
    {
        if (!eventTable.ContainsKey(evnt)) eventTable[evnt] = action;
        else eventTable[evnt] = (Action<string, Vector3>)eventTable[evnt] + action;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="action">string and string</param>
    public static void AddHandler(EVNT evnt, Action<string, string> action)
    {
        if (!eventTable.ContainsKey(evnt)) eventTable[evnt] = action;
        else eventTable[evnt] = (Action<string, string>)eventTable[evnt] + action;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="value"></param>
    public static void Broadcast(EVNT evnt, string value)
    {
        Delegate d;
        if (eventTable.TryGetValue(evnt, out d))
        {
            Action<string> action = d as Action<string>;
            if (action != null) action(value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="value"></param>
    /// <param name="vector"></param>
    public static void Broadcast(EVNT evnt, string value, Vector3 vector)
    {
        Delegate d;
        if(eventTable.TryGetValue(evnt, out d))
        {
            Action<string, Vector3> action = d as Action<string, Vector3>;
            if (action != null) action(value, vector);
        }
    }

    public static void RemoveHandler(EVNT evnt, Action action)
    {
        if (eventTable[evnt] != null)
            eventTable[evnt] = (Action)eventTable[evnt] - action;
        if (eventTable[evnt] == null)
            eventTable.Remove(evnt);
    }
}