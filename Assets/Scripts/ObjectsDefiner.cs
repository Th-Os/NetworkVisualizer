using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;

public class ObjectsDefiner : MonoBehaviour {

    public Transform Camera;
    public Transform Cube;

    

    Canvas canvas;
    Text text;
    Vector3 position;
    Transform currentObj;
    GestureRecognizer recognizer;
    bool disabled;

    // Use this for initialization
    void Start () {
        canvas = GetComponentInChildren<Canvas>();
        text = canvas.GetComponentInChildren<Text>();
        currentObj = Instantiate(Cube, this.transform);
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.Tapped += OnTap;
        recognizer.GestureError += OnError;
        recognizer.StartCapturingGestures();

        InteractionManager.InteractionSourcePressed += InteractionManager_SourcePressed;

        disabled = false;

        Debug.Log("Definer started");
    }

    private void InteractionManager_SourcePressed(InteractionSourcePressedEventArgs args)
    {
        Debug.Log("hello " + args.pressType);
    }

    // Update is called once per frame
    void Update () {
        if(!disabled)
        {
            position = GazeManager.Instance.HitInfo.point;
            currentObj.position = position;
            text.text = position.ToString();
        }

    }

    void OnTap(TappedEventArgs args)
    {
        Debug.Log("TAPPED " + args.tapCount);
        
        currentObj = Instantiate(Cube, currentObj.position, currentObj.rotation, this.transform);
        Events.Broadcast(Events.EVENTS.DEVICE_FOUND, currentObj.gameObject);
        //EventManager.Broadcast(EVNT.NewDevice, currentObj);
    }

    void OnError(GestureErrorEventArgs args)
    {
        Debug.Log(args.error);
    }

    private void OnDestroy()
    {
        recognizer.Tapped -= OnTap;
        recognizer.StopCapturingGestures();
    }

    private void OnDisable()
    {
        disabled = true;
        recognizer.Tapped -= OnTap;
        recognizer.StopCapturingGestures();
    }
}
