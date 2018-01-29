using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NetworkVisualizer.Objects;

public class Events {

    public enum EVENTS {DEVICE_FOUND, DATA_ARRIVED, REQUEST_DATA, START_TEST, END_TEST, NEW_CONNECTION, SWITCH_UI, SHOW_DATA, HIDE_DATA, DATA_VISUALIZED};

    public delegate void DeviceFound(Transform transform);
    public static event DeviceFound OnDeviceFound;

    public delegate void DataRequested(DataRequest request);
    public static event DataRequested OnDataRequested;

    public delegate void DataArrived(Data data);
    public static event DataArrived OnDataArrived;

    public delegate void TestStarted(int id);
    public static event TestStarted OnTestStarted;

    public delegate void TestEnded();
    public static event TestEnded OnTestEnded;

    public delegate void NewConnection(NetworkObject obj);
    public static event NewConnection OnNewConnection;

    public delegate void TestUISwitched();
    public static event TestUISwitched OnTestUISwitched;

    public delegate void ShowData(GameObject obj, Data data);
    public static event ShowData OnShowData;

    public delegate void HideData(int id);
    public static event HideData OnHideData;

    public static void Broadcast<T, E> (EVENTS evnt, T value1, E value2)
    {
        switch (evnt)
        {
            case EVENTS.SHOW_DATA:
                OnShowData(value1 as GameObject, value2 as Data);
                break;
            default:
                Debug.Log("NO valid event broadcast for " + evnt.ToString());
                break;
        }
    }

    public static void Broadcast<T> (EVENTS evnt, T value)
    {
        switch(evnt)
        {
            case EVENTS.DEVICE_FOUND:
                OnDeviceFound(value as Transform);
                break;
            case EVENTS.DATA_ARRIVED:
                OnDataArrived(value as Data);
                break;
            case EVENTS.NEW_CONNECTION:
                OnNewConnection(value as NetworkObject);
                break;
            default:
                Debug.Log("NO valid event broadcast for " + evnt.ToString());
                break;
        }
        
    }

    public static void Broadcast (EVENTS evnt, int value)
    {
        switch (evnt)
        {
            case EVENTS.HIDE_DATA:
                OnHideData(value);
                break;
            case EVENTS.START_TEST:
                OnTestStarted(value);
                break;
            default:
                break;
        }
    }

    public static void Broadcast(EVENTS evnt)
    {
        switch (evnt)
        {
            case EVENTS.END_TEST:
                OnTestEnded();
                break;
            case EVENTS.SWITCH_UI:
                OnTestUISwitched();
                break;
            default:
                Debug.Log("NO valid event broadcast for " + evnt.ToString());
                break;
        }

    }
}
