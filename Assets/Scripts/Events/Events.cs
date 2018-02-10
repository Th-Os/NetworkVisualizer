﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NetworkVisualizer.Objects;

public class Events {

    public enum EVENTS {DEVICE_FOUND, DATA_ARRIVED, REQUEST_LOCAL_DATA, REQUEST_DATA, START_DEFINE, END_DEFINE, START_TEST, END_TEST, NEW_CONNECTION, SHOW_DATA, HIDE_DATA, DATA_VISUALIZED, DRAW_CONNECTION, DRAW_CALL, HIGHLIGHT_OJECT};

    public delegate void DeviceFoundHandler(Transform transform);
    public static event DeviceFoundHandler OnDeviceFound;

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

    public delegate void ShowDataHandler(GameObject obj, Data data);
    public static event ShowDataHandler OnShowData;

    public delegate void HideDataHandler(int id);
    public static event HideDataHandler OnHideData;

    public delegate void DrawConnectionHandler(Transform source, Transform target);
    public static event DrawConnectionHandler OnDrawConnection;

    public delegate void DrawCallHandler(Transform source);
    public static event DrawCallHandler OnDrawCall;

    public delegate void HighlightObjectHandler(Transform obj);
    public static event HighlightObjectHandler OnHighlight;


    public static void Broadcast<T, E> (EVENTS evnt, T value1, E value2)
    {
        switch (evnt)
        {
            case EVENTS.SHOW_DATA:
                OnShowData(value1 as GameObject, value2 as Data);
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
            case EVENTS.DEVICE_FOUND:
                OnDeviceFound(value as Transform);
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
            case EVENTS.HIGHLIGHT_OJECT:
                OnHighlight(value as Transform);
                break;
            case EVENTS.REQUEST_LOCAL_DATA:
                OnLocalDataRequested(value as Transform);
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
