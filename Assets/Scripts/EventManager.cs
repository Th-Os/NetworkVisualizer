using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum EVNT { TestClicked, StartTest, Mqtt, NewConnection, UpdateDevice, NewDevice, EndTest, SwitchTestUI };

public static class EventManager
{
    //TODO EventManager erweitern
    private static Dictionary<EVNT, Delegate> eventTable = new Dictionary<EVNT, Delegate>();

    public static void AddHandler<T>(EVNT evnt, Action<T> action)
    {
        if (!eventTable.ContainsKey(evnt)) eventTable[evnt] = action;
        else eventTable[evnt] = (Action<T>)eventTable[evnt] + action;
    }


    public static void AddHandler<T, E>(EVNT evnt, Action<T, E> action)
    {
        if (!eventTable.ContainsKey(evnt)) eventTable[evnt] = action;
        else eventTable[evnt] = (Action<T, E>)eventTable[evnt] + action;
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


    public static void Broadcast <T>(EVNT evnt, T value)
    {
        Delegate d;
        if (eventTable.TryGetValue(evnt, out d))
        {
            Action<T> action = d as Action<T>;
            if (action != null) action(value);
        }
    }

    public static void Broadcast<T, E>(EVNT evnt, T value1, E value2)
    {
        Delegate d;
        if (eventTable.TryGetValue(evnt, out d))
        {
            Action<T, E> action = d as Action<T, E>;
            if (action != null) action(value1, value2);
        }
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
            if (d is Action<string>)
            {
                Action<string> action = d as Action<string>;
                if (action != null) action(value);
            }
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