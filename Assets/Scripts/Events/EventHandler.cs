using UnityEngine;
using NetworkVisualizer.Enums;
using NetworkVisualizer.Objects;
using NetworkVisualizer.Visual;

namespace NetworkVisualizer
{
    /// <summary>
    /// The Eventhandler can broadcast from everywhere to all subscribers.
    /// </summary>
    public class EventHandler
    {

        public delegate void DeviceFoundHandler(Vector3 position);
        public static event DeviceFoundHandler OnDeviceFound;

        public delegate void DeviceDefinedHandler(Transform transform);
        public static event DeviceDefinedHandler OnDeviceDefined;

        public delegate void FocusHandler(GameObject obg);
        public static event FocusHandler OnFocus;
        public static event FocusHandler OnUnfocus;

        public delegate void DataRequestedHandler(DataRequest request);
        public static event DataRequestedHandler OnDataRequested;

        public delegate void LocalDataRequestHandler(Transform one, Transform two = null);
        public static event LocalDataRequestHandler OnLocalDataRequested;

        public delegate void DataArrivedHandler(DataResponse data);
        public static event DataArrivedHandler OnDataArrived;

        public delegate void TestHandler(int id);
        public static event TestHandler OnTestStarted;
        public static event TestHandler OnInformTestStarted;
        public static event TestHandler OnTestEnded;
        public static event TestHandler OnShowTest;

        public delegate void NewConnectionHandler(NetworkObject obj);
        public static event NewConnectionHandler OnNewConnection;

        public delegate void DefineProcessHandler();
        public static event DefineProcessHandler OnDefineProcessStarted;
        public static event DefineProcessHandler OnDefineProcessEnded;

        public delegate void ShowDeviceDataHandler(GameObject obj, Device device);
        public static event ShowDeviceDataHandler OnShowDeviceData;

        public delegate void ShowConnectionDataHandler(DeviceConnection dc, Connection connection);
        public static event ShowConnectionDataHandler OnShowConnectionData;

        public delegate void DrawConnectionHandler(Transform source, Transform target);
        public static event DrawConnectionHandler OnDrawConnection;

        public delegate void DrawCallHandler(Transform source, Transform target, string toIp);
        public static event DrawCallHandler OnDrawCall;

        public delegate void CallFinishedHandler();
        public static event CallFinishedHandler OnFinishedCall;

        public delegate void HighlightUIHandler(Transform obj);
        public static event HighlightUIHandler OnHighlight;
        public static event HighlightUIHandler OnHide;

        public delegate void MenuHandler();
        public static event MenuHandler OnShowMenu;
        public static event MenuHandler OnHideMenu;

        public delegate void DestroyHandler();
        public static event DestroyHandler OnDestroyVisualization;

        /// <summary>
        /// Used for 3 parameters. All possible events:
        /// <list type="bullet"><item>DRAW_CALL</item></list>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="evnt"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        public static void Broadcast<T, E, F>(Events evnt, T value1, E value2, F value3)
        {
            switch (evnt)
            {
                case Events.DRAW_CALL:
                    OnDrawCall(value1 as Transform, value2 as Transform, value3 as string);
                    break;
                default:
                    Debug.LogWarning("NO valid event broadcast for " + evnt.ToString());
                    break;
            }
        }

        /// <summary>
        /// Used for 2 parameters. All possible events:
        /// SHOW_DEVICE_DATA
        /// SHOW_CONNECTION_DATA
        /// DRAW_CONNECTION
        /// REQUEST_LOCAL_DATA
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="evnt"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        public static void Broadcast<T, E>(Events evnt, T value1, E value2)
        {
            switch (evnt)
            {
                case Events.SHOW_DEVICE_DATA:
                    OnShowDeviceData(value1 as GameObject, value2 as Device);
                    break;
                case Events.SHOW_CONNECTION_DATA:
                    OnShowConnectionData(value1 as DeviceConnection, value2 as Connection);
                    break;
                case Events.DRAW_CONNECTION:
                    OnDrawConnection(value1 as Transform, value2 as Transform);
                    break;
                case Events.REQUEST_LOCAL_DATA:
                    OnLocalDataRequested(value1 as Transform, value2 as Transform);
                    break;
                default:
                    Debug.LogWarning("NO valid event broadcast for " + evnt.ToString());
                    break;
            }
        }

