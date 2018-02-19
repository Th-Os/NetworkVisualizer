using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NetworkVisualizer.Objects;

public class Events {

    public enum EVENTS {DEVICE_FOUND, DEVICE_DEFINED, DATA_ARRIVED, REQUEST_LOCAL_DATA, REQUEST_DATA, START_DEFINE, END_DEFINE, START_TEST, END_TEST, NEW_CONNECTION, SHOW_DEVICE_DATA, SHOW_CONNECTION_DATA, DATA_VISUALIZED, DRAW_CONNECTION, DRAW_CALL, HIGHLIGHT_OJECT, HIDE_OBJECT};

    public delegate void DeviceFoundHandler(Vector3 position);
    public static event DeviceFoundHandler OnDeviceFound;

    public delegate void DeviceDefinedHandler(Transform transform);
    public static event DeviceDefinedHandler OnDeviceDefined;

    public delegate void DataRequestedHandler(DataRequest request);
    public static event DataRequestedHandler OnDataRequested;

    public delegate void LocalDataRequestHandler(Transform one, Transform two = null);
    public static event LocalDataRequestHandler OnLocalDataRequested;

    public delegate void DataArrivedHandler(Data data);
    public static event DataArrivedHandler OnDataArrived;

    public delegate void TestStartedHandler(int id);
    public static event TestStartedHandler OnTestStarted;

    public delegate void TestEndedHandler();
    public static event TestEndedHandler OnTestEnded;

    public delegate void NewConnectionHandler(NetworkObject obj);
    public static event NewConnectionHandler OnNewConnection;

    public delegate void StartDefineProcessHandler();
    public static event StartDefineProcessHandler OnDefineProcessStarted;

    public delegate void EndDefineProcessHander();
    public static event EndDefineProcessHander OnDefineProcessEnded;

    public delegate void ShowDeviceDataHandler(GameObject obj, Device device);
    public static event ShowDeviceDataHandler OnShowDeviceData;

    public delegate void ShowConnectionDataHandler(DeviceConnection dc, Connection connection);
    public static event ShowConnectionDataHandler OnShowConnectionData;

    public delegate void DrawConnectionHandler(Transform source, Transform target);
    public static event DrawConnectionHandler OnDrawConnection;

    public delegate void DrawCallHandler(Transform source);
    public static event DrawCallHandler OnDrawCall;

    public delegate void HighlightObjectHandler(Transform obj);
    public static event HighlightObjectHandler OnHighlight;

    public delegate void HideObjectHandler(Transform obj);
    public static event HideObjectHandler OnHide;

    public static void Broadcast<T, E> (EVENTS evnt, T value1, E value2)
    {
        switch (evnt)
        {
            case EVENTS.SHOW_DEVICE_DATA:
                OnShowDeviceData(value1 as GameObject, value2 as Device);
                break;
            case EVENTS.SHOW_CONNECTION_DATA:
                OnShowConnectionData(value1 as DeviceConnection, value2 as Connection);
                break;
            case EVENTS.DRAW_CONNECTION:
                OnDrawConnection(value1 as Transform, value2 as Transform);
                break;
            case EVENTS.REQUEST_LOCAL_DATA:
                OnLocalDataRequested(value1 as Transform, value2 as Transform);
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
            case EVENTS.DEVICE_DEFINED:
                OnDeviceDefined(value as Transform);
                break;
            case EVENTS.DATA_ARRIVED:
                OnDataArrived(value as Data);
                break;
            case EVENTS.NEW_CONNECTION:
                OnNewConnection(value as NetworkObject);
                break;
            case EVENTS.DRAW_CALL:
                OnDrawCall(value as Transform);
                break;
            case EVENTS.REQUEST_LOCAL_DATA:
                OnLocalDataRequested(value as Transform);
                break;
            case EVENTS.HIGHLIGHT_OJECT:
                OnHighlight(value as Transform);
                break;
            case EVENTS.HIDE_OBJECT:
                OnHide(value as Transform);
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
            case EVENTS.START_TEST:
                OnTestStarted(value);
                break;
            default:
                Debug.Log("NO valid event broadcast for " + evnt.ToString());
                break;
        }
    }

    public static void Broadcast(EVENTS evnt, Vector3 position)
    {
        switch (evnt)
        {
            case EVENTS.DEVICE_FOUND:
                OnDeviceFound(position);
                break;
            default:
                Debug.Log("NO valid event broadcast for " + evnt.ToString());
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
            case EVENTS.START_DEFINE:
                OnDefineProcessStarted();
                break;
            case EVENTS.END_DEFINE:
                OnDefineProcessEnded();
                break;
            default:
                Debug.Log("NO valid event broadcast for " + evnt.ToString());
                break;
        }

    }
}
