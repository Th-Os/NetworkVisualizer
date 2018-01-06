using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NetworkVisualizer.Objects;

public class Events {

    public enum EVENTS {DEVICE_FOUND, DATA_ARRIVED, DATA_REQUEST, TEST_STARTED, TEST_ENDED, NEW_CONNECTION, SWICHTED_TEST_UI};

    public delegate void DeviceFound(GameObject obj);
    public static event DeviceFound OnDeviceFound;

    public delegate void DataRequested(string name);
    public static event DataRequested OnDataRequested;

    public delegate void DataArrived(Data data);
    public static event DataArrived OnDataArrived;

    public delegate void TestStarted();
    public static event TestStarted OnTestStarted;

    public delegate void TestEnded();
    public static event TestEnded OnTestEnded;

    public delegate void NewConnection(NetworkObject obj);
    public static event NewConnection OnNewConnection;

    public delegate void TestUISwitched();
    public static event TestUISwitched OnTestUISwitched;

    public delegate void Test(string test);
    public static event Test OnTest;

    public static void Broadcast<T> (EVENTS evnt, T value)
    {
        switch(evnt)
        {
            case EVENTS.DEVICE_FOUND:
                OnDeviceFound(value as GameObject);
                break;
            case EVENTS.DATA_ARRIVED:
                OnDataArrived(value as Data);
                break;
            case EVENTS.TEST_STARTED:
                OnTestStarted();
                break;
            case EVENTS.TEST_ENDED:
                OnTestEnded();
                break;
            case EVENTS.NEW_CONNECTION:
                OnNewConnection(value as NetworkObject);
                break;
            case EVENTS.SWICHTED_TEST_UI:
                OnTestUISwitched();
                break;
            default:
                OnTest(value as string);
                break;
        }
        
    }
}