        /// <summary>
        /// Used for 1 parameter. All used Events:
        /// DEVICE_DEFINED
        /// DATA_ARRIVED
        /// NEW_CONNECTION
        /// REQUEST_LOCAL_DATA
        /// HIGHLIGHT_OBJECT
        /// HIDE_OBJECT
        /// FOCUS
        /// UNFOCUS
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="evnt"></param>
        /// <param name="value"></param>
        public static void Broadcast<T>(Events evnt, T value)
        {
            switch (evnt)
            {
                case Events.DEVICE_DEFINED:
                    OnDeviceDefined(value as Transform);
                    break;
                case Events.DATA_ARRIVED:
                    OnDataArrived(value as DataResponse);
                    break;
                case Events.NEW_CONNECTION:
                    OnNewConnection(value as NetworkObject);
                    break;
                case Events.REQUEST_LOCAL_DATA:
                    OnLocalDataRequested(value as Transform);
                    break;
                case Events.REQUEST_DATA:
                    OnDataRequested(value as DataRequest);
                    break;
                case Events.HIGHLIGHT_OJECT:
                    OnHighlight(value as Transform);
                    break;
                case Events.HIDE_OBJECT:
                    OnHide(value as Transform);
                    break;
                case Events.FOCUS:
                    OnFocus(value as GameObject);
                    break;
                case Events.UNFOCUS:
                    OnUnfocus(value as GameObject);
                    break;
                default:
                    Debug.LogWarning("NO valid event broadcast for " + evnt.ToString());
                    break;
            }
        }

        /// <summary>
        /// Uses one integer value. All valid events:
        /// START_TEST
        /// SHOW_TEST
        /// END_TEST
        /// INFORM_START_TEST
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="value"></param>
        public static void Broadcast(Events evnt, int value)
        {
            switch (evnt)
            {
                case Events.START_TEST:
                    OnTestStarted(value);
                    break;
                case Events.SHOW_TEST:
                    OnShowTest(value);
                    break;
                case Events.END_TEST:
                    OnTestEnded(value);
                    break;
                case Events.INFORM_START_TEST:
                    OnInformTestStarted(value);
                    break;
                default:
                    Debug.LogWarning("NO valid event broadcast for " + evnt.ToString());
                    break;
            }
        }

        /// <summary>
        /// Uses one <see cref="Vector3"/> value. All applicable events:
        /// DEVICE_FOUND
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="position"></param>
        public static void Broadcast(Events evnt, Vector3 position)
        {
            switch (evnt)
            {
                case Events.DEVICE_FOUND:
                    OnDeviceFound(position);
                    break;
                default:
                    Debug.LogWarning("NO valid event broadcast for " + evnt.ToString());
                    break;
            }
        }

        /// <summary>
        /// Uses no parameters. All possible events:
        /// START_DEFINE
        /// END_DEFINE
        /// OPEN_MENU
        /// HIDE_MENU
        /// DESTROY_VISUALIZATION
        /// CALL_FINISHED
        /// </summary>
        /// <param name="evnt"></param>
        public static void Broadcast(Events evnt)
        {
            switch (evnt)
            {
                case Events.START_DEFINE:
                    OnDefineProcessStarted();
                    break;
                case Events.END_DEFINE:
                    OnDefineProcessEnded();
                    break;
                case Events.OPEN_MENU:
                    OnShowMenu();
                    break;
                case Events.HIDE_MENU:
                    OnHideMenu();
                    break;
                case Events.DESTROY_VISUALIZATION:
                    OnDestroyVisualization();
                    break;
                case Events.CALL_FINISHED:
                    OnFinishedCall();
                    break;
                default:
                    Debug.LogWarning("NO valid event broadcast for " + evnt.ToString());
                    break;
            }

        }
    }
}
